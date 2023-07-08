using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.DTOs.Paging;
using Tiui.Application.Mail.Configuration;
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
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using Tiui.Application.Repository.Seguridad;

namespace Tiui.Services.Guias
{
  /// <summary>
  /// Servicio para el manejo de las guías
  /// </summary>
  public class GuiaService : IGuiaService
  {
    private readonly IUsarioRepository _usuarioRepository;
    private readonly IGuiaRepository _guiaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRegistroGuiaEmail _registroGuiaEmail;
    private readonly IConfiguration _configuration;
    private readonly ILogger<GuiaService> _logger;
    private readonly IPaqueteriaRepository _paqueteriaRepository;
    private readonly IBitacoraGuiaRepository _bitacoraGuiaRepository;
    private readonly HttpClient _httpClient;


    public GuiaService(
      IGuiaRepository guiaRepository,
      IUnitOfWork unitOfWork,
      IMapper mapper,
      IRegistroGuiaEmail registroGuiaEmail,
      HttpClient httpClient,
      IConfiguration configuration,
      ILogger<GuiaService> logger,
      IPaqueteriaRepository paqueteriaRepository,
      IBitacoraGuiaRepository bitacoraGuiaRepository,
      IUsarioRepository usuarioRepository
      )
    {
      this._usuarioRepository = usuarioRepository;
      this._guiaRepository = guiaRepository;
      this._unitOfWork = unitOfWork;
      this._mapper = mapper;
      this._registroGuiaEmail = registroGuiaEmail;
      this._configuration = configuration;
      this._logger = logger;
      this._paqueteriaRepository = paqueteriaRepository;
      this._bitacoraGuiaRepository = bitacoraGuiaRepository;
      this._httpClient = httpClient;
    }
    /// <summary>
    /// Crear el registro de la guia con todas sus relaciones
    /// </summary>
    /// <param name="guiaCreateDTO">Contiene información de la guia a registrar</param>
    /// <returns>Respuesta ApiResultModel con información de la petición</returns>
    public async Task<ApiResultModel<GuiaCreateDTO>> Create(GuiaCreateDTO guiaCreateDTO)
    {
      Guia guia = await GetGuia(guiaCreateDTO);

      Guid firm = Guid.NewGuid();
      try
      {
        await _unitOfWork.BeginTransaction(firm);
        await RegistrarRemitente(guiaCreateDTO);
        await RegistrarDestinatario(guiaCreateDTO);
        await RegistrarConfiguracionCaja(guiaCreateDTO);
        await this._guiaRepository.Create(guia);
        await RegistrarBitacora(guia);
        await this._unitOfWork.Commit(firm);
        this.GetPrintFolio(guia.GuiaId.ToString());
        this.SendMail(guia);
      }
      catch
      {
        await this._unitOfWork.Rollback(firm);
        throw;
      }

      return new ApiResultModel<GuiaCreateDTO> { Entity = this._mapper.Map<GuiaCreateDTO>(guia), Message = "Registro exitoso", Success = true, Status = "200" };
    }
    /// <summary>
    /// Asigna lo valores faltantes de la guía
    /// </summary>
    /// <param name="guiaCreateDTO">DTO con la información de la guía a registrar</param>
    /// <returns>Guia creada</returns>
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
    /// <summary>
    /// Regresa la guía proporcionada con el folio generado
    /// </summary>
    /// <param name="guia">Guía a procesar</param>
    /// <returns>La guía procesada</returns>
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
    /// <summary>
    /// Registra información del remitente
    /// </summary>
    /// <param name="guiaCreateDTO">Contiene información de la guia</param>
    /// <returns>Task de resultado</returns>
    private async Task RegistrarRemitente(GuiaCreateDTO guiaCreateDTO)
    {
      if (guiaCreateDTO.RegistrarRemitente)
      {
        LibretaDireccion libretaDireccionRemitente = this._mapper.Map<LibretaDireccion>(guiaCreateDTO.Remitente);
        libretaDireccionRemitente.TiuiAmigoId = guiaCreateDTO.TiuiAmigoId;
        var libretaRepository = this._unitOfWork.Repository<LibretaDireccion>();
        await libretaRepository.Insert(libretaDireccionRemitente);
        await libretaRepository.Commit();
      }
    }
    /// <summary>
    /// Registra información del destinatario
    /// </summary>
    /// <param name="guiaCreateDTO">Contiene información de la Guía</param>
    /// <returns>Task de resultado</returns>
    private async Task RegistrarDestinatario(GuiaCreateDTO guiaCreateDTO)
    {
      if (guiaCreateDTO.RegistrarDestinatario)
      {
        LibretaDireccion libretaDireccionDestinatario = this._mapper.Map<LibretaDireccion>(guiaCreateDTO.Destinatario);
        libretaDireccionDestinatario.TiuiAmigoId = guiaCreateDTO.TiuiAmigoId;
        var libretaRepository = this._unitOfWork.Repository<LibretaDireccion>();
        await libretaRepository.Insert(libretaDireccionDestinatario);
        await libretaRepository.Commit();
      }
    }
    /// <summary>
    /// Registrar información de la configuración de la caja
    /// </summary>
    /// <param name="guiaCreateDTO">Contiene información de la Guía</param>
    /// <returns>Task de resultado</returns>
    private async Task RegistrarConfiguracionCaja(GuiaCreateDTO guiaCreateDTO)
    {
      if (guiaCreateDTO.RegistrarConfiguracionPaquete)
      {
        ConfiguracionCajaTiuiAmigo configuracionCajaTiuiAmigo = this._mapper.Map<ConfiguracionCajaTiuiAmigo>(guiaCreateDTO.Paquete);
        configuracionCajaTiuiAmigo.TiuiAmigoId = guiaCreateDTO.TiuiAmigoId;
        configuracionCajaTiuiAmigo.Peso = guiaCreateDTO.Paquete.PesoCotizado;
        var configuracionCajaRepository = this._unitOfWork.Repository<ConfiguracionCajaTiuiAmigo>();
        await configuracionCajaRepository.Insert(configuracionCajaTiuiAmigo);
        await configuracionCajaRepository.Commit();
      }
    }
    /// <summary>
    /// Registra en la bitácora el movimiento de la guía
    /// </summary>
    /// <param name="guia">guia creada</param>
    /// <returns>Task</returns>
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
    /// <summary>
    /// Envía notificación al nuevo tiui amigo que su cuenta ha sido creada
    /// </summary>
    /// <param name="usuario">Usuario que contiene la información</param>
    /// <returns>True si el envío de correo fue exitoso</returns>       
    private void SendMail(Guia guia)
    {
      try
      {
        this._registroGuiaEmail.To = guia.Destinatario.CorreoElectronico;
        this._registroGuiaEmail.Destinatario(guia.Destinatario.Nombre).TiuAmigo(guia.Remitente.Nombre)
            .UrlWebSite($"{this._configuration["UrlWebSiteDetalleGuia"]}/{guia.Folio}").NumeroGuia(guia.Folio);
        this._registroGuiaEmail.SendMailAsync();
      }
      catch (Exception ex)
      {
        this._logger.LogError(ex.ToString());
      }
    }
    /// <summary>
    /// Obtiene información de la guia
    /// </summary>
    /// <param name="guiaId">Folio de la guía a buscar</param>
    /// <returns>GuiaDetailDTO con información de la guía encontrada</returns>
    public async Task<GuiaDetailDTO> GetGuia(string folio)
    {
      var guia = (await this._guiaRepository.Query(g => g.Folio.Equals(folio), g => g.Destinatario, g => g.Remitente)).FirstOrDefault();
      if (guia == null)
        throw new DataNotFoundException("No se encontró la guía con el folio proporcionado");
      var repository = this._unitOfWork.Repository<NotificacionCliente>();
      var notificacion = (await repository.Query(n => n.GuiaId == guia.GuiaId)).FirstOrDefault();
      var bitacoraGuia = await this._bitacoraGuiaRepository.GetStateChangeGuia(guia.GuiaId);

      return SetGuiaData((guia, notificacion, bitacoraGuia));
    }
    private GuiaDetailDTO SetGuiaData((Guia guia, NotificacionCliente notificacion, List<BitacoraGuiaDTO> bitacoraGuia) data)
    {
      var guiaDTO = new GuiaDetailDTO();
      guiaDTO.GuiaId = data.guia.GuiaId;
      guiaDTO.Folio = data.guia.Folio;
      guiaDTO.NombreRemitente = data.guia.Remitente.Nombre;
      guiaDTO.Empresa = data.guia.Remitente.Empresa;
      guiaDTO.CiudadOrigen = data.guia.Remitente.Ciudad;
      guiaDTO.CiudadDestino = data.guia.Destinatario.Ciudad;
      guiaDTO.FechaEstimadaEntrega = data.guia.FechaEstimadaEntrega;
      guiaDTO.EstatusId = data.guia.EstatusId;
      if (data.notificacion != null)
        guiaDTO.NotificacionCliente = this._mapper.Map<NotificacionClienteDTO>(data.notificacion);
      guiaDTO.Movimientos = data.bitacoraGuia;
      guiaDTO.Procesos = data.bitacoraGuia.OrderBy(b => b.FechaRegistro).Select(b => b.Proceso).Distinct().ToList();
      return guiaDTO;
    }
    public async Task<GuiaTrackingPagedListDTO> GetWithFilterAndPaging(GuiaFilterDTO filter)
    {
      filter.Page = filter.Page == 0 ? 1 : filter.Page;
      var items = this._mapper.Map<List<GuiaTrackingDTO>>(await this._guiaRepository.GetGuiaWithFilterAndPaging(filter));
      GuiaTrackingPagedListDTO guiaTrackingPagedListDTO = new GuiaTrackingPagedListDTO
      {
        Items = items,
        Page = filter.Page,
        PageSize = filter.PageSize,
        TotalRows = filter.TotalRows
      };
      return guiaTrackingPagedListDTO;
    }
    public async Task<GuiaTrackingPagedListDTO> GetWithFilterAndPagingTiuiAmigo(GuiaFilterDTO filter)
    {
      filter.Page = filter.Page == 0 ? 1 : filter.Page;
      var items = this._mapper.Map<List<GuiaTrackingDTO>>(await this._guiaRepository.GetGuiaWithFilterAndPaging(filter));
      GuiaTrackingPagedListDTO guiaTrackingPagedListDTO = new GuiaTrackingPagedListDTO
      {
        Items = items,
        Page = filter.Page,
        PageSize = filter.PageSize,
        TotalRows = filter.TotalRows
      };
      return guiaTrackingPagedListDTO;
    }
    public async Task<byte[]> GetPrintFolio(string guiaId)
    {
      try
      {
        {
          string query = @"
            query GetPrintGuia($folios: [String]!) {
            getPrintGuia(folios: $folios) {
                error
                filesExistent
                inCreation {
                base64Data
                error
                folio
                isError
                message
                url
                }
                isError
            }
            }";

          var request = new HttpRequestMessage(HttpMethod.Post, this._configuration["URL_GQL"]);
          //request.Headers.Add("Authorization", "Bearer token-de-autenticación");
          var requestBody = new { query, variables = new { folios = new string[] { guiaId } } };
          request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
          var response = await _httpClient.SendAsync(request);
          response.EnsureSuccessStatusCode();
          var responseContent = await response.Content.ReadAsStringAsync();
          var guiaReportResponse = JsonConvert.DeserializeObject<PrintGuiaDTO>(responseContent);

          string[] filesExistent = guiaReportResponse.Data.PrintGuia.FilesExistent;
          InCreation[] inCreation = guiaReportResponse.Data.PrintGuia.InCreation;

          if (filesExistent.Length > 0)
          {
            // Si hay una URL disponible, descarga los datos del archivo
            var fileBytes = await _httpClient.GetByteArrayAsync(filesExistent[0]);
            return fileBytes;
          }
          else if (inCreation.Length > 0)
          {
            // Si hay datos en base64 disponibles, decodifica los datos
            var base64Data = inCreation[0].Base64Data;
            var fileBytes = Convert.FromBase64String(base64Data);
            return fileBytes;
          }
          else
          {
            throw new DataNotFoundException("No se encontró la guía con el folio proporcionado");
          }
        }
      }
      catch (System.Exception)
      {
        throw new DataNotFoundException("Algo malo a pasado revise su folio");
      }
    }
    public async Task<ApiResultModel<GuiaUpdateStateDTO>> SetGuiaCancelationAsync(GuiaUpdateCancelationDTO guiaUpdateCancelationDTO)
    {
      try
      {
        //TODO filtro por tiuiamigo del usuario.
        var guia =
        await GetGuia(guiaUpdateCancelationDTO.Folio.ToString());
        if (guia == null)
        {
          return new ApiResultModel<GuiaUpdateStateDTO>
          {
            Message = "No se encontró la guía",
            Success = false,
          };
        }
        if (guia.EstatusId == 12)
        {
          return new ApiResultModel<GuiaUpdateStateDTO>
          {
            Message = "La guía ya se encuentra cancelada",
            Success = false,
          };
        }
        var guiaUpdateStateDTO = new GuiaUpdateStateDTO
        {
          MotivoCancelacionId = guiaUpdateCancelationDTO.MotivoCancelacionId,
          GuiaId = guia.GuiaId,
          NuevoEstatus = EEstatusGuia.CANCELADO,
        };
        var guiaStateService = new GuiaStateService();
        return await guiaStateService.SetState(guiaUpdateStateDTO);
      }
      catch (System.Exception)
      {
        throw new System.Exception("Ocurrió un error al cancelar la guía");
      }
    }
  }
}
