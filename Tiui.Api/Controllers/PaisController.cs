using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiui.Application.DTOs.Comun;
using Tiui.Application.Services.Comun;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tiui.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly ILocalizacionService _localizacionService;

        public PaisController(ILocalizacionService localizacionService)
        {
            this._localizacionService = localizacionService;
        }
        // GET: api/<PaisController>
        [HttpGet]
        public async Task<ActionResult<List<PaisDTO>>> Get()
        {
            return await this._localizacionService.GetPaises();
        }
    }
}
