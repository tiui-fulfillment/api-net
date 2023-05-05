using Tiui.Application.DTOs.Clientes;
using Tiui.Entities.Comun;

namespace Tiui.Application.Services.Clientes
{
    /// <summary>
    /// Abstracción para el manejo de las direcciones
    /// </summary>
    public interface IDireccionService
    {
        public Task<LibretaDireccionDTO> Create(LibretaDireccionCreateDTO libretaDireccionCreateDTO);
        public Task<List<LibretaDireccionDTO>> GetByTiuiAmigo(LibretaDireccionFiltroDTO filtro);
    }
}
