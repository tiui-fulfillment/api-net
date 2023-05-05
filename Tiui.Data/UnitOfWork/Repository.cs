using Tiui.Application.Repository.UnitOfWork;
using Tiui.Data.Repository;

namespace Tiui.Data.UnitOfWork
{
    /// <summary>
    /// Repositorio generico para el patron UnitOfWork
    /// </summary>
    public class Repository<T> : CRUDRepository<T>, IRepository<T> where T : class
    {
        public Repository(TiuiDBContext context) : base(context)
        {

        }
    }
}
