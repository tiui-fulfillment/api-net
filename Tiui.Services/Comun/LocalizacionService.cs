using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Tiui.Application.DTOs.Comun;
using Tiui.Application.Repository.Comun;
using Tiui.Application.Services.Comun;

namespace Tiui.Services.Comun
{
    /// <summary>
    /// Servicio para la consulta de País, Estados y Municipios
    /// </summary>
    public class LocalizacionService : ILocalizacionService
    {
        private readonly IPaisRepository _paisRepository;
        private readonly IEstadoRepository _estadoRepository;
        private readonly IMunicipioRepository _municipioRepository;
        private readonly IMapper _mapper;

        public LocalizacionService(IPaisRepository paisRepository, IEstadoRepository estadoRepository, IMunicipioRepository municipioRepository
            , IMapper mapper)
        {
            this._paisRepository = paisRepository;
            this._estadoRepository = estadoRepository;
            this._municipioRepository = municipioRepository;
            this._mapper = mapper;
        }
        /// <summary>
        /// Obtiene el listado de paises
        /// </summary>
        /// <returns>Retorna el listado de paises</returns>
        public async Task<List<PaisDTO>> GetPaises()
        {
            var paises = await this._paisRepository.Query(null);
            return this._mapper.Map<List<PaisDTO>>(paises);
        }
        /// <summary>
        /// Obtiene el listado de Estados
        /// </summary>
        /// <returns>Retorna el listado de Estados</returns>
        public async Task<List<EstadoDTO>> GetEstados()
        {
            var estados = await this._estadoRepository.Query(null);
            return this._mapper.Map<List<EstadoDTO>>(estados);
        }
        /// <summary>
        /// Obtiene el listado de Municipios
        /// </summary>
        /// <returns>Retorna el listado de Municipios</returns>
        public async Task<List<MunicipioDTO>> GetMunicipios(int? estadoId)
        {
            var municipios = await this._municipioRepository.Query(m => m.EstadoId == estadoId);
            return this._mapper.Map<List<MunicipioDTO>>(municipios);
        }
    }
}
