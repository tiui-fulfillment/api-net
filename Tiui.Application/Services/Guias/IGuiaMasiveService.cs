using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Guias;

namespace Tiui.Application.Services.Guias
{
    public interface IGuiaMasiveService
    {
        public Task<ApiResultModel<List<GuiaCreateDTO>>> Create(GuiaMasiveDTO guiaMasiveDTO);
        public Task<byte[]> CreateZip(List<GuiaCreateDTO> guiaMasiveDTO);
    }
}
