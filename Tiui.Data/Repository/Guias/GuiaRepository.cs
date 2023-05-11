using Microsoft.EntityFrameworkCore;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.Repository.Guias;
using Tiui.Entities.Guias;

namespace Tiui.Data.Repository.Guias
{
    /// <summary>
    /// Repositorio para el manejo de las guías
    /// </summary>
    public class GuiaRepository : CRUDRepository<Guia>, IGuiaRepository
    {
        public GuiaRepository(TiuiDBContext context) : base(context)
        {
        }
        /// <summary>
        /// Registra información de la guía con todas sus referencias
        /// </summary>
        /// <param name="guia">Guia con la información a registrar</param>
        /// <returns>Guia creada</returns>
        public async Task<Guia> Create(Guia guia)
        {
            this._context.Guias.Add(guia);
            await this._context.SaveChangesAsync();
            return guia;
        }        
        public async Task<List<Guia>> GetGuiaWithFilter(GuiaFilterDTO guiaFilter)
        {
            var query = this._context.Guias.AsQueryable();
            if (!string.IsNullOrWhiteSpace(guiaFilter.Folio))
                query = query.Where(g => g.Folio.ToLower().Equals(guiaFilter.Folio));
            if (Enum.IsDefined(typeof(EEstatusGuia), guiaFilter.EstatusId))
                query = query.Where(g => g.EstatusId == (int)guiaFilter.EstatusId);
            if (guiaFilter.FechaRegistroInicio.HasValue && guiaFilter.FechaRegistroFin.HasValue)
                query = query.Where(g => g.FechaRegistro.Value.Date >= guiaFilter.FechaRegistroInicio.Value.Date
                && g.FechaRegistro.Value.Date <= guiaFilter.FechaRegistroFin.Value.Date);
            if (guiaFilter.TiuiAmigoId.HasValue)
                query = query.Where(g => g.TiuiAmigoId.Value == guiaFilter.TiuiAmigoId.Value);
            return await query.Include(g => g.Destinatario).Include(g => g.Estatus).AsNoTracking().ToListAsync();

        }
        public async Task<List<Guia>> GetGuiaWithFilterAndPaging(GuiaFilterDTO guiaFilter)
        {
            var query = this._context.Guias.AsQueryable();
            if (!string.IsNullOrWhiteSpace(guiaFilter.Folio))
                query = query.Where(g => g.Folio.ToLower().Equals(guiaFilter.Folio));
            if (Enum.IsDefined(typeof(EEstatusGuia), guiaFilter.EstatusId))
                query = query.Where(g => g.EstatusId == (int)guiaFilter.EstatusId);
            if (guiaFilter.FechaRegistroInicio.HasValue && guiaFilter.FechaRegistroFin.HasValue)
                query = query.Where(g => g.FechaRegistro.Value.Date >= guiaFilter.FechaRegistroInicio.Value.Date
                && g.FechaRegistro.Value.Date <= guiaFilter.FechaRegistroFin.Value.Date);
            if (guiaFilter.TiuiAmigoId.HasValue)
                query = query.Where(g => g.TiuiAmigoId.Value == guiaFilter.TiuiAmigoId.Value);
            var itemsSkip = (guiaFilter.Page - 1) * guiaFilter.PageSize;
            guiaFilter.TotalRows = await query.CountAsync();
            return await query.Include(g => g.Destinatario).Include(g => g.Estatus).Skip(itemsSkip).Take(guiaFilter.PageSize).AsNoTracking().ToListAsync();

        }
        /// <summary>
        /// Obtiene la ultima generada
        /// </summary>        
        /// <returns>Task de la guía</returns>
        public async Task<Guia> GetLastGuia(int tiuiAmigoId)
        {
            return await this._context.Guias.Where(g => g.TiuiAmigoId == tiuiAmigoId).Include(g => g.TiuiAmigo).OrderBy(g => g.Consecutivo).LastOrDefaultAsync();
        }
 
        public async Task<GuiaInfoSuscriptionDTO> GetGuiaInfo(string folio)
        {
            return await this._context.guiainfosuscription.Where(g => g.Folio.Equals(folio)).FirstOrDefaultAsync();
        }
    }
}
