namespace Tiui.Application.DTOs.Guias
{
    /// <summary>
    /// DTO para el manejo de los paquetes
    /// </summary>
    public class PaqueteGuiaDTO
    {
        public long? PaqueteId { get; set; }
        public float Largo { get; set; }
        public float Alto { get; set; }
        public float Ancho { get; set; }
        public float PesoFisico { get; set; }
        public float PesoCotizado { get; set; }
    }
}
