using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.Services.Guias;

namespace Tiui.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PaqueteriaController : ControllerBase
    {
        private readonly IPaqueteriaService _paqueteriaService;

        public PaqueteriaController(IPaqueteriaService paqueteriaService)
        {
            this._paqueteriaService = paqueteriaService;
        }
        [HttpGet]
        public async Task<ActionResult<List<PaqueteriaDTO>>> Get()
        {
            return await this._paqueteriaService.GetAll();
        }
    }
}
