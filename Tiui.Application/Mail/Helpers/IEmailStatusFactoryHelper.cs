using Tiui.Application.Mail.Configuration;
using Tiui.Entities.Guias;

namespace Tiui.Application.Mail.Helpers
{
    public interface IEmailStatusFactoryHelper
    {
        public IConfiguracionEmail CreateEmailConfiguration(Guia guia);
    }
}
