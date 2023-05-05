using Tiui.Application.Repository.Comun;
using Tiui.Entities.Comun;

namespace Tiui.Data.Repository.Comun
{
    /// <summary>
    /// Repositorio para el manejo de los paises
    /// </summary>
    public class PaisRepository : CRUDRepository<Pais>, IPaisRepository
    {
        public PaisRepository(TiuiDBContext context) : base(context)
        {

        }
    }
}
