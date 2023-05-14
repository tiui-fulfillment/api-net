using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.DTOs.Paging;
using Tiui.Application.DTOs.Reports;
using Tiui.Application.Reports;
using Tiui.Application.Services.Guias;

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
    public GuiaController(IGuiaService guiaService, IGuiaReport guiaReport, IGuiaStateService guiaStateService, IGuiaCompleteReport guiaCompleteReport
        , IGuiaMasiveService guiaMasiveService, HttpClient httpClient)
    {
      this._guiaService = guiaService;
      this._guiaReport = guiaReport;
      this._guiaStateService = guiaStateService;
      this._guiaCompleteReport = guiaCompleteReport;
      this._guiaMasiveService = guiaMasiveService;
      this._httpClient = httpClient;
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
        }
      ";
      string variables = JsonConvert.SerializeObject(new { folios = new List<string> { guiaId } });

      var request = new HttpRequestMessage(HttpMethod.Post, "https://33ycsx0t9l.execute-api.us-east-1.amazonaws.com");
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
        var fileBytes = await _httpClient.GetByteArrayAsync(filesExistent[0]);
        return File(fileBytes, MediaTypeNames.Application.Pdf, $"guia_{guiaId}.pdf");
      }
      else if (inCreation.Length > 0)
      {
        var fileBytes = await _httpClient.GetByteArrayAsync(inCreation[0].Url);
        return File(fileBytes, MediaTypeNames.Application.Pdf, $"guia_{guiaId}.pdf");
      }

      return File(inCreation[0].Url, MediaTypeNames.Application.Pdf, $"guia_{guiaId}.pdf");

    }

    [AllowAnonymous]
    [HttpGet("{guaId}")]
    public async Task<ActionResult<GuiaDetailDTO>> GetGuia(string guaId)
    {
      return await this._guiaService.GetGuia(guaId);
    }
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
    [Authorize(Roles = "ADMIN")]
    [HttpPut]
    public async Task<ApiResultModel<GuiaUpdateStateDTO>> Put(GuiaUpdateStateDTO guiaUpdateStateDTO)
    {
      return await this._guiaStateService.SetState(guiaUpdateStateDTO);
    }
    [Authorize(Roles = "ADMIN")]
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
    [Authorize(Roles = "ADMIN")]
    [HttpPost, Route("GuiaCompleteReport")]
    public ActionResult GetGuiaCompleteReport(GuiaReportFilterDTO guiaReportFilterDTO)
    {
      this._guiaCompleteReport.SetData(guiaReportFilterDTO);
      return this.File(this._guiaCompleteReport.ToExcel(), MediaTypeNames.Application.Rtf, $"guias.xlsx");
    }
  }
}
