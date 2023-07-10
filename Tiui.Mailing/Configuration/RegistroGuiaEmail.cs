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
    /// Clase para el manejo de la configuración del correo para el registro de guías
    /// </summary>
    public class RegistroGuiaEmail : IRegistroGuiaEmail
    {
        private IEMailFluent<IEmail> _emailFluent;
        private IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private string body;

        public RegistroGuiaEmail(IEMailFluent<IEmail> emailFluent, IEmailSender emailSender, IConfiguration configuration)
        {
            _emailFluent = emailFluent;
            _emailSender = emailSender;
            this._configuration = configuration;
            this.loadTemplate();
            this.Estatus();
            this.TextBody();
            this.ImagenEstatus();
        }
        /// <summary>
        /// Carga la plantilla del correo
        /// </summary>
        private void loadTemplate()
        {
            string path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + this._configuration["email:registroGuia:pathTemplate"];
            this.body = File.ReadAllText(path, Encoding.UTF8);
        }

        public string Template { get => body; }
        public string To { get; set; }
        public IRegistroGuiaEmail Destinatario(string destinatario)
        {
            this.body = this.body.Replace("{destinatario}", destinatario);
            return this;
        }

        public IRegistroGuiaEmail NumeroGuia(string NumeroGuia)
        {
            this.body = this.body.Replace("{numero_guia}", NumeroGuia);
            return this;
        }

        public IRegistroGuiaEmail TiuAmigo(string tiuiAmigo)
        {
            this.body = this.body.Replace("{tiui_amigo}", tiuiAmigo);
            return this;
        }
                public IRegistroGuiaEmail Estatus()
        {
            this.body = this.body.Replace("{estatus}", this._configuration["email:registroguia:subject"]);
            return this;
        }
        public IRegistroGuiaEmail TextBody()
        {
            this.body = this.body.Replace("{text_body}", this._configuration["email:registroguia:textBody"]);
            return this;
        }

        public IRegistroGuiaEmail ImagenEstatus()
        {
            this.body = this.body.Replace("{imagen_estatus}", this._configuration["email:registroguia:imagen_estatus"]);
            return this;
        }

        public IRegistroGuiaEmail UrlWebSite(string url)
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
            this._emailFluent.To(this.To).Subject(this._configuration["email:registroguia:subject"]).Body(this.body);
            return await this._emailSender.SendEmailAsync(this._emailFluent.EMail());
        }
        public void Reload()
        {
            this.loadTemplate();
            this._emailFluent.Reload();
        }
    }
}
