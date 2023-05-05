namespace Tiui.Application.DTOs.Comun
{
    /// <summary>
    /// DTO para el manejo de la configuración de la aplicación
    /// </summary>
    public class ConfiguracionAppDTO
    {
        public string CorreoElectronicoAdministrativo { get; set; }
        public decimal IVA { get; set; }
        public decimal SeguroMercancia { get; set; }
        public decimal ComisionCobroContraEntrega { get; set; }
    }
}
