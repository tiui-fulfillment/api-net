using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Guias;

namespace Tiui.Application.Services.Guias
{
    /// <summary>
    /// Abstracción para el servicio de notificaciones de clientes
    /// </summary>
    public interface INotificacionClienteService
    {
        public Task<ApiResultModel<NotificacionClienteCreateDTO>> Create(NotificacionClienteCreateDTO notificacionClienteDTO);
    }
}
