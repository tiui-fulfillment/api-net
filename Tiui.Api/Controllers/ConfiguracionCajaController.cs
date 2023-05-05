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
    public class ConfiguracionCajaController : ControllerBase
    {
        private readonly IConfiguracionCajaTiuiAmigoService cajaTiuiAmigoService;

        public ConfiguracionCajaController(IConfiguracionCajaTiuiAmigoService cajaTiuiAmigoService)
        {
            this.cajaTiuiAmigoService = cajaTiuiAmigoService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ConfiguracionCajaTiuiAmigoDTO>>> Get(int id)
        {               
            return await this.cajaTiuiAmigoService.GetByTiuiAmigo(id);
        }
    }
}
