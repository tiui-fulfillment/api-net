using System.ComponentModel.DataAnnotations;

public class GuiaInfoSuscription
{
  [Key]
  public int GuiaId { get; set; }
  public string Folio { get; set; }
  public int EstatusId { get; set; }
  public string Estatus { get; set; }
  public DateTime? EstatusFecha { get; set; }
  public int? NovedadId { get; set; }
  public string? NovedadDescripcion { get; set; }
  public string? Comentario { get; set; }
  public List<Evidencia>? Evidencias { get; set; }
  public Boolean EsPagoContraEntrega { get; set; }
  public decimal ImporteContraEntrega { get; set; }
  public Boolean TieneSeguroMercancia { get; set; }
  public decimal ImporteCalculoSeguro { get; set; }
  public decimal ImporteSeguroMercancia { get; set; }
  public decimal ImportePaqueteria { get; set; }
  public decimal CostoOperativo { get; set; }
  public decimal SubTotal { get; set; }
  public decimal IVA { get; set; }
  public decimal Total { get; set; }
  public int PaqueteId { get; set; }
  public int PaqueteriaId { get; set; }
  public decimal CobroContraEntrega { get; set; }
  public int CantidadPaquetes { get; set; }
  public string NombreProducto { get; set; }
  public DateTime? FechaEstimadaEntrega { get; set; }
  public int Consecutivo { get; set; }
  public string TipoProcesoCancelacion { get; set; }
  public string ProcesoCancelacion { get; set; }
  public DateTime? FechaReagendado { get; set; }
  public DateTime? FechaConciliacion { get; set; }
  public Boolean? EsDevolucion { get; set; }

  public string? FolioDevolucion { get; set; }
  public Boolean? RequiereVerificacion { get; set; }
  public DireccionesGuia Remitente { get; set; }
  public DireccionesGuia Destinatario { get; set; }

  public int IntentosDeEntrega { get; set; }
  public int IntentosRecoleccion { get; set; }
}

public class GuiaInfoSuscriptionDTO
{
  [Key]
  public int GuiaId { get; }
  public string Folio { get; set; }
  public int EstatusId { get; set; }
  public string Estatus { get; set; }
  public DateTime? EstatusFecha { get; set; }
  public int? NovedadId { get; set; }
  public string? NovedadDescripcion { get; set; }
  public string? Comentario { get; set; }
  public string[]? Evidencias { get; set; }
  public Boolean EsPagoContraEntrega { get; set; }
  public decimal ImporteContraEntrega { get; set; }
  public Boolean TieneSeguroMercancia { get; set; }
  public decimal ImporteCalculoSeguro { get; set; }
  public decimal ImporteSeguroMercancia { get; set; }
  public decimal ImportePaqueteria { get; set; }
  public decimal CostoOperativo { get; set; }
  public decimal SubTotal { get; set; }
  public decimal IVA { get; set; }
  public decimal Total { get; set; }
  public int PaqueteId { get; set; }
  public int PaqueteriaId { get; set; }
  public decimal CobroContraEntrega { get; set; }
  public int CantidadPaquetes { get; set; }
  public string NombreProducto { get; set; }
  public DateTime? FechaEstimadaEntrega { get; set; }
  public int Consecutivo { get; set; }
  public string TipoProcesoCancelacion { get; set; }
  public string ProcesoCancelacion { get; set; }
  public DateTime? FechaReagendado { get; set; }
  public DateTime? FechaConciliacion { get; set; }
  public Boolean? EsDevolucion { get; set; }

  public string? FolioDevolucion { get; set; }
  public Boolean? RequiereVerificacion { get; set; }
  public string Remitente { get; set; }
  public string Destinatario { get; set; }

  public int IntentosDeEntrega { get; set; }
  public int IntentosRecoleccion { get; set; }
}

public class Evidencia
{
  [Key]
  public string id { get; }
  public string text { get; set; }
  public string url { get; set; }
  public int? GuiaId { get; set; }
  public string mimeType { get; set; }
}

public class DireccionesGuia
{
  [Key]
  public int DireccionGuiaId { get; }
  public string Nombre { get; set; }
  public string Empresa { get; set; }
  public string Telefono { get; set; }
  public string CorreoElectronico { get; set; }
  public string CodigoPostal { get; set; }
  public string Pais { get; set; }
  public string Estado { get; set; }
  public string Ciudad { get; set; }
  public string Colonia { get; set; }
  public string Calle { get; set; }
  public string Cruzamiento { get; set; }
  public string Numero { get; set; }
  public string Referencias { get; set; }
}
