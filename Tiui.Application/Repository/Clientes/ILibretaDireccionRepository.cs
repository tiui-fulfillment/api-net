using Tiui.Entities.Comun;

namespace Tiui.Application.Repository.Clientes
{
    /// <summary>
    /// Abstracción para la libreta de direcciones
    /// </summary>
    public interface ILibretaDireccionRepository : ICRUDRepository<LibretaDireccion>
    {
        public Task<List<LibretaDireccion>> GetByTiuiAmigo(int? tiuiAmigoId, string filtro);
    }
}
