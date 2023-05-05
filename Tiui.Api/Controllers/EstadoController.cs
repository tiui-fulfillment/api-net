using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiui.Application.DTOs.Comun;
using Tiui.Application.Services.Comun;

namespace Tiui.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly ILocalizacionService _localizacionService;

        public EstadoController(ILocalizacionService localizacionService)
        {
            this._localizacionService = localizacionService;
        }
        // GET: api/<EstadoController>
        [HttpGet]
        public async Task<ActionResult<List<EstadoDTO>>> Get()
        {           
            return await this._localizacionService.GetEstados();
        }
    }
}
