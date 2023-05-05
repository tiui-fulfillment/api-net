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
    public class EstatusController : ControllerBase
    {
        private readonly IEstatusService _estatusService;

        public EstatusController(IEstatusService estatusService)
        {
            this._estatusService = estatusService;
        }
        [HttpGet]
        public async Task<ActionResult<List<EstatusDTO>>> Get() => await this._estatusService.GetAll();
        [HttpGet, Route("tipo-proceso/{tiuiAmigoId}")]
        public async Task<ActionResult<List<EstatusDTO>>> Get(int tiuiAmigoId) => await this._estatusService.GetAllByTiuiAmigoId(tiuiAmigoId);
        [HttpGet("{estatusId}")]
        public async Task<ActionResult<List<EstatusDTO>>> GetNexStatus(int estatusId) => await this._estatusService.GetNextStatus(estatusId);

    }
}
