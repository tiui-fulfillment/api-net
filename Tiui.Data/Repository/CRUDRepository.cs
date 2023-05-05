using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Tiui.Data.Repository
{

    /// <summary>
    /// Clase base para repositorios
    /// </summary>
    /// <typeparam name="TEntity">Entidad con la que se va a trabajar</typeparam>
    public class CRUDRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Contexto de datos
        /// </summary>        
        protected TiuiDBContext _context;


        /// <summary>
        /// Entities
        /// </summary>
        private readonly DbSet<TEntity> _entities;

        /// <summary>
        /// Crea una instancia del repositorio base
        /// </summary>
        /// <param name="context">Contexto con el que se va a trabajar</param>
        public CRUDRepository(DbContext context)
        {
            if (context == null)
                throw new Exception("El contexto no puede se nulo: BaseRepository");         
            _context = context as TiuiDBContext;
            if (_context == null) throw new Exception("El contexto no es valido: BaseRepository");
            _entities = _context.Set<TEntity>();            
        }
        /// <summary>
        /// Recarga las entradas desde la base de datos 
        /// </summary>
        /// <param name="entity">Entidad con la que se esta trabajando</param>
        public virtual void Reload(TEntity entity)
        {
            (_context as DbContext).Entry(entity).Reload();
        }
        /// <summary>
        /// Filtra las entidades en base a keyvalues
        /// </summary>
        /// <param name="keyValues">filtros a aplicar al método find</param>
        /// <returns>Lista de entidades filtradas</returns>
        public TEntity Find(params object[] keyValues) => _entities.Find(keyValues);
        /// <summary>
        /// Obtiene un objeto IQueriable (Permite seguir consultando) de las entidades
        /// </summary>
        /// <returns>Retorna IQueriable de la entidad</returns>
        public IQueryable<TEntity> GetAllQuery() => _entities;
        /// <summary>
        /// Obtiene todas los registros de la entidad con la que se esta trabajando
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<TEntity>> GetAll() => await _entities.ToListAsync();


        #region Custom Queries       
        public async Task<ICollection<TEntity>> Query(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _entities.OfType<TEntity>().AsNoTracking();
            if (includes != null && includes.Any())
                query = includes.Aggregate(query, (current, expression) => current.Include(expression));
            if (where != null)
                query = query.Where(where).OfType<TEntity>();

            return await query.ToListAsync();
        }
        #endregion
        /// <summary>
        /// Registra varias entidades
        /// </summary>
        /// <param name="entities">Entidades a registrar</param>
        public async Task Insert(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                await Insert(entity);
            }
        }
        /// <summary>
        /// Registra una entidad
        /// </summary>
        /// <param name="entity">Entidad a registrar</param>
        public async Task Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }
            await _entities.AddAsync(entity);
        }
        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        public void Update(TEntity entity)
        {
            if (!_entities.Local.Any(e => e == entity))
                _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        public void Delete(TEntity entity)
        {
            if (!_entities.Local.Any(e => e == entity))
                _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
        }
        /// <summary>
        /// Elimina varias entidades
        /// </summary>
        /// <param name="entities">Entidades a eliminar</param>
        public void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }
        /// <summary>
        /// Metodo para aplicar todos los cambios a la bd
        /// </summary>
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
