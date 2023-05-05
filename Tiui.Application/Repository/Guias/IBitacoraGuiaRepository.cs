using Tiui.Application.DTOs.Guias;
using Tiui.Entities.Guias;

namespace Tiui.Application.Repository.Guias
{
    public interface IBitacoraGuiaRepository : ICRUDRepository<BitacoraGuia>
    {
        public Task<List<BitacoraGuiaDTO>> GetStateChangeGuia(long? guiaId);
    }
}
