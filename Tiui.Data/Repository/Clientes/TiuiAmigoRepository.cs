using Microsoft.EntityFrameworkCore;
using Tiui.Application.Repository.Clientes;
using Tiui.Entities.Comun;

namespace Tiui.Data.Repository.Clientes
{
    /// <summary>
    /// Repositorio para el manejo de los tiui amigos
    /// </summary>
    public class TiuiAmigoRepository : CRUDRepository<TiuiAmigo>, ITiuiAmigoRepository
    {
        /// <summary>
        /// Inicializa la instancia del repositorio
        /// </summary>
        /// <param name="context"></param>
        public TiuiAmigoRepository(TiuiDBContext context) : base(context)
        {
            
        }
        /// <summary>
        /// Obtiene la secuencia del usuario
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetSequence()
        {
            return (await this._context.TiuiAmigos.OrderBy(t => t.TiuiAmigoId).LastOrDefaultAsync())?.Codigo;
        }
    }
}
