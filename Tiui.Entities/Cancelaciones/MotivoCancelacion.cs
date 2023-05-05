namespace Tiui.Entities.Cancelaciones
{
    /// <summary>
    /// Clase para el manejo de los tipos de cancelación
    /// </summary>
    public class MotivoCancelacion
    {
        public int MotivoCancelacionId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public bool Activo { get; set; }
        public ETipoCancelacion TipoCancelacion { get; set; }
    }
}
