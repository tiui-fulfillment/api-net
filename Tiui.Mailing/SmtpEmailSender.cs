using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Tiui.Application.Mail;

namespace Tiui.Mailing
{
    /// <summary>
    /// Manejador de correo electónico de tipo SmtpEmail
    /// </summary>
    public class SmtpEmailSender : IEmailSender
    {
        private IEmailServerConfiguration emailServerConfiguration;
        public SmtpEmailSender(IEmailServerConfiguration emailServerConfiguration) => this.emailServerConfiguration = emailServerConfiguration;
        /// <summary>
        /// Envía el correo electrónico de forma sincrona
        /// </summary>
        /// <param name="email">Contiene la configuración de la plantilla</param>
        /// <returns>Retorna verdadero si el envío del correo electrónico es exitoso</returns>
        public bool SendEmail(IEmail email)
        {
            using (SmtpClient smtpClient = new SmtpClient(this.emailServerConfiguration.Client, this.emailServerConfiguration.Port))
            {
                using (MailMessage message = new MailMessage())
                {
                    if (!string.IsNullOrEmpty(this.emailServerConfiguration.Account))
                        message.From = new MailAddress(this.emailServerConfiguration.Account);
                    if (!string.IsNullOrEmpty(email.From))
                        message.From = new MailAddress(email.From);
                    message.Subject = email.Subject;
                    message.Body = email.Body;
                    if (email.Tos != null)
                    {
                        foreach (string to in email.Tos)
                            message.To.Add(to);
                    }
                    if (email.Cc != null)
                    {
                        foreach (string addresses in email.Cc)
                            message.CC.Add(addresses);
                    }
                    if (email.Bcc != null)
                    {
                        foreach (string addresses in email.Bcc)
                            message.Bcc.Add(addresses);
                    }
                    if (email.Attachments != null)
                    {
                        foreach (string attachment in email.Attachments)
                            message.Attachments.Add(new Attachment(attachment));
                    }
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    message.IsBodyHtml = true;
                    smtpClient.Credentials = new NetworkCredential(this.emailServerConfiguration.Account, this.emailServerConfiguration.Password);
                    smtpClient.EnableSsl = this.emailServerConfiguration.Ssl;
                    smtpClient.Send(message);
                    return true;
                }
            }
        }
        /// <summary>
        /// Envía el correo electrónico de forma asincrona
        /// </summary>
        /// <param name="email">Contiene la configuración de la plantilla</param>
        /// <returns>Retorna verdadero si el envío del correo electrónico es exitoso</returns>
        public async Task<bool> SendEmailAsync(IEmail email)
        {           
            using (SmtpClient smtpServer = new SmtpClient(this.emailServerConfiguration.Client, this.emailServerConfiguration.Port))
            {
                using (MailMessage mail = new MailMessage())
                {
                    if (!string.IsNullOrEmpty(this.emailServerConfiguration.Account))
                        mail.From = new MailAddress(this.emailServerConfiguration.Account);
                    if (!string.IsNullOrEmpty(email.From))
                        mail.From = new MailAddress(email.From);
                    mail.Subject = email.Subject;
                    mail.Body = email.Body;                  
                    if (email.Tos != null)
                    {
                        foreach (string to in email.Tos)
                            mail.To.Add(to);

                    }
                    if (email.Cc != null)
                    {
                        foreach (string addresses in email.Cc)
                            mail.CC.Add(addresses);
                    }
                    if (email.Bcc != null)
                    {
                        foreach (string addresses in email.Bcc)
                            mail.Bcc.Add(addresses);

                    }
                    if (email.Attachments != null)
                    {
                        foreach (string attachment in email.Attachments)
                            mail.Attachments.Add(new Attachment(attachment));
                    }
                    smtpServer.UseDefaultCredentials = false;
                    smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mail.IsBodyHtml = true;
                    smtpServer.Credentials = new NetworkCredential(this.emailServerConfiguration.Account, this.emailServerConfiguration.Password);
                    smtpServer.EnableSsl = this.emailServerConfiguration.Ssl;
                    await smtpServer.SendMailAsync(mail);                    
                }
            }
            return true;
        }
    }
}
