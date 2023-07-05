namespace Tiui.Application.Mail.Configuration
{
    /// <summary>
    /// Abstracción para la plantilla del registro de guias
    /// </summary>
    public interface IEstatusIntentoDeEntregaEmail : IConfiguracionEmail
    {
        IEstatusIntentoDeEntregaEmail Destinatario(string destinatario);
        IEstatusIntentoDeEntregaEmail TiuAmigo(string tiuiAmigo);
        IEstatusIntentoDeEntregaEmail NumeroGuia(string NumeroGuia);
        IEstatusIntentoDeEntregaEmail Estatus();
        IEstatusIntentoDeEntregaEmail UrlWebSite(string url);
        IEstatusIntentoDeEntregaEmail TextBody();
        IEstatusIntentoDeEntregaEmail ImagenEstatus();
    }
}
