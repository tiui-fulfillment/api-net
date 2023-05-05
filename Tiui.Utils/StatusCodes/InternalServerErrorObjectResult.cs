using Microsoft.AspNetCore.Mvc;

namespace Tiui.Utils.StatusCodes
{
    /// <summary>
    /// Para el manejo de los código de estado personalizados
    /// </summary>
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            this.StatusCode = 500;
            this.Value = value;
        }
    }
}
