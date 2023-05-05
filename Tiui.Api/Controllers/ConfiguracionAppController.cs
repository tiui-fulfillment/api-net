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
    public class ConfiguracionAppController : ControllerBase
    {
        private readonly IConfiguracionAppService _configuracionAppService;

        public ConfiguracionAppController(IConfiguracionAppService configuracionAppService)
        {
            this._configuracionAppService = configuracionAppService;
        }
        [HttpGet]
        public async Task<ConfiguracionAppDTO> Get()
        {
            return await this._configuracionAppService.Get();
        }
    }
}
