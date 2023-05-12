using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.File;
using Tiui.Application.Mail.Configuration;
using Tiui.Application.Reports;
using Tiui.Application.Repository.Guias;
using Tiui.Application.Repository.UnitOfWork;
using Tiui.Application.Services.Guias;
using Tiui.Entities.Comun;
using Tiui.Entities.Guias;
using Tiui.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.IO;

namespace Tiui.Services.Guias
{
    public class GuiaMasiveService : IGuiaMasiveService
    {
        private readonly IGuiaRepository _guiaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRegistroGuiaEmail _registroGuiaEmail;
        private readonly IConfiguration _configuration;
        private readonly IPaqueteriaRepository _paqueteriaRepository;
        private readonly ILogger<GuiaService> _logger;

        private readonly IBitacoraGuiaRepository _bitacoraGuiaRepository;
        private readonly IFileService _fileService;
        private readonly IGuiaReport _guiaReport;

        public GuiaMasiveService(IGuiaRepository guiaRepository, IUnitOfWork unitOfWork, IMapper mapper, IRegistroGuiaEmail registroGuiaEmail
            , IConfiguration configuration, ILogger<GuiaService> logger, IPaqueteriaRepository paqueteriaRepository
            , IBitacoraGuiaRepository bitacoraGuiaRepository, IFileService fileService, IGuiaReport guiaReport)
        {
            this._guiaRepository = guiaRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._registroGuiaEmail = registroGuiaEmail;
            this._configuration = configuration;
            this._logger = logger;
            this._paqueteriaRepository = paqueteriaRepository;
            this._bitacoraGuiaRepository = bitacoraGuiaRepository;
            this._fileService = fileService;
            this._guiaReport = guiaReport;
        }
        public async Task<ApiResultModel<List<GuiaCreateDTO>>> Create(GuiaMasiveDTO guiaMasiveDTO)
        {
            LibretaDireccion remitente = (await this._unitOfWork.Repository<LibretaDireccion>()
                .Query(r => r.DireccionId == guiaMasiveDTO.RemitenteId, d => d.Municipio
                , d => d.Municipio.Estado, d => d.Municipio.Estado.Pais))
                .FirstOrDefault();
            if (remitente == null)
                throw new DataNotFoundException("No se encontro el remitente seleccionado");
            List<GuiaCreateDTO> guiasNoCreadas = new List<GuiaCreateDTO>();
            List<GuiaCreateDTO> guiasCreadas = new List<GuiaCreateDTO>();

            foreach (var guiaDTO in guiaMasiveDTO.Guias)
            {
                guiaDTO.Remitente = this._mapper.Map<RemitenteDTO>(remitente);
                guiaDTO.PaqueteriaId = 1; //TODO; código duro por falta de definición
                bool result = await this.CreateGuia(guiaDTO);
                if (!result)
                    guiasNoCreadas.Add(guiaDTO);
                else
                    guiasCreadas.Add(guiaDTO);
            }
            return new ApiResultModel<List<GuiaCreateDTO>>
            {
                Entity = guiasNoCreadas.Count == 0 ? guiasCreadas : guiasNoCreadas,
                Message = guiasNoCreadas.Count > 0 ? "Proceso con errores" : "Registro exitoso",
                Success = guiasNoCreadas.Count == 0,
                Status = guiasNoCreadas.Count > 0 ? "200" : "500"
            };
        }

        public async Task<byte[]> CreateZip(List<GuiaCreateDTO> guiaMasiveDTO)
        {
            //DireccionGuia remitente = (await this._unitOfWork.Repository<DireccionGuia>().Query(r => r.DireccionGuiaId == guiaMasiveDTO.RemitenteId)).FirstOrDefault();
            //if (remitente == null)
            //    throw new DataNotFoundException("No se encontro el remitente seleccionado");
            Dictionary<string, MemoryStream> files = new Dictionary<string, MemoryStream>();
            foreach (var guiaDTO in guiaMasiveDTO)
            {
                //guiaDTO.Remitente = this._mapper.Map<RemitenteDTO>(remitente);
                //guiaDTO.PaqueteriaId = 1; //TODO; código duro por falta de definición
                //bool result = await this.CreateGuia(guiaDTO);
                //if (result)
                //{
                await this._guiaReport.SetData(guiaDTO.GuiaId.Value);
                files.Add($"Guia_{guiaDTO.Folio}.pdf", new MemoryStream(this._guiaReport.ToPdf()));
                //}
            }
            return this._fileService.GenerarZip(files, null);
        }
        private async Task<bool> CreateGuia(GuiaCreateDTO guiaCreateDTO)
        {
            Guia guia = await GetGuia(guiaCreateDTO);
            Guid firm = Guid.NewGuid();
            try
            {
                await _unitOfWork.BeginTransaction(firm);
                await this._guiaRepository.Create(guia);
                await RegistrarBitacora(guia);
                await this._unitOfWork.Commit(firm);
                this.SendMail(guia);
                guiaCreateDTO.GuiaId = guia.GuiaId;
                guiaCreateDTO.Folio = guia.Folio;
                return true;
            }
            catch
            {
                await this._unitOfWork.Rollback(firm);
                return false;
            }
        }
        private async Task<Guia> GetGuia(GuiaCreateDTO guiaCreateDTO)
        {
            var guia = this._mapper.Map<Guia>(guiaCreateDTO);
            guia.GuiaId = null;
            var paqueteria = (await this._paqueteriaRepository.Query(p => p.PaqueteriaId == guia.PaqueteriaId)).FirstOrDefault();

            if (paqueteria == null)
                throw new BusinessRuleException("La paqueteria asociada a la guía no esta registrada");
            guia.Paquete.PaqueteId = null;
            guia.Paquete.FechaRegistro = DateTime.UtcNow;
            guia.FechaRegistro = DateTime.UtcNow;
            guia.FechaEstimadaEntrega = DateTime.UtcNow.AddDays(paqueteria.MaximoDiasDeEntrega);
            guia = await this.GetGuiaWithFolioAndConsecutivo(guia);
            return guia;
        }
        private async Task<Guia> GetGuiaWithFolioAndConsecutivo(Guia guia)
        {
            var guiaLast = await this._guiaRepository.GetLastGuia(guia.TiuiAmigoId.Value);
            TiuiAmigo tiuiAmigo;
            int consecutivo = 1;
            if (guiaLast == null)
                tiuiAmigo = (await this._unitOfWork.Repository<TiuiAmigo>().Query(t => t.TiuiAmigoId == guia.TiuiAmigoId)).FirstOrDefault();
            else
            {
                tiuiAmigo = guiaLast.TiuiAmigo;
                consecutivo = guiaLast.Consecutivo + 1;
            }
            Random random = new Random();
            guia.SetFollio(tiuiAmigo.Codigo, random.Next(1, 99), consecutivo);
            guia.Consecutivo = consecutivo;
            guia.EstatusId = tiuiAmigo.TipoProceso == ETipoFlujoGuia.CONSOLIDADO ? (int)EEstatusGuia.DOCUMENTADO_CONSOLIDADO
                : tiuiAmigo.TipoProceso == ETipoFlujoGuia.FULFILMENT ? (int)EEstatusGuia.DOCUMENTADO_FULFILLMENT : (int)EEstatusGuia.DOCUMENTADO_RECOLECCION;
            return guia;
        }
        private async Task RegistrarBitacora(Guia guia)
        {
            BitacoraGuia bitacoraGuia = new BitacoraGuia()
            {
                EstatusAnterior = (EEstatusGuia)guia.EstatusId,
                EstatusNuevo = (EEstatusGuia)guia.EstatusId,
                FechaRegistro = DateTime.UtcNow,
                Guia = guia,
                GuiaId = guia.GuiaId
            };
            await _bitacoraGuiaRepository.Insert(bitacoraGuia);
            await _bitacoraGuiaRepository.Commit();
        }
        private void SendMail(Guia guia)
        {
            try
            {
                TiuiAmigo tiuiAmigo = this._unitOfWork.Repository<TiuiAmigo>().Query(t => t.TiuiAmigoId == guia.TiuiAmigoId).Result.FirstOrDefault();
                this._registroGuiaEmail.To = guia.Destinatario.CorreoElectronico;
                this._registroGuiaEmail.Destinatario(guia.Destinatario.Nombre).TiuAmigo(tiuiAmigo.GetNombreCompleto())
                    .UrlWebSite($"{this._configuration["UrlWebSiteDetalleGuia"]}/{guia.Folio}").NumeroGuia(guia.Folio);
                this._registroGuiaEmail.SendMailAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
            }
        }
    }
}
