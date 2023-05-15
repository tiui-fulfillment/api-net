namespace Tiui.Application.DTOs.Guias
{
    /// <summary>
    /// DTO para el manejo de la Guia
    /// </summary>
    public class GuiaCreateDTO
    {
        public long? GuiaId { get; set; }
        public string Folio { get; set; }
        public bool EsPagoContraEntrega { get; set; }
        public decimal ImporteContraEntrega { get; set; }
        public bool TieneSeguroMercancia { get; set; }
        public decimal ImporteSeguroMercancia { get; set; }
        public decimal ImporteCalculoSeguro { get; set; }
        public decimal ImportePaqueteria { get; set; }
        public decimal CostoOperativo { get; set; }
        public decimal SubTotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }     
        public RemitenteDTO Remitente { get; set; }
        public DestinatarioDTO Destinatario { get; set; }
        public PaqueteGuiaDTO Paquete { get; set; }       
        public int? TiuiAmigoId { get; set; }     
        public int? PaqueteriaId { get; set; }
        public bool RegistrarRemitente { get; set; }
        public bool RegistrarDestinatario { get; set; }
        public  bool RegistrarConfiguracionPaquete { get; set; }
        public decimal CobroContraEntrega { get; set; }
        public int CantidadPaquetes { get; set; }
        public string NombreProducto { get; set; }
        public bool? EsDevolucion { get; set; }
        public string? FolioDevolucion { get; set; }
        public bool? RequiereVerificacion { get; set; }
    }
}
