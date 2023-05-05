namespace Tiui.Application.DTOs.Guias
{
    /// <summary>
    /// DTO para el manejo de las notificaciones de clientes
    /// </summary>
    public class NotificacionClienteCreateDTO
    {       
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public bool Activo { get; set; }        
        public long? GuiaId { get; set; }
    }
}
