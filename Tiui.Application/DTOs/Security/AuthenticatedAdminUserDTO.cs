namespace Tiui.Application.DTOs.Security
{
    /// <summary>
    /// DTO para autenticación de usuarios
    /// </summary>
    public class AuthenticatedAdminUserDTO
    {
        public UsuarioDTO User { get; set; }      
        public string AccessToken { get; set; }
        public int ExpireInSeconds { get; set; }
        public string MessageError { get; set; }
        public int? TiuiAmigoId { get; set; }
    }
}
