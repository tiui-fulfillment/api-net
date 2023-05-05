namespace Tiui.Application.DTOs.Guias
{
    /// <summary>
    /// DTO para el manejo de las paqueterias
    /// </summary>
    public class PaqueteriaDTO
    {
        public int? PaqueteriaId { get; set; }
        public string Nombre { get; set; }
        public decimal CostoEnvio { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
