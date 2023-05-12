using System;
using Tiui.Entities.Comun;

namespace Tiui.Entities.Guias
{
    /// <summary>
    /// Entidad para el manejo de la evidencias de la entrega
    /// </summary>
    public class EvidenciaEntrega
    {
        public long? EvidenciaEntregaId { get; set; }
        public string PersonaRecibe { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow.ToUniversalTime();
        public Archivo Foto { get; set; }
        public Archivo Firma { get; set; }
        public Paquete Paquete { get; set; }
        public long? PaqueteId { get; set; }
    }
}
