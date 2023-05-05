namespace Tiui.Application.DTOs.Comun
{
    /// <summary>
    /// DTO para el manejo de los municipios
    /// </summary>
    public class MunicipioDTO
    {
        public int? MunicipioId { get; set; }
        public string Nombre { get; set; }
        public EstadoDTO Estado { get; set; }
        public int? EstadoId { get; set; }
    }
}
