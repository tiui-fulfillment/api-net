using Tiui.Entities.Cancelaciones;

namespace Tiui.Application.DTOs.Cancelaciones
{
    public class MotivoCancelacionDTO
    {
        public int MotivoCancelacionId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public ETipoCancelacion TipoCancelacion { get; set; }
    }
}
