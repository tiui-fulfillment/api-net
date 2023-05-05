using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiui.Application.DTOs.Cancelaciones;
using Tiui.Application.Services.Cancelaciones;

namespace Tiui.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MotivoCancelacionController : ControllerBase
    {
        private readonly IMotivoCancelacionService _motivoCancelacionService;

        public MotivoCancelacionController(IMotivoCancelacionService motivoCancelacionService)
        {
            this._motivoCancelacionService = motivoCancelacionService;
        }
        [HttpGet("{tipoCancelacion}")]
        public async Task<List<MotivoCancelacionDTO>> Get(int tipoCancelacion) => await this._motivoCancelacionService.GetMotivosByProceso(tipoCancelacion);
    }
}
