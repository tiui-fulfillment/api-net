using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Security;

namespace Tiui.Application.Services.Seguidad
{
    /// <summary>
    /// Abstracción para el manejo de servicio de usuarios
    /// </summary>
    public interface IUsuarioService
    {
        Task<AuthenticatedUserDTO> RegistrarUsuarioTiuiAmigo(UsuarioTiuiAmigoCreateDTO usuarioTiuiAmigo);
        Task<AuthenticatedUserDTO> LoginTiui(LoginDTO loginDTO);
        Task<ApiResultModel<UsuarioDTO>> GeneratePasswordRecoveryCode(UserRecoveryPasswordDTO loginDTO);
        Task<ApiResultModel<UsuarioDTO>> ValidatePasswordRecoveryCode(UserRecoveryPasswordDTO loginDTO);
        Task<ApiResultModel<UsuarioDTO>> PasswordChange(UserRecoveryPasswordDTO loginDTO);
        Task<AuthenticatedUserDTO> RefreshToken(RefreshTokenDTO refreshToken);
    }
}
