namespace Tiui.Application.Mail
{
    /// <summary>
    /// Abstraccion para la configuración de correo
    /// </summary>
    public interface IEmailServerConfiguration
    {
        string Client { get; }
        string Account { get; }
        string Password { get; }
        int Port { get; }
        bool Ssl { get; }
    }
}
