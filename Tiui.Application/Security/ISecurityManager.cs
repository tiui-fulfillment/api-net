using Tiui.Application.DTOs.Security;

namespace Tiui.Application.Security
{
    /// <summary>
    /// Abstracción para el manejador de seguridad jwt
    /// </summary>
    public interface ISecurityManager
    {
        public AuthenticatedUserDTO BuildAuthenticatedUserObject(UsuarioDTO user, int? tiuiAmigoId, string sessionId = "");
        public AuthenticatedAdminUserDTO BuildAuthenticatedAdminUserObject(UsuarioDTO user);
    }
}
