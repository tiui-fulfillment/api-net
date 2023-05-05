namespace Tiui.Application.Mail.Configuration
{
    public interface IRecuperarContraseñaEmail : IConfiguracionEmail
    {
        IRecuperarContraseñaEmail Codigo(string codigo);    
    }
}
