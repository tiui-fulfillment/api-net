using System;

namespace Tiui.Entities.Guias
{
  /// <summary>
  /// Entidad para el manejo de destinatario y remitente de la guía
  /// </summary>
  public class DireccionGuia
  {
    public long? DireccionGuiaId { get; set; }
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
    public string Discriminator { get; set; }
    public long? GuiaId { get; set; }
    public long? Remitente_GuiaId { get; set; }
    public decimal Latitud { get; set; }
    public decimal Longitud { get; set; }
    public bool google_validate { get; set; }
    public bool google_error { get; set; }
    public bool? edited { get; set; }
    public string act_user { get; set; }
    public DateTime? act_date { get; set; }
    public char act_type { get; set; }
  }
}
