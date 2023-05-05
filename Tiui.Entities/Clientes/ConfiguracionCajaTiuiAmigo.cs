namespace Tiui.Entities.Comun
{
    /// <summary>
    /// Entidad para el manejo de las configuraciones de cajas
    /// </summary>
    public class ConfiguracionCajaTiuiAmigo
    {
        public int? ConfiguracionCajaTiuiAmigoId { get; set; }
        public float Largo { get; set; }
        public float Alto { get; set; }
        public float Ancho { get; set; }
        public float Peso { get; set; }
        public TiuiAmigo TiuiAmigo { get; set; }
        public int? TiuiAmigoId { get; set; }
    }
}
