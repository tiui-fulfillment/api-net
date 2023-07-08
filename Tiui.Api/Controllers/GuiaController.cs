using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Mime;
using System.Net.WebSockets;
using System.Text;
using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.DTOs.Paging;
using Tiui.Application.DTOs.Reports;
using Tiui.Application.Reports;
using Tiui.Application.Services.Guias;
using Tiui.Application.Services.websocket;
using Tiui.Application.Services.Comun;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tiui.Api.Controllers
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [Route("api/[controller]")]
  [ApiController]
  public class GuiaController : ControllerBase
  {
    private readonly IGuiaService _guiaService;
    private readonly IGuiaReport _guiaReport;
    private readonly IGuiaStateService _guiaStateService;
    private readonly IGuiaCompleteReport _guiaCompleteReport;
    private readonly IGuiaMasiveService _guiaMasiveService;
    private readonly HttpClient _httpClient;
    private readonly IGuiaWebSocketHandler _guiaWebSocketHandler;
    private readonly IGraphqlService _graphqlService;

    public GuiaController(IGuiaService guiaService, IGuiaReport guiaReport, IGuiaStateService guiaStateService, IGuiaCompleteReport guiaCompleteReport
        , IGuiaMasiveService guiaMasiveService, HttpClient httpClient, IGuiaWebSocketHandler guiaWebSocketHandler, IGraphqlService graphqlService)
    {
      this._guiaService = guiaService;
      this._guiaReport = guiaReport;
      this._guiaStateService = guiaStateService;
      this._guiaCompleteReport = guiaCompleteReport;
      this._guiaMasiveService = guiaMasiveService;
      this._httpClient = httpClient;
      this._guiaWebSocketHandler = guiaWebSocketHandler;
      this._graphqlService = graphqlService;
    }
    // POST api/<GuiaController>        
    [HttpPost]
    public async Task<ApiResultModel<GuiaCreateDTO>> Post(GuiaCreateDTO guiaCreateDTO)
    {
      return await this._guiaService.Create(guiaCreateDTO);
    }
    [AllowAnonymous]
    [HttpGet, Route("GuiaReport/{guiaId}")]
    public async Task<ActionResult> GetGuiaReport(string guiaId)
    {
      try
      {
        var fileBytes = await this._guiaService.GetPrintFolio(guiaId);
        return File(fileBytes, MediaTypeNames.Application.Pdf, $"guia_{guiaId}.pdf");
      }
      catch (Exception ex)
      {
        return NotFound(ex.Message);
      }
    }

    [AllowAnonymous]
    [HttpGet("{guaId}")]
    public async Task<ActionResult<GuiaDetailDTO>> GetGuia(string guaId)
    {
      return await this._guiaService.GetGuia(guaId);
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("ws")]
    public async Task GetGuiaWS()
    {
      if (HttpContext.WebSockets.IsWebSocketRequest)
      {
        var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        var webSocketConnectionManager = new WebSocketConnectionManager();
        var connectionId = webSocketConnectionManager.AddSocket(webSocket);
        await this._guiaWebSocketHandler.OnConnectedAsync(new WebSocketConnection(connectionId, webSocket)).ContinueWith(async (task) =>
        {
          await this._guiaWebSocketHandler.StartAsync(CancellationToken.None
          );
        });

        WebSocketReceiveResult result;
        try
        {
          var buffer = new byte[1024 * 4];
          result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
          while (webSocket.State == WebSocketState.Open && !result.CloseStatus.HasValue)
          {
            if (result.MessageType == WebSocketMessageType.Text)
            {
              var message = Encoding.ASCII.GetString(buffer, 0, result.Count);
              Console.WriteLine($"Received message from client: {message}");
              // Aquí puedes hacer algo con el mensaje recibido
              await this._guiaWebSocketHandler.HandleMessageAsync(webSocket, message);

            }
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine($"WebSocket Error: {ex.Message}");
        }
      }
      else
      {
        HttpContext.Response.StatusCode = 400;
      }
    }
    [AllowAnonymous]
    [HttpPost("guias-traking")]
    public async Task<GuiaTrackingPagedListDTO> GetGuiasTracking(GuiaFilterDTO guiaFilterDTO)
    {
      return await this._guiaService.GetWithFilterAndPagingTiuiAmigo(guiaFilterDTO);
    }
    [HttpPost, Route("masive")]
    public async Task<ApiResultModel<List<GuiaCreateDTO>>> PostMasive(GuiaMasiveDTO guiaMasiveDTO)
    {
      return await this._guiaMasiveService.Create(guiaMasiveDTO);
    }
    [HttpPost, Route("masive-zip")]
    public async Task<ActionResult> PostMasive2(List<GuiaCreateDTO> guiaMasiveDTO)
    {
      return this.File(await this._guiaMasiveService.CreateZip(guiaMasiveDTO), MediaTypeNames.Application.Zip, $"guias.zip");
    }
    [HttpPut]
    public async Task<ApiResultModel<GuiaUpdateStateDTO>> Put(GuiaUpdateStateDTO guiaUpdateStateDTO)
    {
      return await this._guiaStateService.SetState(guiaUpdateStateDTO);
    }
    [HttpPut("change-state-masive")]
    public async Task<ApiResultModel<GuiaUpdateStateDTO>> PutMavise(GuiaStateChangeMasiveDTO guiaUpdateStateDTO)
    {
      return await this._guiaStateService.SetStateMasive(guiaUpdateStateDTO);
    }
    [Authorize(Roles = "ADMIN")]
    [HttpPost("guias-traking-admin")]
    public async Task<GuiaTrackingPagedListDTO> GetGuiasTrackingAdmin(GuiaFilterDTO guiaFilterDTO)
    {
      return await this._guiaService.GetWithFilterAndPaging(guiaFilterDTO);
    }
    [HttpPost, Route("GuiaCompleteReport")]
    public ActionResult GetGuiaCompleteReport(GuiaReportFilterDTO guiaReportFilterDTO)
    {
      this._guiaCompleteReport.SetData(guiaReportFilterDTO);
      return this.File(this._guiaCompleteReport.ToExcel(), MediaTypeNames.Application.Rtf, $"guias.xlsx");
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost, Route("cancelation")]
    public async Task<ApiResultModel<GuiaUpdateStateDTO>> SetGuiaCancelationAsync(GuiaUpdateCancelationDTO guiaUpdateCancelationDTO)
    {
      return await this._guiaService.SetGuiaCancelationAsync(guiaUpdateCancelationDTO);
    }
    [AllowAnonymous]
    [HttpGet, Route("getGuia/{folio}")]
    public async Task<IActionResult> GetGuiaGQL(string folio)
    {
      string query = @"
              query GetGuias($filters: guiaTableFilter) {
                  getGuias(GuiasFilter: $filters) {
                    codeError
                    isError
                    message
                    result {
                      CantidadPaquetes
                      CobroContraEntrega
                      Destinatario {
                        Calle
                        Ciudad
                        CodigoPostal
                        Colonia
                        CorreoElectronico
                        Cruzamiento
                        DireccionGuiaId
                        Discriminator
                        Empresa
                        Estado
                        GuiaId
                        Nombre
                        Numero
                        Pais
                        Referencias
                        Remitente_GuiaId
                        Telefono
                      }
                      EsPagoContraEntrega
                      Estatus {
                        Descripcion
                        EstatusId
                        Nombre
                        Proceso
                        TipoFlujo
                      }
                      EstatusId
                      FechaConciliacion
                      FechaEstimadaEntrega
                      FechaReagendado
                      FechaRegistro
                      Folio
                      GuiaId
                      ImporteContraEntrega
                      NombreProducto
                      ProcesoCancelacion
                      Remitente {
                        Calle
                        Ciudad
                        CodigoPostal
                        Colonia
                        DireccionGuiaId
                        Cruzamiento
                        CorreoElectronico
                        Discriminator
                        Empresa
                        Estado
                        GuiaId
                        Nombre
                        Numero
                        Pais
                        Referencias
                        Remitente_GuiaId
                        Telefono
                      }
                      TipoProcesoCancelacion
                      TiuiAmigoId
                      TiuiAmigo {
                        TiuiAmigoId
                        RazonSocial
                        Nombres
                        Apellidos
                      }
                      Movimientos {
                        FechaRegistro
                        estatusnuevoname
                        Folio
                        BitacoraGuiaId
                        EstatusNuevo
                        GuiaId
                        Comentarios {
                          GuiaId
                          date_register
                          for
                          id
                          idMotivoCancelacion
                          text
                        }
                        Evidencias {
                          GuiaId
                          address
                          date_register
                          for
                          id
                          latitude
                          longitude
                          mimeType
                          text
                          url
                        }          FechaRegistro
                        estatusnuevoname
                        Folio
                        BitacoraGuiaId
                        EstatusNuevo
                        GuiaId
                        Comentarios {
                          GuiaId
                          date_register
                          for
                          id
                          idMotivoCancelacion
                          text
                          act_user
                        }
                        Evidencias {
                          GuiaId
                          address
                          date_register
                          for
                          id
                          latitude
                          longitude
                          mimeType
                          text
                          url
                          act_user
                        }
                      }
                    }
                  }
                }";
      string variables = "{\"filters\": {\"Folio\": {\"value\": \"" + folio + "\"}}}";
      var response = await this._graphqlService.SendGraphQlRequestAsync(query, variables);
      Ok();
      return Content(response.ToString(), "application/json");
    }
  }
}
