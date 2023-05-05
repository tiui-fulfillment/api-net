namespace Tiui.Application.Mail.Configuration
{
    /// <summary>
    /// Abstracción para la plantilla del registro de guias
    /// </summary>
    public interface IRegistroGuiaEmail : IConfiguracionEmail
    {
        IRegistroGuiaEmail Destinatario(string destinatario);
        IRegistroGuiaEmail TiuAmigo(string tiuiAmigo);
        IRegistroGuiaEmail UrlWebSite(string url);
        IRegistroGuiaEmail NumeroGuia(string NumeroGuia);
    }
}
