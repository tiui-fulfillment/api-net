using System.ComponentModel.DataAnnotations;

namespace Tiui.Application.DTOs.Security
{
    /// <summary>
    /// DTO para el registro de usuarios
    /// </summary>
    public class UsuarioTiuiAmigoCreateDTO
    {
        [Required(ErrorMessage ="El Nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Apellido es requerido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "El Correo electrónico es requerido")]
        public string CorreoElectronico { get; set; }
        [Required(ErrorMessage = "El Teléfono es requerido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
        public int TipoProceso { get; set; }
    }
}
