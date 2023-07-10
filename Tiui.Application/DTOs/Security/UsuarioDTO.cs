using Tiui.Entities.Seguridad;

namespace Tiui.Application.DTOs.Security
{
    /// <summary>
    /// DTO para el manejo de la información de los usuarios
    /// </summary>
    public class UsuarioDTO
    {
        public int? UsuarioId { get; set; }
        public string NombreUsuario { get; set; }                
        public string NombreCompleto { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }
        public ETipoUsuario TipoUsuario { get; set; }
        public int? TiuiAmigoId { get; set; }
    }
}
