using System.Linq.Expressions;

namespace Tiui.Application.Repository
{
    /// <summary>
    /// Interfaz generica para el manejo de los CRUD repositories
    /// </summary>
    /// <typeparam name="TEntity">Entidad con la que trabaja el repositorio</typeparam>
    public interface ICRUDRepository<TEntity>
    {
        public void Reload(TEntity entity);
        public TEntity Find(params object[] keyValues);
        public IQueryable<TEntity> GetAllQuery();
        public Task<ICollection<TEntity>> GetAll();
        public Task<ICollection<TEntity>> Query(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
        public Task Insert(IEnumerable<TEntity> entities);
        public Task Insert(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public void Delete(IEnumerable<TEntity> entities);
        public Task Commit();
    }
}
