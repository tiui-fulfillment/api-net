namespace Tiui.Application.DTOs.Guias
{
    /// <summary>
    /// DTO para la presentación del detalle de la guia
    /// </summary>
    public class GuiaDetailDTO
    {
        public long? GuiaId { get; set; }
        public string Folio { get; set; }
        public string NombreRemitente { get; set; }
        public string CiudadOrigen { get; set; }
        public string CiudadDestino { get; set; }
        public string Empresa { get; set; }
        public long? EstatusId { get; set; }
        public long? TiuiAmigoId { get; set; }
        public DateTime FechaEstimadaEntrega { get; set; }
        public NotificacionClienteDTO NotificacionCliente { get; set; }
        public List<BitacoraGuiaDTO> Movimientos { get; set; }
        public List<string> Procesos { get; set; }
    }
}
