namespace Tiui.Entities.Comun
{
    /// <summary>
    /// Entidad para el manejo de los municipios
    /// </summary>
    public class Municipio
    {
        public int? MunicipioId { get; set; }
        public string Nombre { get; set; }
        public Estado Estado { get; set; }
        public int? EstadoId { get; set; }
    }
}
