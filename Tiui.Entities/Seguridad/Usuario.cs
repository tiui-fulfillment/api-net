using System;

namespace Tiui.Entities.Seguridad
{
    /// <summary>
    /// Entidad para el manejo de los usuarios
    /// </summary>
    public class Usuario
    {
        public int? UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }
        public ETipoUsuario TipoUsuario { get; set; }
        public int CodigoReestablecerPassword { get; set; }
    }
}
