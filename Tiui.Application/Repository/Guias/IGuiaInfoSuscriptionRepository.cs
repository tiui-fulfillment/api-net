using Tiui.Application.DTOs.Guias;
using Tiui.Entities.Guias;

namespace Tiui.Application.Repository.Guias
{
    /// <summary>
    /// Abstracción para el repositorio de guías
    /// </summary>
    public interface IGuiaInfoSuscriptionRepository : ICRUDRepository<GuiaInfoSuscriptionDTO>
    {
        public Task<GuiaInfoSuscriptionDTO> GetGuiaInfo(string folio);
    }
}
