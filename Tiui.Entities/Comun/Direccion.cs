namespace Tiui.Entities.Comun
{
    /// <summary>
    /// Entidad para el manejo de las direcciones
    /// </summary>
    public class Direccion
    {
        public long? DireccionId { get; set; }
        public string Calle { get; set; }
        public string Cruzamiento { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Referencias { get; set; }
        public bool Activo { get; set; }
        public Municipio Municipio { get; set; }
        public int? MunicipioId { get; set; }
    }

}
