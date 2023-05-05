namespace Tiui.Application.DTOs.Reports
{
    public class GuiaReportFilterDTO
    {
        public DateTime? FechaRegistroInicio { get; set; }
        public DateTime? FechaRegistroFin { get; set; }
        public int? tiuiAmigoId { get; set; }
        public int? EstatusId { get; set; }
    }
}
