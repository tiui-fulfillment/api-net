namespace Tiui.Application.Mail.Configuration
{    
    public interface IEstatusReagendadoEmail : IConfiguracionEmail
    {
        IEstatusReagendadoEmail Destinatario(string destinatario);
        IEstatusReagendadoEmail NumeroGuia(string NumeroGuia);
        IEstatusReagendadoEmail FechaEntrega(DateTime fechaEntrega);
    }
}
