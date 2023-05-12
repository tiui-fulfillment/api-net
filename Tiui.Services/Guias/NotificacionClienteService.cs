using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.Repository.UnitOfWork;
using Tiui.Application.Services.Guias;
using Tiui.Entities.Guias;
using Tiui.Utils.Exceptions;

namespace Tiui.Services.Guias
{
  /// <summary>
  /// Servicio para el manejo de los servicios
  /// </summary>
  public class NotificacionClienteService : INotificacionClienteService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NotificacionClienteService(IUnitOfWork unitOfWork, IMapper mapper)
    {
      this._unitOfWork = unitOfWork;
      this._mapper = mapper;
    }
    /// <summary>
    /// Registrar una configuración de notificación para clientes
    /// </summary>
    /// <param name="notificacionClienteDTO">Contiene la información de la configuración</param>
    /// <returns>El objeto creado</returns>
    public async Task<ApiResultModel<NotificacionClienteCreateDTO>> Create(NotificacionClienteCreateDTO notificacionClienteDTO)
    {
      var repository = this._unitOfWork.Repository<NotificacionCliente>();
      var notificacion = (await repository.Query(n => n.GuiaId == notificacionClienteDTO.GuiaId)).FirstOrDefault();
      if (notificacion != null)
        throw new BusinessRuleException("La guía ya tiene registrado un usuario de notificación");
      var notificacionCliente = this._mapper.Map<NotificacionCliente>(notificacionClienteDTO);
      await repository.Insert(notificacionCliente);
      await repository.Commit();

      return new ApiResultModel<NotificacionClienteCreateDTO> { Entity = this._mapper.Map<NotificacionClienteCreateDTO>(notificacionCliente), Message = "Información registrada con éxito", Success = true, Status = "200" };
    }


  }
}
