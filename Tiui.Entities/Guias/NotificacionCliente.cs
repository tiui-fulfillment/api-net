namespace Tiui.Entities.Guias
{
    /// <summary>
    /// Entidad para noticaciones a los clientes
    /// </summary>
    public class NotificacionCliente
    {
        public int? NotificacionClienteId { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public bool Activo { get; set; }
        public Guia Guia { get; set; }
        public long? GuiaId { get; set; }
    }
}
