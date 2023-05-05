using Tiui.Application.DTOs.Guias;

namespace Tiui.Application.Services.Guias
{
    /// <summary>
    /// Abstracción para el manejo de las paqueterias
    /// </summary>
    public interface IPaqueteriaService
    {
        public Task<List<PaqueteriaDTO>> GetAll();
    }
}
