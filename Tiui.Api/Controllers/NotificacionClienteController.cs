using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.Services.Guias;

namespace Tiui.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionClienteController : ControllerBase
    {
        private readonly INotificacionClienteService _notificacionClienteService;

        public NotificacionClienteController(INotificacionClienteService notificacionClienteService)
        {
            this._notificacionClienteService = notificacionClienteService;
        }
        [HttpPost]
        public async Task<ApiResultModel<NotificacionClienteCreateDTO>> Post(NotificacionClienteCreateDTO notificacionClienteCreateDTO)
        {
            return await this._notificacionClienteService.Create(notificacionClienteCreateDTO);
        }
    }
}
