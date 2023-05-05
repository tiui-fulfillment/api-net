using System.ComponentModel.DataAnnotations;

namespace Tiui.Application.DTOs.Clientes
{
    /// <summary>
    /// DTO para el filtro de la libreta de direcciones
    /// </summary>
    public class LibretaDireccionFiltroDTO
    {
        [Required(ErrorMessage = "El identificador del TiuiAmigo es requerido")]
        public int? TiuiAmigoId { get; set; }
        public string ValorFiltro { get; set; }
    }
}
