using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiui.Application.DTOs.Clientes;
using Tiui.Application.Services.Clientes;

namespace Tiui.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TiuiAmigoController : ControllerBase
    {
        private readonly ITiuiAmigoService _tiuiAmigoService;

        public TiuiAmigoController(ITiuiAmigoService tiuiAmigoService)
        {
            this._tiuiAmigoService = tiuiAmigoService;
        }
        [HttpGet]
        public async Task<ActionResult<List<TiuiAmigoSelectDTO>>> Get() => await this._tiuiAmigoService.GetAll();
    }
}
