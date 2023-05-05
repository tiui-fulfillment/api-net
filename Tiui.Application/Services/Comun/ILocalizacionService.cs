using Tiui.Application.DTOs.Comun;

namespace Tiui.Application.Services.Comun
{
    /// <summary>
    /// Abstracción para el manejo de los estados, paises y municipios
    /// </summary>
    public interface ILocalizacionService
    {
        public Task<List<PaisDTO>> GetPaises();
        public Task<List<EstadoDTO>> GetEstados();
        public Task<List<MunicipioDTO>> GetMunicipios(int? estadoId);

    }
}
