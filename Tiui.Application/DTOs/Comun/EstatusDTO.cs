namespace Tiui.Application.DTOs.Comun
{
    public class EstatusDTO
   {
        public int? EstatusId { get; set; }
        public string Nombre { get; set; }
        public string Proceso { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
