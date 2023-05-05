namespace Tiui.Application.Mail.Configuration
{
    /// <summary>
    /// Abstración general para la plantilla de correos
    /// </summary>
    public interface IConfiguracionEmail
    {
        string Template { get; }
        string To { get; set; }
        void Reload();
        Task<bool> SendMailAsync();
    }
}
