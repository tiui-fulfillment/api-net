namespace Tiui.Entities.Comun
{
    /// <summary>
    /// Permite configurar parámetros generales para la aplicación
    /// </summary>
    public class ConfiguracionApp
    {
        public int? ConfiguracionAppId { get; set; }
        public string CorreoElectronicoAdministrativo { get; set; }
        public decimal IVA { get; set; }
        public decimal SeguroMercancia { get; set; }
        public decimal ComisionCobroContraEntrega { get; set; }

    }
}
