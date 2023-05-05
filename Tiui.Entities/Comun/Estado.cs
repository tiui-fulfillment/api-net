namespace Tiui.Entities.Comun
{
    /// <summary>
    /// Entidad para el manejo de los estados
    /// </summary>
    public class Estado
    {
        public int? EstadoId { get; set; }
        public string Nombre { get; set; }
        public Pais Pais { get; set; }
        public int? PaisId { get; set; }
    }
}
