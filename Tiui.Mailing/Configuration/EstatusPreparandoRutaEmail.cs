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
    public class EstatusPreparandoRutaEmail : IEstatusPreparandoRutaEmail
    {
        private IEMailFluent<IEmail> _emailFluent;
        private IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private string body;

        public EstatusPreparandoRutaEmail(IEMailFluent<IEmail> emailFluent, IEmailSender emailSender, IConfiguration configuration)
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
            string path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + this._configuration["email:preparando_ruta:pathTemplate"];
            this.body = File.ReadAllText(path, Encoding.UTF8);
            this.Estatus();
            this.TextBody();
            this.ImagenEstatus();
        }

        public string Template { get => body; }
        public string To { get; set; }
        public IEstatusPreparandoRutaEmail Destinatario(string destinatario)
        {
            this.body = this.body.Replace("{destinatario}", destinatario);
            return this;
        }

        public IEstatusPreparandoRutaEmail NumeroGuia(string NumeroGuia)
        {
            this.body = this.body.Replace("{numero_guia}", NumeroGuia);
            return this;
        }
        public IEstatusPreparandoRutaEmail Estatus()
        {
            this.body = this.body.Replace("{estatus}", this._configuration["email:preparando_ruta:subject"]);
            return this;
        }
        public IEstatusPreparandoRutaEmail TextBody()
        {
            this.body = this.body.Replace("{text_body}", this._configuration["email:preparando_ruta:textBody"]);
            return this;
        }

        public IEstatusPreparandoRutaEmail UrlWebSite(string url)
        {
            this.body = this.body.Replace("{url_detalle_guia}", url);
            return this;
        }
        public IEstatusPreparandoRutaEmail ImagenEstatus()
        {
            this.body = this.body.Replace("{imagen_estatus}", this._configuration["email:preparando_ruta:imagen_estatus"]);
            return this;
        }

        public IEstatusPreparandoRutaEmail TiuAmigo(string tiuiAmigo)
        {
            this.body = this.body.Replace("{tiui_amigo}", tiuiAmigo);
            return this;
        }
        /// <summary>
        /// Envía el correo de forma asincrona
        /// </summary>
        /// <returns>Envia en correo electronico</returns>
        public async Task<bool> SendMailAsync()
        {
            string subject = "Cambio de Estatus: " + this._configuration["email:preparando_ruta:subject"];
            this._emailFluent.To(this.To).Subject(subject).Body(this.body);
            return await this._emailSender.SendEmailAsync(this._emailFluent.EMail());
        }
        public void Reload()
        {
            this.loadTemplate();
            this._emailFluent.Reload();
        }
    }
}
