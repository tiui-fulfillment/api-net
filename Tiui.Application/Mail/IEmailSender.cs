namespace Tiui.Application.Mail
{
    /// <summary>
    /// Abstracción para el manejador de correo electónico
    /// </summary>
    public interface IEmailSender
    {
        bool SendEmail(IEmail email);
        Task<bool> SendEmailAsync(IEmail email);
    }
}
