using AutoMapper;
using Tiui.Application.DTOs.Comun;
using Tiui.Application.Repository.UnitOfWork;
using Tiui.Application.Services.Comun;
using Tiui.Entities.Comun;

namespace Tiui.Services.Comun
{
    /// <summary>
    /// Servicio para el manejo de la configuración de la aplicación
    /// </summary>
    public class ConfiguracionAppService : IConfiguracionAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConfiguracionAppService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<ConfiguracionAppDTO> Get()
        {
            var repository = this._unitOfWork.Repository<ConfiguracionApp>();
            var configuracionApp = (await repository.GetAll()).FirstOrDefault();
            return this._mapper.Map<ConfiguracionAppDTO>(configuracionApp);
        }
    }
}
