using Microsoft.Extensions.Configuration;
using Tiui.Application.Mail.Configuration;
using Tiui.Application.Mail.Helpers;
using Tiui.Entities.Guias;

namespace Tiui.Mailing.Configuration.Helpers
{
    public class EmailStatusFactoryHelper : IEmailStatusFactoryHelper
    {
        private readonly IEstatusCaminoCDMXEmail _estatusCaminoCDMXEmail;
        private readonly IEstatusEnCedisCDMXEmail _estatusEnCedisCDMXEmail;
        private readonly IEstatusEntregadoEmail _estatusEntregadoEmail;
        private readonly IEstatusReagendadoEmail _estatusReagendadoEmail;
        private readonly IEstatusRutaEntregaEmail _estatusRutaEntregaEmail;
        private readonly IConfiguration _configuration;

        public EmailStatusFactoryHelper(IEstatusCaminoCDMXEmail estatusCaminoCDMXEmail, IEstatusEnCedisCDMXEmail estatusEnCedisCDMXEmail,
            IEstatusEntregadoEmail estatusEntregadoEmail, IEstatusReagendadoEmail estatusReagendadoEmail, IEstatusRutaEntregaEmail estatusRutaEntregaEmail
            , IConfiguration configuration)
        {
            this._estatusCaminoCDMXEmail = estatusCaminoCDMXEmail;
            this._estatusEnCedisCDMXEmail = estatusEnCedisCDMXEmail;
            this._estatusEntregadoEmail = estatusEntregadoEmail;
            this._estatusReagendadoEmail = estatusReagendadoEmail;
            this._estatusRutaEntregaEmail = estatusRutaEntregaEmail;
            this._configuration = configuration;
        }
        public IConfiguracionEmail CreateEmailConfiguration(Guia guia)
        {
            switch ((EEstatusGuia)guia.EstatusId)
            {
                case EEstatusGuia.CAMINO_CEDIS_CDMX:
                    this._estatusCaminoCDMXEmail.Reload();
                    this._estatusCaminoCDMXEmail.To = guia.Destinatario.CorreoElectronico;
                    this._estatusCaminoCDMXEmail.Destinatario(guia.Destinatario.Nombre).NumeroGuia(guia.Folio)
                        .UrlWebSite($"{this._configuration["UrlWebSiteDetalleGuia"]}/{guia.Folio}");
                    return this._estatusCaminoCDMXEmail;
                case EEstatusGuia.EN_CEDIS_CDMX:
                case EEstatusGuia.FULFILLMENT_EN_CEDIS_CDMX:
                    this._estatusEnCedisCDMXEmail.Reload();
                    this._estatusEnCedisCDMXEmail.To = guia.Destinatario.CorreoElectronico;
                    this._estatusEnCedisCDMXEmail.Destinatario(guia.Destinatario.Nombre).NumeroGuia(guia.Folio)
                        .UrlWebSite($"{this._configuration["UrlWebSiteDetalleGuia"]}/{guia.Folio}");
                    return this._estatusEnCedisCDMXEmail;
                case EEstatusGuia.RUTA_ENTREGA:
                    this._estatusRutaEntregaEmail.Reload();
                    this._estatusRutaEntregaEmail.To = guia.Destinatario.CorreoElectronico;
                    this._estatusRutaEntregaEmail.Destinatario(guia.Destinatario.Nombre).NumeroGuia(guia.Folio)
                        .TiuAmigo(guia.TiuiAmigo.GetNombreCompleto());
                    return this._estatusRutaEntregaEmail;
                case EEstatusGuia.REAGENDADO:
                    this._estatusReagendadoEmail.Reload();
                    this._estatusReagendadoEmail.To = guia.Destinatario.CorreoElectronico;
                    this._estatusReagendadoEmail.Destinatario(guia.Destinatario.Nombre).NumeroGuia(guia.Folio)
                        .FechaEntrega(guia.FechaReagendado.Value);
                    return this._estatusReagendadoEmail;
                case EEstatusGuia.ENTREGAGO:
                    this._estatusEntregadoEmail.Reload();
                    this._estatusEntregadoEmail.To = guia.Destinatario.CorreoElectronico;
                    this._estatusEntregadoEmail.Destinatario(guia.Destinatario.Nombre).NumeroGuia(guia.Folio); 
                    return this._estatusEntregadoEmail;
                default:
                    return null;
            }
        }
    }
}
