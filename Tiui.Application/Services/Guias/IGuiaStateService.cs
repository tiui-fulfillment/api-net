using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Guias;

namespace Tiui.Application.Services.Guias
{
    /// <summary>
    /// Abstracción para el servicio que maneja el cambio de los estatus de la guía
    /// </summary>
    public interface IGuiaStateService
    {
        public Task<ApiResultModel<GuiaUpdateStateDTO>> SetState(GuiaUpdateStateDTO guiaStateDTO);
        public Task<ApiResultModel<GuiaUpdateStateDTO>> SetStateMasive(GuiaStateChangeMasiveDTO guiaStateMasiveDTO);
    }
}
