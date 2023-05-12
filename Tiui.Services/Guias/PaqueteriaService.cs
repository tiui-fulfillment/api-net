using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.Repository.Guias;
using Tiui.Application.Services.Guias;
using Tiui.Entities.Guias;

namespace Tiui.Services.Guias
{
    /// <summary>
    /// Servicio para el manejo de las paqueterias
    /// </summary>
    public class PaqueteriaService : IPaqueteriaService
    {
        private readonly IPaqueteriaRepository _paqueteriaRepository;
        private readonly IMapper _mapper;

        public PaqueteriaService(IPaqueteriaRepository paqueteriaRepository, IMapper mapper)
        {
            this._paqueteriaRepository = paqueteriaRepository;
            this._mapper = mapper;
        }
        /// <summary>
        /// Registrar una instancia de paqueteria
        /// </summary>
        /// <param name="paqueteriaDTO">DTO con información de la paqueteria a registrar</param>
        /// <returns>DTO con información de la paqueteria registrada</returns>
        public async Task<PaqueteriaDTO> Create(PaqueteriaDTO paqueteriaDTO)
        {
            var paqueteria = this._mapper.Map<Paqueteria>(paqueteriaDTO);
            await this._paqueteriaRepository.Insert(paqueteria);
            await this._paqueteriaRepository.Commit();
            return this._mapper.Map<PaqueteriaDTO>(paqueteria);
        }
        /// <summary>
        /// Obtiene listado de paqueterias registradas en el sistema
        /// </summary>
        /// <returns>Listado de paqueterias</returns>
        public async Task<List<PaqueteriaDTO>> GetAll()
        {
            var result = await this._paqueteriaRepository.Query(null);
            return this._mapper.Map<List<PaqueteriaDTO>>(result);
        }
    }
}
