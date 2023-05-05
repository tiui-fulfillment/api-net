namespace Tiui.Application.DTOs.Comun
{
    /// <summary>
    /// DTO para el manejo de los estados
    /// </summary>
    public class EstadoDTO
    {
        public int? EstadoId { get; set; }
        public string Nombre { get; set; }
        public PaisDTO Pais { get; set; }
        public int? PaisId { get; set; }
    }
}
