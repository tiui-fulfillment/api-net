using Microsoft.AspNetCore.Mvc;
using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Security;
using Tiui.Application.Services.Seguidad;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tiui.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTiuiAmigoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AccountTiuiAmigoController(IUsuarioService usuarioService)
        {
            this._usuarioService = usuarioService;
        }
        // POST api/<AccountTiuiAmigoController>
        [HttpPost, Route("login")]
        public async Task<ActionResult<AuthenticatedUserDTO>> PostLogin(LoginDTO loginDTO)
        {
            return await this._usuarioService.LoginTiui(loginDTO);
        }

        // POST api/<AccountTiuiAmigoController>
        [HttpPost, Route("registrar")]
        public async Task<ActionResult<AuthenticatedUserDTO>> Post(UsuarioTiuiAmigoCreateDTO usuarioTiuiAmigoDTO)
        {
            return await this._usuarioService.RegistrarUsuarioTiuiAmigo(usuarioTiuiAmigoDTO);
        }

        // POST api/<AccountTiuiAmigoController>
        [HttpPost, Route("code-password")]
        public async Task<ActionResult<ApiResultModel<UsuarioDTO>>> PostcodePassword(UserRecoveryPasswordDTO loginDTO)
        {
            return await this._usuarioService.GeneratePasswordRecoveryCode(loginDTO);
        }
        // POST api/<AccountTiuiAmigoController>
        [HttpPost, Route("validate-code")]
        public async Task<ActionResult<ApiResultModel<UsuarioDTO>>> PostValidateCode(UserRecoveryPasswordDTO code)
        {
            return await this._usuarioService.ValidatePasswordRecoveryCode(code);
        }
        // POST api/<AccountTiuiAmigoController>
        [HttpPut("update-password/{id}")]
        public async Task<ActionResult<ApiResultModel<UsuarioDTO>>> PutPassword(int id, UserRecoveryPasswordDTO loginDTO)
        {
            loginDTO.UserId = id;
            return await this._usuarioService.PasswordChange(loginDTO);
        }
        // POST api/<AccountTiuiAmigoController>
        [HttpPost, Route("refresh-token")]
        public async Task<ActionResult<AuthenticatedUserDTO>> PostRefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            return await this._usuarioService.RefreshToken(refreshTokenDTO);
        }
    }
}
