using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.DTOs.Paging;

namespace Tiui.Application.Services.Guias
{
  /// <summary>
  /// Abstracción para el manejo de las Guías
  /// </summary>
  public interface IGuiaService
  {
    public Task<ApiResultModel<GuiaCreateDTO>> Create(GuiaCreateDTO guiaCreateDTO);
    public Task<GuiaDetailDTO> GetGuia(string guiaId);
    public Task<GuiaTrackingPagedListDTO> GetWithFilterAndPaging(GuiaFilterDTO filtroDTO);
    public Task<GuiaTrackingPagedListDTO> GetWithFilterAndPagingTiuiAmigo(GuiaFilterDTO filtroDTO);
    public Task<byte[]> GetPrintFolio(string guiaId);
    public Task<ApiResultModel<GuiaUpdateStateDTO>> SetGuiaCancelationAsync(GuiaUpdateCancelationDTO guiaUpdateCancelationDTO, TokenClaimsDTO tokenClaims);
  }
}
