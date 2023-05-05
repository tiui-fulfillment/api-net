namespace Tiui.Application.DTOs.Guias
{
    /// <summary>
    /// DTO para las notificaiones de los clientes
    /// </summary>
    public class NotificacionClienteDTO
    {
        public int? NotificacionClienteId { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public bool Activo { get; set; }       
        public long? GuiaId { get; set; }
    }
}
