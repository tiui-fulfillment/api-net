using System.ComponentModel.DataAnnotations;
using Tiui.Entities.Guias;

namespace Tiui.Application.DTOs.Guias
{
    /// <summary>
    /// DTO para la actualización de los estatus de la guía
    /// </summary>
    public class GuiaUpdateCancelationDTO
    {
        public string Folio { get; set; }
        [Required(ErrorMessage = "El Motivo de Cancelacion asignar a la guía es requerido")]

        public int MotivoCancelacionId { get; set; }
    }
}
