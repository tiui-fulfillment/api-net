namespace Tiui.Application.Mail.Configuration
{    
    public interface IEstatusEntregadoEmail : IConfiguracionEmail
    {
        IEstatusEntregadoEmail Destinatario(string destinatario);
        IEstatusEntregadoEmail NumeroGuia(string NumeroGuia);
    }
}
