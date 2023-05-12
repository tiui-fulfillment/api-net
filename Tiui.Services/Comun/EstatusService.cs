using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tiui.Application.DTOs.Comun;
using Tiui.Application.Repository.UnitOfWork;
using Tiui.Application.Services.Comun;
using Tiui.Entities.Comun;
using Tiui.Entities.Guias;
using Tiui.Entities.State;
using Tiui.Utils.Exceptions;

namespace Tiui.Services.Comun
{
    public class EstatusService : IEstatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EstatusService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<List<EstatusDTO>> GetAll()
        {
            var repository = this._unitOfWork.Repository<Estatus>();
            return this._mapper.Map<List<EstatusDTO>>(await repository.GetAll()).OrderBy(e => e.EstatusId).ToList();
        }
        public async Task<List<EstatusDTO>> GetAllByTiuiAmigoId(int tiuiAmigoId)
        {
            var repository = this._unitOfWork.Repository<Estatus>();
            var tiuiAmigoRepository = this._unitOfWork.Repository<TiuiAmigo>();
            var tiuiAmigo = (await tiuiAmigoRepository.Query(t => t.TiuiAmigoId == tiuiAmigoId)).FirstOrDefault();
            if (tiuiAmigo == null)
                throw new DataNotFoundException("El tiui amigo no existe");

            return this._mapper.Map<List<EstatusDTO>>(await repository.Query(e=>e.TipoFlujo == tiuiAmigo.TipoProceso || e.TipoFlujo == ETipoFlujoGuia.TODOS)).OrderBy(e => e.EstatusId).ToList();
        }

        public async Task<List<EstatusDTO>> GetNextStatus(int estatusGuia)
        {
            var repository = this._unitOfWork.Repository<Estatus>();
            var statusGuia = await repository.GetAll();
            if (!Enum.IsDefined(typeof(EEstatusGuia), estatusGuia))
                throw new DataNotFoundException("El estatus proporcionado no esta definido para la guía");
            return this._mapper.Map<List<EstatusDTO>>(GuiaRuleState.GetStatus(statusGuia.ToList(), (EEstatusGuia)estatusGuia));
        }
    }
}
