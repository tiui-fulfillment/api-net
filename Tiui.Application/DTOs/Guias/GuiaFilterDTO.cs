using Tiui.Entities.Guias;
using Tiui.Utils.Paging;

namespace Tiui.Application.DTOs.Guias
{
    /// <summary>
    /// Filtro para la consulta de guias
    /// </summary>
    public class GuiaFilterDTO : PageFilter
    {
        public string Folio { get; set; }
        public EEstatusGuia EstatusId { get; set; }
        public DateTime? FechaRegistroInicio { get; set; }
        public DateTime? FechaRegistroFin { get; set; }
        public int? TiuiAmigoId { get; set; }
    }
}
