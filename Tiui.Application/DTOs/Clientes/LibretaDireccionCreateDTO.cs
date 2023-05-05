namespace Tiui.Application.DTOs.Clientes
{
    /// <summary>
    /// DTO para el manejo de las direcciones de la libreta
    /// </summary>
    public class LibretaDireccionCreateDTO
    {
        public string Nombre { get; set; }
        public string Empresa { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public int? TiuiAmigoId { get; set; }       
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Referencias { get; set; }
        public bool Activo { get; set; }      
        public int? MunicipioId { get; set; }
    }
}
