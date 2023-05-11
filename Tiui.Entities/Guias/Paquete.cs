using Tiui.Entities.Comun;

namespace Tiui.Entities.Guias
{
    /// <summary>
    /// Entidad para el manejo de los paquetes
    /// </summary>
    public class Paquete
    {
        public long? PaqueteId { get; set; }
        public float Largo { get; set; }
        public float Alto { get; set; }
        public float Ancho { get; set; }
        public float PesoFisico { get; set; }
        public float PesoCotizado { get; set; }        
        public EvidenciaEntrega EvidenciaEntrega { get; set; }
        public int? EvidenciaEntregaId { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public Guia Guia { get; set; }
        /// <summary>
        /// Obtiene las descripción de las medidas del paquete
        /// </summary>
        /// <returns></returns>
        public string GetMedidas()
        {
            return $"Alto: {this.Alto}, Ancho: {this.Ancho}, Largo: {this.Largo}";
        }

        /// <summary>
        /// Obtiene el peso volumetrico del paquete
        /// </summary>
        /// <returns>Retorna el valor obtenido</returns>
        public float ObtenerPesoVolumetrico()
        {
            return (this.Alto * this.Ancho * this.Largo) / 5000;
        }
    }
}
