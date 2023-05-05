using Tiui.Application.Mail;

namespace Tiui.Mailing
{
    /// <summary>
    /// Flujo para generación de la plantilla del correo electrónico
    /// </summary>
    /// <typeparam name="T">Tipo de plantilla a generar</typeparam>
    public class EMailFluent<T> : IEMailFluent<T> where T : IEmail
    {
        private T _email;
        private List<string> _tos;
        private List<string> _cc;
        private List<string> _bcc;
        private List<string> _attachments;

        public EMailFluent(T email)
        {
            this._email = email;
            this._tos = email.Tos != null ? new List<string>(email.Tos) : new List<string>();
            this._cc = email.Cc != null ? new List<string>(email.Cc) : new List<string>();
            this._bcc = email.Bcc != null ? new List<string>(email.Bcc) : new List<string>();
            this._attachments = email.Attachments != null ? new List<string>(email.Attachments) : new List<string>();
        }
        public IEMailFluent<T> From(string mail)
        {
            this._email.From = mail;
            return this;
        }
        public IEMailFluent<T> Subject(string subject)
        {
            this._email.Subject = subject;
            return this;
        }
        public IEMailFluent<T> Body(string body)
        {
            this._email.Body = body;
            return this;
        }

        public IEMailFluent<T> To(string mail)
        {            
            this._tos.Add(mail);
            this._email.Tos = this._tos.ToArray();
            return this;
        }

        public IEMailFluent<T> Cc(string mail)
        {
            this._cc.Add(mail);
            this._email.Cc = this._cc.ToArray();
            return this;
        }

        public IEMailFluent<T> Bcc(string mail)
        {
            this._bcc.Add(mail);
            this._email.Bcc = this._bcc.ToArray();
            return this;
        }

        public IEMailFluent<T> Attachment(string attachment)
        {
            this._attachments.Add(attachment);
            this._email.Attachments = this._attachments.ToArray();
            return this;
        }
        public T EMail() => this._email;

        public void Reload()
        {
            this._tos = new List<string>();
            this._cc = new List<string>();
            this._bcc = new List<string>();
            this._attachments = new List<string>();
        }
    }
}
