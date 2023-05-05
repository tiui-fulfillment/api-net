using Tiui.Application.Repository.Guias;
using Tiui.Entities.Guias;

namespace Tiui.Data.Repository.Guias
{
    /// <summary>
    /// Repositorio para el manejo de las paqueterias
    /// </summary>
    public class PaqueteriaRepository : CRUDRepository<Paqueteria>, IPaqueteriaRepository
    {
        public PaqueteriaRepository(TiuiDBContext context) : base(context)
        {

        }
    }
}
