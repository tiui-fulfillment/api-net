using Microsoft.EntityFrameworkCore.Storage;
using Tiui.Application.Repository.UnitOfWork;

namespace Tiui.Data.UnitOfWork
{
    /// <summary>
    /// Implementación del patrón UnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly TiuiDBContext _context;
        private bool _disposed;
        private IDbContextTransaction _transaction;
        private Guid _keyWork;

        /// <summary>
        /// Inicializa la instancia de la clase
        /// </summary>
        /// <param name="context">Contexto de datos</param>
        /// <param name="keyWork">Clave para determinar si es la misma instancia</param>
        public UnitOfWork(TiuiDBContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Crea una transacción
        /// </summary>
        /// <param name="key">Clave de la transacción</param>
        /// <returns>Task</returns>
        public async Task BeginTransaction(Guid key)
        {
            if (this._transaction == null)
            {
                this._transaction = await this._context.Database.BeginTransactionAsync();
                _keyWork = key;
            }
        }
        /// <summary>
        /// Realiza el commit de la transacción
        /// </summary>
        /// <param name="key">Clave de la transacción</param>
        /// <returns>Task con True si la transacción se realizó con éxito</returns>
        public async Task<bool> Commit(Guid key)
        {
            if (_keyWork != key || this._transaction == null)
                return false;
            await this._transaction.CommitAsync();
            this._keyWork = Guid.Empty;
            return true;
        }
        /// <summary>
        /// Elimina la instancia de la clase
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }
        /// <summary>
        /// Libera recursos
        /// </summary>
        /// <param name="disposing">Indica si se debe realizar el dispose</param>
        public virtual void Dispose(bool disposing)
        {
            if (this._disposed)
                return;
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                }
            }
            this._disposed = true;
        }
        /// <summary>
        /// Crear una instancia generica de un repositorio
        /// </summary>
        /// <typeparam name="TEntity">Entidad a partir de la cual se creara el repositorio</typeparam>
        /// <returns>IRepository generico</returns>
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return Activator.CreateInstance(typeof(Repository<>).MakeGenericType(typeof(TEntity)), this._context) as IRepository<TEntity>;
        }
        /// <summary>
        /// Cancela la transacción y revierte los cambios
        /// </summary>
        /// <param name="key">Clave de la transacción</param>
        /// <returns>Task</returns>
        public async Task Rollback(Guid key)
        {
            if (_keyWork == key && this._transaction != null)
            {
                await this._transaction.RollbackAsync();
                this._keyWork = Guid.Empty;
            }
        }
        /// <summary>
        /// Aplica los cambios a la colección del contexto de datos
        /// </summary>
        /// <returns>El número de afectaciones realizadas a la base de datos</returns>
        public async Task<int> SaveChanges()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}
