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
    public class MunicipioController : ControllerBase
    {
        private readonly ILocalizacionService _localizacionService;

        public MunicipioController(ILocalizacionService localizacionService)
        {
            this._localizacionService = localizacionService;
        }
        // GET: api/<MunicipioController>
        [HttpGet("{estadoId}")]
        public async Task<ActionResult<List<MunicipioDTO>>> Get(int estadoId)
        {            
            return await this._localizacionService.GetMunicipios(estadoId);
        }
    }
}
