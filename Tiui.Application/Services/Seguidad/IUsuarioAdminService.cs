using Tiui.Application.DTOs.Security;

namespace Tiui.Application.Services.Seguidad
{
    public interface IUsuarioAdminService
    {
        Task<AuthenticatedAdminUserDTO> RegistrarUsuarioTiuiAmigo(UsuarioTiuiAmigoCreateDTO usuarioTiuiAmigo);
        Task<AuthenticatedAdminUserDTO> LoginTiui(LoginDTO loginDTO);
    }
}
