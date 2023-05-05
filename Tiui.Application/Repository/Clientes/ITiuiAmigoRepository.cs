using Tiui.Entities.Comun;

namespace Tiui.Application.Repository.Clientes
{
    /// <summary>
    /// Interfaz para el repositorio de los amigos Tiui
    /// </summary>
    public interface ITiuiAmigoRepository : ICRUDRepository<TiuiAmigo>
    {
        public Task<string> GetSequence();
    }
}
