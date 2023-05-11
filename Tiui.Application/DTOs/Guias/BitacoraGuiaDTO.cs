namespace Tiui.Application.DTOs.Guias
{
    public class BitacoraGuiaDTO
    {
        public string Estatus { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public string Proceso { get; set; }
    }
}
