using Tiui.Application.Repository.Clientes;
using Tiui.Entities.Comun;

namespace Tiui.Data.Repository.Clientes
{
    /// <summary>
    /// Repositorio para el manejo de la configuración de cajas
    /// </summary>
    public class ConfiguracionCajaTiuiAmigoRepository:CRUDRepository<ConfiguracionCajaTiuiAmigo>, IConfiguracionCajaTiuiAmigoRepository
    {
        public ConfiguracionCajaTiuiAmigoRepository(TiuiDBContext context):base(context)
        {

        }
    }
}
