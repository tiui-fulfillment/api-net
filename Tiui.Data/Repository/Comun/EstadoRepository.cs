using Tiui.Application.Repository.Comun;
using Tiui.Entities.Comun;

namespace Tiui.Data.Repository.Comun
{
    /// <summary>
    /// Repositorio para el manejo de los estados
    /// </summary>
    public class EstadoRepository : CRUDRepository<Estado>, IEstadoRepository
    {
        public EstadoRepository(TiuiDBContext context) : base(context)
        {

        }
    }
}
