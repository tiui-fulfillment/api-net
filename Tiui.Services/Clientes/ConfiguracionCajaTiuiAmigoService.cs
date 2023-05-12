using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Tiui.Application.DTOs.Clientes;
using Tiui.Application.Repository.Clientes;
using Tiui.Application.Services.Clientes;

namespace Tiui.Services.Clientes
{
    /// <summary>
    /// Servicio para el manejo de la configuración de cajas
    /// </summary>
    public class ConfiguracionCajaTiuiAmigoService : IConfiguracionCajaTiuiAmigoService
    {
        private readonly IConfiguracionCajaTiuiAmigoRepository _configuracionCajaTiuiAmigoRepository;
        private readonly IMapper _mapper;

        public ConfiguracionCajaTiuiAmigoService(IConfiguracionCajaTiuiAmigoRepository configuracionCajaTiuiAmigoRepository, IMapper mapper)
        {
            this._configuracionCajaTiuiAmigoRepository = configuracionCajaTiuiAmigoRepository;
            this._mapper = mapper;
        }
        /// <summary>
        /// Consulta las configuraciones de caja por tiui amigo
        /// </summary>
        /// <param name="tiuiAmigoId">Identificador del tiui amigo</param>
        /// <returns>Listado de configuraciones para el tiui amigo</returns>
        public async Task<List<ConfiguracionCajaTiuiAmigoDTO>> GetByTiuiAmigo(int? tiuiAmigoId)
        {
            var query = await this._configuracionCajaTiuiAmigoRepository.Query(c => c.TiuiAmigoId == tiuiAmigoId);
            return this._mapper.Map<List<ConfiguracionCajaTiuiAmigoDTO>>(query);
        }
    }
}
