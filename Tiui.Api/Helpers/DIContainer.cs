using Tiui.Application.Repository.Clientes;
using Tiui.Application.Repository.Comun;
using Tiui.Application.Repository.Guias;
using Tiui.Application.Repository.Seguridad;
using Tiui.Application.Repository.UnitOfWork;
using Tiui.Application.Security;
using Tiui.Application.Services.Clientes;
using Tiui.Application.Services.Comun;
using Tiui.Application.Services.Guias;
using Tiui.Application.Services.Seguidad;
using Tiui.Data.Repository.Clientes;
using Tiui.Data.Repository.Comun;
using Tiui.Data.Repository.Guias;
using Tiui.Data.Repository.Seguridad;
using Tiui.Data.UnitOfWork;
using Tiui.Services.Clientes;
using Tiui.Services.Comun;
using Tiui.Services.Guias;
using Tiui.Services.WebSockets;
using Tiui.Services.Seguridad;
using Tiui.Reports.Reports;
using Tiui.Reports.DataAccess;
using Tiui.Application.Reports;
using Tiui.Security;
using Tiui.Mailing;
using Tiui.Application.Mail;
using Tiui.Application.Mail.Configuration;
using Tiui.Mailing.Configuration;
using Tiui.Application.Services.Cancelaciones;
using Tiui.Services.Cancelaciones;
using Tiui.Application.Mail.Helpers;
using Tiui.Mailing.Configuration.Helpers;
using Tiui.Application.File;
using Tiui.Files;
using Tiui.Application.Services.websocket;
using Tiui.Data.Repository.GuiaInfoSuscription;
using Tiui.Services.GuiaNotificationClientes;

namespace Tiui.Api.Helpers
{
    /// <summary>
    /// Administrador de inyección de dependencias
    /// </summary>
    public static class DIContainer
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            #region Repository
            services.AddScoped<IUsarioRepository, UsuarioReporitory>();
            services.AddScoped<ITiuiAmigoRepository, TiuiAmigoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPaisRepository, PaisRepository>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();
            services.AddScoped<IMunicipioRepository, MunicipioRepository>();
            services.AddScoped<IConfiguracionCajaTiuiAmigoRepository, ConfiguracionCajaTiuiAmigoRepository>();
            services.AddScoped<ILibretaDireccionRepository, LibretaDireccionRepository>();
            services.AddScoped<ITiuiAmigoRepository, TiuiAmigoRepository>();
            services.AddScoped<IGuiaRepository, GuiaRepository>();
            services.AddScoped<IPaqueteRepository, PaqueteRepository>();
            services.AddScoped<IPaqueteriaRepository, PaqueteriaRepository>();
            services.AddScoped<IBitacoraGuiaRepository, BitacoraGuiaRepository>();
            #endregion
            #region Services
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IHashService, HashService>();
            services.AddTransient<ISecurityManager, SecurityManager>();
            services.AddScoped<ILocalizacionService, LocalizacionService>();
            services.AddScoped<IPaqueteriaService, PaqueteriaService>();
            services.AddScoped<IGuiaService, GuiaService>();
            services.AddScoped<IGuiaInfoSuscriptionRepository, GuiaInfoSuscriptionRepository>();
            services.AddScoped<IDireccionService, DireccionService>();
            services.AddScoped<IConfiguracionCajaTiuiAmigoService, ConfiguracionCajaTiuiAmigoService>();
            services.AddScoped<IConfiguracionAppService, ConfiguracionAppService>();
            services.AddScoped<IGraphqlService, GraphqlService>();
            services.AddScoped<INotificacionClienteService, NotificacionClienteService>();
            services.AddScoped<IGuiaStateService, GuiaStateService>();
            services.AddScoped<ITiuiAmigoService, TiuiAmigoService>();
            services.AddScoped<IEstatusService, EstatusService>();
            services.AddScoped<IMotivoCancelacionService, MotivoCancelacionService>();
            services.AddScoped<IUsuarioAdminService, UsuarioAdminService>();
            services.AddScoped<IGuiaMasiveService, GuiaMasiveService>();
            services.AddScoped<IGuiaWebSocketHandler, GuiaWebSocketHandler>();
            services.AddScoped<IGuiaNotificationClientes, GuiaNotificationClientes>();
            #endregion
            #region Reports
            services.AddScoped<IGuiaReport, GuiaReport>();
            services.AddScoped<GuiaDataAccess>();
            services.AddScoped<IGuiaCompleteReport, GuiaCompleteReport>();
            services.AddScoped<GuiaCompleteDataAccess>();
            #endregion
            #region Email
            services.AddScoped<IEmail, Email>();
            services.AddScoped<IEMailFluent<IEmail>, EMailFluent<IEmail>>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();
            services.AddScoped<IEmailServerConfiguration, SmtpEmailServerConfiguration>();
            services.AddScoped<INuevoTiuiAmigoEmail, NuevoTiuiAmigoEmail>();
            services.AddScoped<IRegistroGuiaEmail, RegistroGuiaEmail>();
            services.AddScoped<IEstatusCaminoCDMXEmail, EstatusCaminoCDMXEmail>();
            services.AddScoped<IEstatusEnCedisCDMXEmail, EstatusEnCedisCDMXEmail>();
            services.AddScoped<IEstatusRutaEntregaEmail, EstatusRutaEntregaEmail>();
            services.AddScoped<IEstatusPreparandoRutaEmail, EstatusPreparandoRutaEmail>();
            services.AddScoped<IEstatusIntentoDeEntregaEmail, EstatusIntentoDeEntregaEmail>();
            services.AddScoped<IEstatusReagendadoEmail, EstatusReagendadoEmail>();
            services.AddScoped<IEstatusNoVisitadoEmail, EstatusNoVisitadoEmail>();
            services.AddScoped<IEstatusEntregadoEmail, EstatusEntregadoEmail>();
            services.AddScoped<IEmailStatusFactoryHelper, EmailStatusFactoryHelper>();
            services.AddScoped<IRecuperarContraseñaEmail, RecuperarContraseñaEmail>();
            #endregion
            #region File
            services.AddScoped<IFileService, FileService>();
            #endregion
            return services;
        }
    }
}
