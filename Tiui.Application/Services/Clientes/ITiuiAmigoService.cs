using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiui.Application.DTOs.Clientes;

namespace Tiui.Application.Services.Clientes
{
    /// <summary>
    /// Abstracción para el servicio de los tiuiamigos
    /// </summary>
    public interface ITiuiAmigoService
    {
        public Task<List<TiuiAmigoSelectDTO>> GetAll();
    }
}
