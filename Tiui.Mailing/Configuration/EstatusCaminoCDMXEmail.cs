using Microsoft.Extensions.Configuration;
using System.Text;
using Tiui.Application.Mail;
using Tiui.Application.Mail.Configuration;

namespace Tiui.Mailing.Configuration
{   
    public class EstatusCaminoCDMXEmail : IEstatusCaminoCDMXEmail
    {
        private IEMailFluent<IEmail> _emailFluent;
        private IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private string body;

        public EstatusCaminoCDMXEmail(IEMailFluent<IEmail> emailFluent, IEmailSender emailSender, IConfiguration configuration)
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
            string path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + this._configuration["email:pathTemplate:caminoCDMX"];
            this.body = File.ReadAllText(path, Encoding.UTF8);
        }

        public string Template { get => body; }
        public string To { get; set; }
        public IEstatusCaminoCDMXEmail Destinatario(string destinatario)
        {
            this.body = this.body.Replace("{destinatario}", destinatario);
            return this;
        }

        public IEstatusCaminoCDMXEmail NumeroGuia(string NumeroGuia)
        {
            this.body = this.body.Replace("{numero_guia}", NumeroGuia);
            return this;
        }      

        public IEstatusCaminoCDMXEmail UrlWebSite(string url)
        {
            this.body = this.body.Replace("{url_detalle_guia}", url);
            return this;
        }
        /// <summary>
        /// Envía el correo de forma asincrona
        /// </summary>
        /// <returns>Envia en correo electronico</returns>
        public async Task<bool> SendMailAsync()
        {           
            this._emailFluent.To(this.To).Subject(this._configuration["email:subject:caminoCDMX"]).Body(this.body);
            return await this._emailSender.SendEmailAsync(this._emailFluent.EMail());
        }

        public void Reload()
        {
            this.loadTemplate();
            this._emailFluent.Reload();
        }
    }
}
