namespace Tiui.Application.DTOs.Clientes
{
    public class TiuiAmigoSelectDTO
    {
        public int? TiuiAmigoId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto { get { return $"{Nombres} {Apellidos}"; } }
    }
}
