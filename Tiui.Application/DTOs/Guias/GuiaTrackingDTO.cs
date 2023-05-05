namespace Tiui.Application.DTOs.Guias
{
    /// <summary>
    /// DTO para el manejo del tracking de la guía
    /// </summary>
    public class GuiaTrackingDTO
    {
        public long? GuiaId { get; set; }
        public string Folio { get; set; }
        public string Destinatario { get; set; }     
        public string Estatus { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaReagendado { get; set; }
        public int? EstatusId { get; set; }
    }
}
