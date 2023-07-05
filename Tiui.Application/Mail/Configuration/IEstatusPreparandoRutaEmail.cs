namespace Tiui.Application.Mail.Configuration
{
    /// <summary>
    /// Abstracción para la plantilla del registro de guias
    /// </summary>
    public interface IEstatusPreparandoRutaEmail : IConfiguracionEmail
    {
        IEstatusPreparandoRutaEmail Destinatario(string destinatario);
        IEstatusPreparandoRutaEmail TiuAmigo(string tiuiAmigo);
        IEstatusPreparandoRutaEmail NumeroGuia(string NumeroGuia);
        IEstatusPreparandoRutaEmail Estatus();
        IEstatusPreparandoRutaEmail UrlWebSite(string url);
        IEstatusPreparandoRutaEmail TextBody();
        IEstatusPreparandoRutaEmail ImagenEstatus();
    }
}
