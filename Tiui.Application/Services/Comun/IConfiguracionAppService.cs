using Tiui.Application.DTOs.Comun;

namespace Tiui.Application.Services.Comun
{
    /// <summary>
    /// Abstracción para el manejo de la configuración de la aplicación
    /// </summary>
    public interface IConfiguracionAppService
    {
        public Task<ConfiguracionAppDTO> Get();
    }
}
