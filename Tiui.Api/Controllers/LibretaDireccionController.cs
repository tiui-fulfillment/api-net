using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiui.Application.DTOs.Clientes;
using Tiui.Application.Services.Clientes;

namespace Tiui.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LibretaDireccionController : ControllerBase
    {
        private readonly IDireccionService _direccionService;

        public LibretaDireccionController(IDireccionService direccionService)
        {
            this._direccionService = direccionService;
        }
        [HttpPost]
        public async Task<ActionResult<List<LibretaDireccionDTO>>> Get(LibretaDireccionFiltroDTO filtro)
        {
            return await this._direccionService.GetByTiuiAmigo(filtro);
        }
    }
}
