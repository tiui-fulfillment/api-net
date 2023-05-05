using Tiui.Application.DTOs.Cancelaciones;
using Tiui.Entities.Cancelaciones;

namespace Tiui.Application.Services.Cancelaciones
{
    /// <summary>
    /// Abstracción para el servicio de motivos de cancelación
    /// </summary>
    public interface IMotivoCancelacionService
    {
        public Task<List<MotivoCancelacionDTO>> GetMotivosByProceso(int tipoCancelacion);
    }
}
