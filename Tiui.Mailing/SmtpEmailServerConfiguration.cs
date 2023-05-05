using Microsoft.Extensions.Configuration;
using Tiui.Application.Mail;

namespace Tiui.Mailing
{
    /// <summary>
    /// Clase para el manejo de la configuración de correo electrónico
    /// </summary>
    public class SmtpEmailServerConfiguration : IEmailServerConfiguration
    {
        private IConfiguration _configuration;
        public SmtpEmailServerConfiguration(IConfiguration configuration) => this._configuration = configuration;
        public string Client => this._configuration["email:configuration:smtpClient"];
        public string Account => this._configuration["email:configuration:userNameEmail"];
        public string Password => this._configuration["email:configuration:passwordEmail"];
        public int Port
        {
            get
            {
                if (int.TryParse(this._configuration["email:configuration:portEmail"], out int port) && port > 0)
                    return port;
                return this.Ssl ? 465 : 587;
            }
        }
        public bool Ssl => bool.Parse(this._configuration["email:configuration:sslEmail"]);
    }
}
