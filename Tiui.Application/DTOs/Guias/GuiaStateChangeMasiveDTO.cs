
namespace Tiui.Application.DTOs.Guias
{
    /// <summary>
    /// DTO para el cambio de estatus masivo de guias
    /// </summary>
    public class GuiaStateChangeMasiveDTO
    {
        public GuiaFilterDTO Filter { get; set; }
        public GuiaUpdateStateDTO Parameters { get; set; }
        public bool PorFiltro { get; set; }
        public List<long> Guias { get; set; }
    }
}
