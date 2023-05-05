namespace Tiui.Application.DTOs.Security
{
    public class UserRecoveryPasswordDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Code { get; set; }
        public bool ChangePassword { get; set; } = false;
    }
}
