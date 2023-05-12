using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.Repository.Guias;
using Tiui.Entities.Guias;

namespace Tiui.Data.Repository.GuiaInfoSuscription
{
    public class GuiaInfoSuscriptionRepository : CRUDRepository<GuiaInfoSuscriptionDTO>, IGuiaInfoSuscriptionRepository
    {
        public GuiaInfoSuscriptionRepository(TiuiDBContext context) : base(context)
        {
        }
        public async Task<GuiaInfoSuscriptionDTO> GetGuiaInfo(string folio)
        {
            return await this._context.guiainfosuscription.Where(g => g.Folio.Equals(folio)).FirstOrDefaultAsync();
        }
    }
}
