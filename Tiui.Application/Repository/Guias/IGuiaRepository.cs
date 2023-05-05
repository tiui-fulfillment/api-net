using Tiui.Application.DTOs.Guias;
using Tiui.Entities.Guias;

namespace Tiui.Application.Repository.Guias
{
    /// <summary>
    /// Abstracción para el repositorio de guías
    /// </summary>
    public interface IGuiaRepository : ICRUDRepository<Guia>
    {
        public Task<Guia> Create(Guia guia);
        public Task<Guia> GetLastGuia(int tiuiAmigoId);
        public Task<List<Guia>> GetGuiaWithFilter(GuiaFilterDTO guiaFilter);
        public Task<List<Guia>> GetGuiaWithFilterAndPaging(GuiaFilterDTO guiaFilter);
    }
}
