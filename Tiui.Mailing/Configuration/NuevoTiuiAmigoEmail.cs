using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tiui.Application.Mail;
using Tiui.Application.Mail.Configuration;

namespace Tiui.Mailing.Configuration
{
    /// <summary>
    /// Menejador del correo para nuevos tiuiamigos
    /// </summary>
    public class NuevoTiuiAmigoEmail : INuevoTiuiAmigoEmail
    {
        private IEMailFluent<IEmail> _emailFluent;
        private IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private string body;

        public NuevoTiuiAmigoEmail(IEMailFluent<IEmail> emailFluent, IEmailSender emailSender, IConfiguration configuration)
        {
            _emailFluent = emailFluent;
            _emailSender = emailSender;
            this._configuration = configuration;
            this.loadTemplate();
        }
        /// <summary>
        /// Carga la plantilla del correo
        /// </summary>
        private void loadTemplate()
        {
            string path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + this._configuration["email:pathTemplate:nuevoTiuiAmigo"];
            this.body = File.ReadAllText(path, Encoding.UTF8);
        }

        public string Template { get => body; }
        public string To { get; set; }
        public INuevoTiuiAmigoEmail TiuAmigo(string tiuiAmigo)
        {
            this.body = this.body.Replace("{TiuiAmigo}", tiuiAmigo);
            return this;
        }

        public INuevoTiuiAmigoEmail UrlWebSite(string url)
        {
            this.body = this.body.Replace("{url_sitioweb}", url);
            return this;
        }     
        /// <summary>
        /// Envía el correo de forma asincrona
        /// </summary>
        /// <returns>Envia en correo electronico</returns>
        public async Task<bool> SendMailAsync()
        {
            this._emailFluent.To(this.To).Subject(this._configuration["email:subject:nuevoTiuiAmigo"]).Body(this.body);
            return await this._emailSender.SendEmailAsync(this._emailFluent.EMail());
        }
        public void Reload()
        {
            this.loadTemplate();
            this._emailFluent.Reload();
        }
    }
}
