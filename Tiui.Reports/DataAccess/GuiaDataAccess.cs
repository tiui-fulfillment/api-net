using System.Linq;
using System.Threading.Tasks;
using Tiui.Application.Repository.Guias;
using Tiui.Entities.Guias;

namespace Tiui.Reports.DataAccess
{
    /// <summary>
    /// Clase para el acceso a datos de la guía
    /// </summary>
    public class GuiaDataAccess
    {
        private readonly IGuiaRepository _guiaRepository;

        public GuiaDataAccess(IGuiaRepository guiaRepository)
        {
            this._guiaRepository = guiaRepository;
        }
        /// <summary>
        /// Obtiene información de la guía 
        /// </summary>
        /// <param name="guiaId">Identificador de la guia a buscar</param>
        /// <returns>Guia encontrada</returns>
        public async Task<Guia> Action(long? guiaId)
        {
            var query = await this._guiaRepository.Query(g => g.GuiaId == guiaId, g => g.Destinatario, g => g.Remitente, g => g.Paquete);
            return query.FirstOrDefault();
        }
    }
}
