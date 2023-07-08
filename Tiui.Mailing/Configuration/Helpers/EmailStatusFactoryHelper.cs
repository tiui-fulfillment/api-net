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
        private readonly IEstatusNoVisitadoEmail _estatusReagendadoEmail;
        private readonly IEstatusRutaEntregaEmail _estatusRutaEntregaEmail;
        private readonly IEstatusPreparandoRutaEmail _estatusPreparandoRutaEmail;
        private readonly IEstatusNoVisitadoEmail _estatusNoVisitadoEmail;
        private readonly IEstatusIntentoDeEntregaEmail _estatusIntentoDeEntregaEmail;
        private readonly IConfiguration _configuration;

        public EmailStatusFactoryHelper(IEstatusCaminoCDMXEmail estatusCaminoCDMXEmail, IEstatusEnCedisCDMXEmail estatusEnCedisCDMXEmail,
            IEstatusEntregadoEmail estatusEntregadoEmail, IEstatusNoVisitadoEmail estatusReagendadoEmail, IEstatusRutaEntregaEmail estatusRutaEntregaEmail, IEstatusPreparandoRutaEmail estatusPreparandoRutaEmail
            ,
            IEstatusNoVisitadoEmail estatusNoVisitadoEmail, IEstatusIntentoDeEntregaEmail estatusIntentoDeEntregaEmail,
             IConfiguration configuration)
        {
            this._estatusCaminoCDMXEmail = estatusCaminoCDMXEmail;
            this._estatusEnCedisCDMXEmail = estatusEnCedisCDMXEmail;
            this._estatusEntregadoEmail = estatusEntregadoEmail;
            this._estatusReagendadoEmail = estatusReagendadoEmail;
            this._estatusRutaEntregaEmail = estatusRutaEntregaEmail;
            this._estatusPreparandoRutaEmail = estatusPreparandoRutaEmail;
            this._estatusNoVisitadoEmail = estatusNoVisitadoEmail;
            this._estatusIntentoDeEntregaEmail = estatusIntentoDeEntregaEmail;

            this._configuration = configuration;
        }
        public IConfiguracionEmail CreateEmailConfiguration(Guia guia)
        {
            switch ((EEstatusGuia)guia.EstatusId)
            {
                /* case EEstatusGuia.CAMINO_CEDIS_CDMX:
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
                 */
                 case EEstatusGuia.RUTA_ENTREGA:
                    this._estatusRutaEntregaEmail.Reload();
                    this._estatusRutaEntregaEmail.To = guia.Destinatario.CorreoElectronico;
                    this._estatusRutaEntregaEmail.Destinatario(guia.Destinatario.Nombre)
                    .NumeroGuia(guia.Folio)
                        .TiuAmigo(guia.Remitente.Nombre)
                        .UrlWebSite($"{this._configuration["UrlWebSiteDetalleGuia"]}/{guia.Folio}"); ;
                    return this._estatusRutaEntregaEmail;
                case EEstatusGuia.PREPARANDO_RUTA:
                    this._estatusPreparandoRutaEmail.Reload();
                    this._estatusPreparandoRutaEmail.To = guia.Destinatario.CorreoElectronico;
                    this._estatusPreparandoRutaEmail.Destinatario(guia.Destinatario.Nombre)
                    .NumeroGuia(guia.Folio)
                        .TiuAmigo(guia.Remitente.Nombre)
                        .UrlWebSite($"{this._configuration["UrlWebSiteDetalleGuia"]}/{guia.Folio}"); ;
                    return this._estatusPreparandoRutaEmail;
                case EEstatusGuia.NO_VISITADO:
                    this._estatusNoVisitadoEmail.Reload();
                    this._estatusNoVisitadoEmail.To = guia.Destinatario.CorreoElectronico;
                    this._estatusNoVisitadoEmail.Destinatario(guia.Destinatario.Nombre)
                    .NumeroGuia(guia.Folio)
                        .TiuAmigo(guia.Remitente.Nombre)
                        .UrlWebSite($"{this._configuration["UrlWebSiteDetalleGuia"]}/{guia.Folio}"); ;
                    return this._estatusNoVisitadoEmail;
                case EEstatusGuia.PRIMER_INTENTO_ENTREGA:
                    this._estatusIntentoDeEntregaEmail.Reload();
                    this._estatusIntentoDeEntregaEmail.To = guia.Destinatario.CorreoElectronico;
                    this._estatusIntentoDeEntregaEmail.Destinatario(guia.Destinatario.Nombre)
                    .NumeroGuia(guia.Folio)
                        .TiuAmigo(guia.Remitente.Nombre)
                        .UrlWebSite($"{this._configuration["UrlWebSiteDetalleGuia"]}/{guia.Folio}"); ;
                    return this._estatusNoVisitadoEmail;
                case EEstatusGuia.REAGENDADO:
                    this._estatusReagendadoEmail.Reload();
                    this._estatusReagendadoEmail.To = guia.Destinatario.CorreoElectronico;
                    this._estatusReagendadoEmail.Destinatario(guia.Destinatario.Nombre).NumeroGuia(guia.Folio).TiuAmigo(guia.Remitente.Nombre)
                        .UrlWebSite($"{this._configuration["UrlWebSiteDetalleGuia"]}/{guia.Folio}");

                    return this._estatusReagendadoEmail;
                case EEstatusGuia.ENTREGAGO:
                    this._estatusEntregadoEmail.Reload();
                    this._estatusEntregadoEmail.To = guia.Destinatario.CorreoElectronico;
                    this._estatusEntregadoEmail.Destinatario(guia.Destinatario.Nombre).NumeroGuia(guia.Folio)
                        .TiuAmigo(guia.Remitente.Nombre)
                        .UrlWebSite($"{this._configuration["UrlWebSiteDetalleGuia"]}/{guia.Folio}"); ;
                    return this._estatusEntregadoEmail;
                default:
                    return null;
            }
        }
    }
}
