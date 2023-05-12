using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Tiui.Application.DTOs.Clientes;
using Tiui.Application.Repository.Clientes;
using Tiui.Application.Services.Clientes;

namespace Tiui.Services.Clientes
{
    public class TiuiAmigoService : ITiuiAmigoService
    {
        private readonly ITiuiAmigoRepository _tiuiAmigoRepository;
        private readonly IMapper _mapper;

        public TiuiAmigoService(ITiuiAmigoRepository tiuiAmigoRepository, IMapper mapper)
        {
            this._tiuiAmigoRepository = tiuiAmigoRepository;
            this._mapper = mapper;
        }
        public async Task<List<TiuiAmigoSelectDTO>> GetAll() => this._mapper.Map<List<TiuiAmigoSelectDTO>>(await this._tiuiAmigoRepository.GetAll());
    }
}
