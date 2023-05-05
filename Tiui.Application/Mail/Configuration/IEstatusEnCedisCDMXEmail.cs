namespace Tiui.Application.Mail.Configuration
{   
    public interface IEstatusEnCedisCDMXEmail : IConfiguracionEmail
    {
        IEstatusEnCedisCDMXEmail Destinatario(string destinatario);
        IEstatusEnCedisCDMXEmail UrlWebSite(string url);
        IEstatusEnCedisCDMXEmail NumeroGuia(string NumeroGuia);
    }
}
