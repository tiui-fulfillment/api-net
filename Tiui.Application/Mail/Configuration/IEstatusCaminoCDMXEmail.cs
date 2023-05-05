namespace Tiui.Application.Mail.Configuration
{   
    public interface IEstatusCaminoCDMXEmail : IConfiguracionEmail
    {
        IEstatusCaminoCDMXEmail Destinatario(string destinatario);
        IEstatusCaminoCDMXEmail UrlWebSite(string url);
        IEstatusCaminoCDMXEmail NumeroGuia(string NumeroGuia);
    }
}
