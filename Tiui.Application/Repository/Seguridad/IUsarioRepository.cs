using Tiui.Entities.Comun;
using Tiui.Entities.Seguridad;

namespace Tiui.Application.Repository.Seguridad
{
    /// <summary>
    /// Abstracción para el repositorio de usuarios
    /// </summary>
    public interface IUsarioRepository : ICRUDRepository<Usuario>
    {
        Task<Usuario> Create(Usuario usuario, TiuiAmigo tiuiAmigo);
        Task<bool> Exists(string correoElectronico);
        Task<Usuario> CreateAdmin(Usuario usuario);
    }
}
