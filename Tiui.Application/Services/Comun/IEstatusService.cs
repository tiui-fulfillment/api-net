using Tiui.Application.DTOs.Comun;
using Tiui.Entities.Guias;

namespace Tiui.Application.Services.Comun
{
    public interface IEstatusService
    {
        public Task<List<EstatusDTO>> GetAll();
        public Task<List<EstatusDTO>> GetNextStatus(int estatusGuia);
        public Task<List<EstatusDTO>> GetAllByTiuiAmigoId(int tiuiAmigoId);
        
    }
}
