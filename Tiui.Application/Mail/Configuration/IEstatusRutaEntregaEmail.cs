namespace Tiui.Application.Mail.Configuration
{
    /// <summary>
    /// Abstracción para la plantilla del registro de guias
    /// </summary>
    public interface IEstatusRutaEntregaEmail : IConfiguracionEmail
    {
        IEstatusRutaEntregaEmail Destinatario(string destinatario);
        IEstatusRutaEntregaEmail TiuAmigo(string tiuiAmigo);
        IEstatusRutaEntregaEmail NumeroGuia(string NumeroGuia);
    }
}
