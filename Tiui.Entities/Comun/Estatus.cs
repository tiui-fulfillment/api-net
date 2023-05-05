namespace Tiui.Entities.Comun
{
    /// <summary>
    /// Entidad para el manejo de los estatus de los eventos del sistema
    /// </summary>
    public class Estatus
    {
        public int? EstatusId { get; set; }
        public string Nombre { get; set; }
        public string Proceso { get; set; }
        public string Descripcion { get; set; }
        public ETipoFlujoGuia TipoFlujo { get; set; }
        public bool Activo { get; set; }
    }
}
