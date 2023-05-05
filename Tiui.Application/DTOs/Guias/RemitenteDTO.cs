namespace Tiui.Application.DTOs.Guias
{
    /// <summary>
    /// DTO para el manejo del remitente
    /// </summary>
    public class RemitenteDTO
    {       
        public string Nombre { get; set; }
        public string Empresa { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Calle { get; set; }
        public string Cruzamiento { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Referencias { get; set; }
        public int? MunicipioId { get; set; }
        public int? TiuiAmigoId { get; set; }
    }
}
