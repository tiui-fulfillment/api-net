using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.DTOs.Paging;

namespace Tiui.Application.Services.Comun
{
    /// <summary>
    /// Abstracción para el manejo de las Guías
    /// </summary>
    public interface IGraphQLService
    {
        public Task<ApiResultModel<GuiaCreateDTO>> Create(GuiaCreateDTO guiaCreateDTO);
        public Task<GuiaDetailDTO> GetGuia(string guiaId);
        public Task<GuiaTrackingPagedListDTO> GetWithFilterAndPaging(GuiaFilterDTO filtroDTO);
        public Task<GuiaTrackingPagedListDTO> GetWithFilterAndPagingTiuiAmigo(GuiaFilterDTO filtroDTO);
        
    }
}
