using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiui.Application.DTOs.Security;
using Tiui.Application.Services.Seguidad;

namespace Tiui.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountAdminController : ControllerBase
    {
        private readonly IUsuarioAdminService _usuarioService;

        public AccountAdminController(IUsuarioAdminService usuarioService)
        {
            this._usuarioService = usuarioService;
        }
        [HttpPost, Route("login")]
        public async Task<ActionResult<AuthenticatedAdminUserDTO>> PostLogin(LoginDTO loginDTO)
        {
            return await this._usuarioService.LoginTiui(loginDTO);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(AuthenticatedAdminUserDTO), StatusCodes.Status401Unauthorized)]
        [HttpPost, Route("registrar")]
        public async Task<ActionResult<AuthenticatedAdminUserDTO>> Post(UsuarioTiuiAmigoCreateDTO usuarioTiuiAmigoDTO)
        {
            return await this._usuarioService.RegistrarUsuarioTiuiAmigo(usuarioTiuiAmigoDTO);
        }

    }
}
