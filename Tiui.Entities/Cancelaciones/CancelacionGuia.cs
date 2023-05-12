using System;
using Tiui.Entities.Guias;

namespace Tiui.Entities.Cancelaciones
{
    /// <summary>
    /// Entidad para el manejo de las cancelaciones de la guia
    /// </summary>
    public class CancelacionGuia
    {
        public int? CancelacionGuiaId { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public string Observacion { get; set; }
        public long? GuiaId { get; set; }
        public Guia Guia { get; set; }
        public int? MotivoCancelacionId { get; set; }
        public MotivoCancelacion MotivoCancelacion { get; set; }
    }
}
