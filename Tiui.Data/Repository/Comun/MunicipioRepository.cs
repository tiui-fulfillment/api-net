using Tiui.Application.Repository.Comun;
using Tiui.Entities.Comun;

namespace Tiui.Data.Repository.Comun
{
    /// <summary>
    /// Repositorio para el manejo de los municipios
    /// </summary>
    public class MunicipioRepository : CRUDRepository<Municipio>, IMunicipioRepository
    {
        public MunicipioRepository(TiuiDBContext context) : base(context)
        {

        }
    }
}
