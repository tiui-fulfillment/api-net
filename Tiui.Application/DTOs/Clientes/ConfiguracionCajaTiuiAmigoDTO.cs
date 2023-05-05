namespace Tiui.Application.DTOs.Clientes
{
    /// <summary>
    /// DTO para la configuración de cajas del tiui amigo
    /// </summary>
    public class ConfiguracionCajaTiuiAmigoDTO
    {
        public int? ConfiguracionCajaTiuiAmigoId { get; set; }
        public float Largo { get; set; }
        public float Alto { get; set; }
        public float Ancho { get; set; }
        public float Peso { get; set; }
        public int? TiuiAmigoId { get; set; }
        public string NombreConfiguracion { get; set; }
    }
}
