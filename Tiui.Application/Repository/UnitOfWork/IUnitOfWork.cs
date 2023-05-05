using System.Data;

namespace Tiui.Application.Repository.UnitOfWork
{
    /// <summary>
    /// Intefaz para el manejo del patron IUnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChanges();
        void Dispose(bool disposing);
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task BeginTransaction(Guid key);
        Task<bool> Commit(Guid key);
        Task Rollback(Guid key);
    }
}
