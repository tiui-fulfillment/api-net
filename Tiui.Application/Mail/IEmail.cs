namespace Tiui.Application.Mail
{
    /// <summary>
    /// Abstracción para el manejo de un correo electrónico
    /// </summary>
    public interface IEmail
    {
        string From { get; set; }
        string Subject { get; set; }
        string[] Tos { get; set; }
        string Body { get; set; }
        string[] Cc { get; set; }
        string[] Bcc { get; set; }
        string[] Attachments { get; set; }
    }
}
