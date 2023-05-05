using Microsoft.EntityFrameworkCore;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.Repository.Guias;
using Tiui.Entities.Guias;

namespace Tiui.Data.Repository.Guias
{
    public class BitacoraGuiaRepository : CRUDRepository<BitacoraGuia>, IBitacoraGuiaRepository
    {
        public BitacoraGuiaRepository(TiuiDBContext context) : base(context)
        {
        }
        public async Task<List<BitacoraGuiaDTO>> GetStateChangeGuia(long? guiaId)
        {
            var query = from bitacora in this._context.BitacoraGuias
                        join estatus in this._context.Estatus on (int)bitacora.EstatusNuevo equals estatus.EstatusId
                        where bitacora.GuiaId == guiaId
                        select new BitacoraGuiaDTO { FechaRegistro = bitacora.FechaRegistro, Estatus = estatus.Nombre, Proceso = estatus.Proceso };
            return await query.OrderByDescending(b => b.FechaRegistro).ToListAsync();
        }
    }
}
