using Tiui.Application.Mail;

namespace Tiui.Mailing
{
    /// <summary>
    /// Configuración de correo electrónico
    /// </summary>
    public class Email : IEmail
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string[] Tos { get; set; }
        public string Body { get; set; }
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public string[] Attachments { get; set; }      
    }
}
