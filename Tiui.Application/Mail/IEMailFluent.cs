namespace Tiui.Application.Mail
{
    /// <summary>
    /// Abstracción para el seteo de la información del correo electrónico
    /// </summary>
    /// <typeparam name="T">Plantilla de correo</typeparam>
    public interface IEMailFluent<T> where T : IEmail
    {
        IEMailFluent<T> From(string mail);
        IEMailFluent<T> Subject(string subject);
        IEMailFluent<T> Body(string body);
        IEMailFluent<T> To(string mail);
        IEMailFluent<T> Cc(string mail);
        IEMailFluent<T> Bcc(string mail);
        IEMailFluent<T> Attachment(string attachment);
        void Reload();
        T EMail();
    }
}
