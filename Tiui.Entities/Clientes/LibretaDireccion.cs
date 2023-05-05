using Tiui.Entities.Comun;

namespace Tiui.Entities.Comun
{
    /// <summary>
    /// Entidad para el manejo de la libreta de direcciones
    /// </summary>
    public class LibretaDireccion : Direccion
    {
        public string Nombre { get; set; }
        public string Empresa { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public TiuiAmigo TiuiAmigo { get; set; }
        public int? TiuiAmigoId { get; set; }
    }
}
