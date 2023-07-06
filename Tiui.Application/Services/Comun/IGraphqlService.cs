using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using Tiui.Application.DTOs.Comun;

namespace Tiui.Application.Services.Comun
{
    /// <summary>
    /// Abstracción para el manejo de la configuración de la aplicación
    /// </summary>
    public interface IGraphqlService
    {
        Task<string> SendGraphQlRequestAsync(string query, string variables);
    }
}
