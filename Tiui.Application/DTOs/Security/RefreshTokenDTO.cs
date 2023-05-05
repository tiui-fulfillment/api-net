using System.ComponentModel.DataAnnotations;

namespace Tiui.Application.DTOs.Security
{
    public class RefreshTokenDTO
    {
        [Required]
        public int? UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string SessionId { get; set; }
    }
}
