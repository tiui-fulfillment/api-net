using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Tiui.Application.DTOs.Cancelaciones;
using Tiui.Application.Repository.UnitOfWork;
using Tiui.Application.Services.Cancelaciones;
using Tiui.Entities.Cancelaciones;
using Tiui.Utils.Exceptions;

namespace Tiui.Services.Cancelaciones
{
    public class MotivoCancelacionService : IMotivoCancelacionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MotivoCancelacionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<List<MotivoCancelacionDTO>> GetMotivosByProceso(int tipoCancelacion)
        {
            if (!Enum.IsDefined(typeof(ETipoCancelacion), tipoCancelacion))
                throw new DataNotFoundException("El tipo de cancelación proporcionado no es válido");
            var repository = this._unitOfWork.Repository<MotivoCancelacion>();
            var query = await repository.Query(m => m.TipoCancelacion == (ETipoCancelacion)tipoCancelacion);
            return this._mapper.Map<List<MotivoCancelacionDTO>>(query);
        }
    }
}
