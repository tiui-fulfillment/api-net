using Microsoft.EntityFrameworkCore;
using Tiui.Application.Repository.Clientes;
using Tiui.Entities.Comun;

namespace Tiui.Data.Repository.Clientes
{
    /// <summary>
    /// Repositorio para la libreta de direcciones
    /// </summary>
    public class LibretaDireccionRepository : CRUDRepository<LibretaDireccion>, ILibretaDireccionRepository
    {
        public LibretaDireccionRepository(TiuiDBContext context) : base(context)
        {

        }
        public async Task<List<LibretaDireccion>> GetByTiuiAmigo(int? tiuiAmigoId, string filtro)
        {
            var query = this._context.LibretaDirecciones.AsQueryable();
            if (tiuiAmigoId.HasValue)
                query = query.Where(l => l.TiuiAmigoId == tiuiAmigoId);
            if (!string.IsNullOrWhiteSpace(filtro))
                query = query.Where(l => l.Nombre.Contains(filtro) || l.Empresa.Contains(filtro)
                || l.Telefono.Contains(filtro) || l.CorreoElectronico.Contains(filtro));
            return await query.Include(d=>d.Municipio).ThenInclude(m=>m.Estado).ToListAsync();
        }
    }
}
