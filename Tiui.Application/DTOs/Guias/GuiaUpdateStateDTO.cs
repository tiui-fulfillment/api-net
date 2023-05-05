using System.ComponentModel.DataAnnotations;
using Tiui.Entities.Guias;

namespace Tiui.Application.DTOs.Guias
{
    /// <summary>
    /// DTO para la actualización de los estatus de la guía
    /// </summary>
    public class GuiaUpdateStateDTO
    {       
        public long? GuiaId { get; set; }
        public int? MotivoCancelacionId { get; set; }
        public DateTime? FechaReagendado { get; set; }
        public DateTime? FechaConciliacion { get; set; }
        [Required(ErrorMessage = "El estatus a asignar a la guía es requerido")]
        public EEstatusGuia NuevoEstatus { get; set; }
    }
}
