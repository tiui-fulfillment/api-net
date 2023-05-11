namespace Tiui.Entities.Guias
{
    /// <summary>
    /// Entidad para el manejo de las paqueterias
    /// </summary>
    public class Paqueteria
    {
        public int? PaqueteriaId { get; set; }
        public string Nombre { get; set; }
        public decimal CostoEnvio { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public int MaximoDiasDeEntrega { get; set; }
    }
}
