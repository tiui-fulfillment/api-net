using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Tiui.Application.DTOs.Clientes;
using Tiui.Application.Repository.Clientes;
using Tiui.Application.Services.Clientes;
using Tiui.Entities.Comun;

namespace Tiui.Services.Clientes
{
    /// <summary>
    /// Servicio para el manejo de las direcciones
    /// </summary>
    public class DireccionService : IDireccionService
    {
        private readonly ILibretaDireccionRepository _libretaDireccionRepository;
        private readonly IMapper _mapper;

        public DireccionService(ILibretaDireccionRepository libretaDireccionRepository, IMapper mapper)
        {
            this._libretaDireccionRepository = libretaDireccionRepository;
            this._mapper = mapper;
        }
        public async Task<LibretaDireccionDTO> Create(LibretaDireccionCreateDTO libretaDireccionCreateDTO)
        {
            var libretaDireccion = this._mapper.Map<LibretaDireccion>(libretaDireccionCreateDTO);
            await this._libretaDireccionRepository.Insert(libretaDireccion);
            await this._libretaDireccionRepository.Commit();
            return this._mapper.Map<LibretaDireccionDTO>(libretaDireccion);
        }
        /// <summary>
        /// Obtiene las direcciones asociadas al tiui amigo
        /// </summary>
        /// <param name="filtro">Valor del filtro a aplicar</param>
        /// <returns>Listado de direcciones encontradas</returns>
        public async Task<List<LibretaDireccionDTO>> GetByTiuiAmigo(LibretaDireccionFiltroDTO filtro)
        {
            var result = await this._libretaDireccionRepository.GetByTiuiAmigo(filtro.TiuiAmigoId, filtro.ValorFiltro);
            return this._mapper.Map<List<LibretaDireccionDTO>>(result);

        }
    }
}
