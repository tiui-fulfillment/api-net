namespace Tiui.Application.Mail.Configuration
{
    /// <summary>
    /// Abstracción para la plantilla del registro de guias
    /// </summary>
    public interface IEstatusNoVisitadoEmail : IConfiguracionEmail
    {
        IEstatusNoVisitadoEmail Destinatario(string destinatario);
        IEstatusNoVisitadoEmail TiuAmigo(string tiuiAmigo);
        IEstatusNoVisitadoEmail NumeroGuia(string NumeroGuia);
        IEstatusNoVisitadoEmail Estatus();
        IEstatusNoVisitadoEmail UrlWebSite(string url);
        IEstatusNoVisitadoEmail TextBody();
        IEstatusNoVisitadoEmail ImagenEstatus();
    }
}
