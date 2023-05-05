using Tiui.Entities.Comun;

namespace Tiui.Entities.Guias
{
    /// <summary>
    /// Entidad para el manejo de los eventos de la guía
    /// </summary>
    public class BitacoraGuia
    {
        public long? BitacoraGuiaId { get; set; }
        public EEstatusGuia EstatusAnterior { get; set; }
        public EEstatusGuia EstatusNuevo { get; set; }       
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public Guia Guia { get; set; }
        public long? GuiaId { get; set; }
    }
}
