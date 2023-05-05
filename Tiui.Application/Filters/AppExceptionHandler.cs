using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Tiui.Application.DTOs;
using Tiui.Utils.Exceptions;
using Tiui.Utils.StatusCodes;

namespace Tiui.Application.Filters
{
    /// <summary>
    /// Filtro global para el manejo de errores
    /// </summary>
    public class AppExceptionHandler : ExceptionFilterAttribute
    {
        private readonly ILogger<AppExceptionHandler> _logger;

        public AppExceptionHandler(ILogger<AppExceptionHandler> logger)
        {
            this._logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is DataNotFoundException)
                context.Result = new NotFoundObjectResult(new ApiResultModel<object> { Success = false, Status = "404", Message = context.Exception.Message });
            else if (context.Exception is BusinessRuleException)
                context.Result = new BadRequestObjectResult(new ApiResultModel<object> { Success = false, Status = "400", Message = context.Exception.Message });
            else if (context.Exception is AuthException)
                context.Result = new UnauthorizedObjectResult(new ApiResultModel<object> { Success = false, Status = "401", Message = context.Exception.Message });
            else
            {
                context.Result = new InternalServerErrorObjectResult(new ApiResultModel<object> { Success = false, Status = "500", Message = "Error en el servidor. Contacte al administrador del sistema." });
                this._logger.LogError(context.Exception.ToString());
            }
        }
    }
}
