namespace Tiui.Application.DTOs.Guias
{
    public class GuiaMasiveDTO
    {
        public int RemitenteId { get; set; }
        public int TiuiAmigoId { get; set; }
        public List<GuiaCreateDTO> Guias { get; set; }
    }
}
