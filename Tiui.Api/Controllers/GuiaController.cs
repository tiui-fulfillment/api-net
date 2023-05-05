using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
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

        public GuiaController(IGuiaService guiaService, IGuiaReport guiaReport, IGuiaStateService guiaStateService, IGuiaCompleteReport guiaCompleteReport
            , IGuiaMasiveService guiaMasiveService)
        {
            this._guiaService = guiaService;
            this._guiaReport = guiaReport;
            this._guiaStateService = guiaStateService;
            this._guiaCompleteReport = guiaCompleteReport;
            this._guiaMasiveService = guiaMasiveService;
        }
        // POST api/<GuiaController>        
        [HttpPost]
        public async Task<ApiResultModel<GuiaCreateDTO>> Post(GuiaCreateDTO guiaCreateDTO)
        {
            return await this._guiaService.Create(guiaCreateDTO);
        }
        [AllowAnonymous]
        [HttpGet, Route("GuiaReport/{guiaId}")]
        public async Task<ActionResult> GetGuiaReport(long guiaId)
        {
            await this._guiaReport.SetData(guiaId);
            return this.File(this._guiaReport.ToPdf(), MediaTypeNames.Application.Pdf, $"guia_{guiaId}.pdf");
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
