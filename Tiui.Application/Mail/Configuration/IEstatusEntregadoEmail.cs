namespace Tiui.Application.Mail.Configuration
{
    public interface IEstatusEntregadoEmail : IConfiguracionEmail
    {
        IEstatusEntregadoEmail Destinatario(string destinatario);
        IEstatusEntregadoEmail TiuAmigo(string tiuiAmigo);
        IEstatusEntregadoEmail NumeroGuia(string NumeroGuia);
        IEstatusEntregadoEmail Estatus();
        IEstatusEntregadoEmail UrlWebSite(string url);
        IEstatusEntregadoEmail TextBody();
        IEstatusEntregadoEmail ImagenEstatus();
    }
}
