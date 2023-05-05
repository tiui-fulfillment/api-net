using AutoMapper;
using Microsoft.Extensions.Logging;
using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.Mail.Configuration;
using Tiui.Application.Mail.Helpers;
using Tiui.Application.Repository.Guias;
using Tiui.Application.Repository.UnitOfWork;
using Tiui.Application.Services.Guias;
using Tiui.Entities.Cancelaciones;
using Tiui.Entities.Guias;
using Tiui.Entities.State;
using Tiui.Utils.Exceptions;

namespace Tiui.Services.Guias
{
    /// <summary>
    /// Servicio para el manejo de los estatus de la guía
    /// </summary>
    public class GuiaStateService : IGuiaStateService
    {
        private readonly IGuiaRepository _guiaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailStatusFactoryHelper _emailStatusFactoryHelper;
        private readonly ILogger _logger;

        public GuiaStateService(IGuiaRepository guiaRepository, IUnitOfWork unitOfWork
            , IEmailStatusFactoryHelper emailStatusFactoryHelper, ILogger<GuiaStateService> logger)
        {
            this._guiaRepository = guiaRepository;
            this._unitOfWork = unitOfWork;
            this._emailStatusFactoryHelper = emailStatusFactoryHelper;
            this._logger = logger;
        }
        public async Task<ApiResultModel<GuiaUpdateStateDTO>> SetState(GuiaUpdateStateDTO guiaStateDTO)
        {
            var guia = await this.GetGuia(guiaStateDTO.GuiaId);
            EEstatusGuia estatusAnterior = (EEstatusGuia)guia.EstatusId;
            var motivoCancelacion = guiaStateDTO.NuevoEstatus == EEstatusGuia.CANCELADO ? await GetMotivoCancelacion(guiaStateDTO.MotivoCancelacionId) : null;
            GuiaStateFactoryMethod.CreateGuiaState(guia, guiaStateDTO.NuevoEstatus, fechaConciliado: guiaStateDTO.FechaConciliacion
                 , fechaReagendado: guiaStateDTO.FechaReagendado, motivoCancelacion: motivoCancelacion);
            guia.ChangeState();
            guia.Estatus = null;
            await Persist(guia, estatusAnterior);

            return new ApiResultModel<GuiaUpdateStateDTO> { Entity = guiaStateDTO, Message = "Actualización exitosa", Success = true, Status = "200" };
        }
        private async Task<Guia> GetGuia(long? guiaId)
        {
            if (!guiaId.HasValue)
                throw new BusinessRuleException($"El identificador de la guía es requerido");
            var guia = (await this._guiaRepository.Query(g => g.GuiaId == guiaId, g => g.Estatus)).FirstOrDefault();
            if (guia == null)
                throw new DataNotFoundException($"No existe una guía con el identificador: ´{guiaId}");
            return guia;
        }
        private async Task<MotivoCancelacion> GetMotivoCancelacion(int? motivoCancelacionId)
        {
            var repository = this._unitOfWork.Repository<MotivoCancelacion>();
            var motivo = (await repository.Query(m => m.MotivoCancelacionId == motivoCancelacionId)).FirstOrDefault();
            if (motivo == null)
                throw new DataNotFoundException($"No existe un motivo de cancelación con el identificador: ´{motivoCancelacionId}");
            return motivo;
        }

        private async Task Persist(Guia guia, EEstatusGuia estatusAnterior)
        {
            Guid firm = Guid.NewGuid();
            try
            {
                await _unitOfWork.BeginTransaction(firm);
                this._guiaRepository.Update(guia);
                await this._guiaRepository.Commit();
                await RegistrarBitacora(guia, estatusAnterior);
                if (guia.CurrenteState.GetId() == (int)EEstatusGuia.CANCELADO)
                    await RegistrarCancelacion(guia);
                await this._unitOfWork.Commit(firm);
                this.SendMail(guia);
            }
            catch
            {
                await this._unitOfWork.Rollback(firm);
                throw;
            }
        }
        private async Task RegistrarBitacora(Guia guia, EEstatusGuia estatusAnterior)
        {
            BitacoraGuia bitacoraGuia = new BitacoraGuia()
            {
                EstatusAnterior = estatusAnterior,                
                EstatusNuevo = (EEstatusGuia)guia.EstatusId,
                FechaRegistro = DateTime.Now,
                GuiaId = guia.GuiaId
            };
            var bitacoraRepository = this._unitOfWork.Repository<BitacoraGuia>();
            await bitacoraRepository.Insert(bitacoraGuia);
            await bitacoraRepository.Commit();
        }
        private async Task RegistrarCancelacion(Guia guia)
        {
            ICancelState cancelState = guia.CurrenteState as ICancelState;
            if (cancelState == null)
                throw new BusinessRuleException("El cambio de estatus no es cancelable");
            CancelacionGuia cancelacionGuia = new CancelacionGuia
            {
                GuiaId = guia.GuiaId,
                MotivoCancelacionId = cancelState.MotivoCancelacion.MotivoCancelacionId,
                FechaRegistro = DateTime.Now,
                Observacion = this.GetMenssageCancelacion(cancelState.MotivoCancelacion)
            };
            var repository = this._unitOfWork.Repository<CancelacionGuia>();
            await repository.Insert(cancelacionGuia);
            await repository.Commit();
        }
        private string GetMenssageCancelacion(MotivoCancelacion motivoCancelacion)
        {
            if (motivoCancelacion.TipoCancelacion == ETipoCancelacion.ANTES_DE_RUTA)
                return $"Cancelación Tiui Amigo; {motivoCancelacion.Descripcion}";
            return $"Cancelación {motivoCancelacion.Descripcion}";
        }
        private void SendMail(Guia guia)
        {
            try
            {               
                guia = this._guiaRepository.Query(g => g.GuiaId == guia.GuiaId, g => g.TiuiAmigo, g => g.Destinatario).Result.FirstOrDefault();
                IConfiguracionEmail configuracionEmail = this._emailStatusFactoryHelper.CreateEmailConfiguration(guia);
                if (configuracionEmail != null)
                    configuracionEmail.SendMailAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
            }
        }
        public async Task<ApiResultModel<GuiaUpdateStateDTO>> SetStateMasive(GuiaStateChangeMasiveDTO guiaStateMasiveDTO)
        {
            this.ParametersValidate(guiaStateMasiveDTO);
            var guias = await this.GetGuias(guiaStateMasiveDTO);
            foreach (var guiaId in guias)
            {
                guiaStateMasiveDTO.Parameters.GuiaId = guiaId;
                await this.SetState(guiaStateMasiveDTO.Parameters);
            }
            return new ApiResultModel<GuiaUpdateStateDTO> { Entity = guiaStateMasiveDTO.Parameters, Message = "Actualización exitosa", Success = true, Status = "200" };
        }
        private void ParametersValidate(GuiaStateChangeMasiveDTO guiaStateMasiveDTO)
        {
            if (!guiaStateMasiveDTO.PorFiltro && (guiaStateMasiveDTO.Guias == null && guiaStateMasiveDTO.Guias.Count == 0))
                throw new BusinessRuleException("No se ha proporcionado ninguna guía");
            if (!Enum.IsDefined(typeof(EEstatusGuia), guiaStateMasiveDTO.Parameters.NuevoEstatus))
                throw new BusinessRuleException("El estatus que se desea asignar no es válido");
            if (guiaStateMasiveDTO.PorFiltro && !Enum.IsDefined(typeof(EEstatusGuia), guiaStateMasiveDTO.Filter.EstatusId))
                throw new BusinessRuleException("El estatus proporcionado no es válido");
        }
        private async Task<List<long>> GetGuias(GuiaStateChangeMasiveDTO guiaStateMasiveDTO)
        {
            if (!guiaStateMasiveDTO.PorFiltro)
                return guiaStateMasiveDTO.Guias;
            var guias = await this._guiaRepository.GetGuiaWithFilter(guiaStateMasiveDTO.Filter);
            return guias.Select(g => g.GuiaId.Value).ToList();
        }
    }
}
