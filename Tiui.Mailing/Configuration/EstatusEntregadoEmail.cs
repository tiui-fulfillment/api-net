using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tiui.Application.Mail;
using Tiui.Application.Mail.Configuration;

namespace Tiui.Mailing.Configuration
{   
    public class EstatusEntregadoEmail : IEstatusEntregadoEmail
    {
        private IEMailFluent<IEmail> _emailFluent;
        private IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private string body;

        public EstatusEntregadoEmail(IEMailFluent<IEmail> emailFluent, IEmailSender emailSender, IConfiguration configuration)
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
            string path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + this._configuration["email:entregado:pathTemplate"];
            this.body = File.ReadAllText(path, Encoding.UTF8);
            this.Estatus();
            this.TextBody();
            this.ImagenEstatus();

        }

        public string Template { get => body; }
        public string To { get; set; }
        public IEstatusEntregadoEmail Destinatario(string destinatario)
        {
            this.body = this.body.Replace("{destinatario}", destinatario);
            return this;
        }

        public IEstatusEntregadoEmail NumeroGuia(string NumeroGuia)
        {
            this.body = this.body.Replace("{numero_guia}", NumeroGuia);
            return this;
        }             
        /// <summary>
        /// Envía el correo de forma asincrona
        /// </summary>
        /// <returns>Envia en correo electronico</returns>
        public IEstatusEntregadoEmail Estatus()
        {
            this.body = this.body.Replace("{estatus}", this._configuration["email:entregado:subject"]);
            return this;
        }
        public IEstatusEntregadoEmail TextBody()
        {
            this.body = this.body.Replace("{text_body}", this._configuration["email:entregado:textBody"]);
            return this;
        }

        public IEstatusEntregadoEmail UrlWebSite(string url)
        {
            this.body = this.body.Replace("{url_detalle_guia}", url);
            return this;
        }
        public IEstatusEntregadoEmail ImagenEstatus()
        {
            this.body = this.body.Replace("{imagen_estatus}", this._configuration["email:intento_de_entrega:imagen_estatus"]);
            return this;
        }

        public IEstatusEntregadoEmail TiuAmigo(string tiuiAmigo)
        {
            this.body = this.body.Replace("{tiui_amigo}", tiuiAmigo);
            return this;
        }
        public async Task<bool> SendMailAsync()
        {
            this._emailFluent.To(this.To).Subject(this._configuration["email:entregado:subject"]).Body(this.body);
            return await this._emailSender.SendEmailAsync(this._emailFluent.EMail());
        }
        public void Reload()
        {
            this.loadTemplate();
            this._emailFluent.Reload();
        }
    }
}
