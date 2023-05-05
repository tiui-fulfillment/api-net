using Tiui.Application.DTOs.Clientes;

namespace Tiui.Application.Services.Clientes
{
    /// <summary>
    /// Abstracción para el manejo de las configuraciones de caja para el tiui amigo
    /// </summary>
    public interface IConfiguracionCajaTiuiAmigoService
    {
        public Task<List<ConfiguracionCajaTiuiAmigoDTO>> GetByTiuiAmigo(int? tiuiAmigoId);
    }
}
