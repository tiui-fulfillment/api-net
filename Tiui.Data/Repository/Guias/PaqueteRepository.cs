using Tiui.Application.Repository.Guias;
using Tiui.Entities.Guias;

namespace Tiui.Data.Repository.Guias
{
    /// <summary>
    /// Repositorio para el manejo de los paquetes
    /// </summary>
    public class PaqueteRepository : CRUDRepository<Paquete>, IPaqueteRepository
    {
        public PaqueteRepository(TiuiDBContext context) : base(context)
        {

        }
    }
}
