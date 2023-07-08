using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tiui.Entities.Cancelaciones;
using Tiui.Entities.Comun;
using Tiui.Entities.Guias;
using Tiui.Entities.Seguridad;

namespace Tiui.Data
{
  /// <summary>
  /// Contexto de datos para comunicación con la base de datos
  /// </summary>
  public class TiuiDBContext : DbContext
  {
    #region Comun
    private readonly IConfiguration _configuration;
    public DbSet<ConfiguracionApp> ConfiguracionApp { get; set; }
    public DbSet<Estatus> Estatus { get; set; }
    public DbSet<Pais> Paises { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Municipio> Municipios { get; set; }
    public DbSet<Direccion> Direcciones { get; set; }
    public DbSet<GuiaInfoSuscriptionDTO> guiainfosuscription { get; set; }

    #endregion

    #region Clientes
    public DbSet<TiuiAmigo> TiuiAmigos { get; set; }
    public DbSet<LibretaDireccion> LibretaDirecciones { get; set; }
    public DbSet<ConfiguracionCajaTiuiAmigo> CofiguracionesCajaTiuiAmigo { get; set; }
    #endregion

    #region Guias
    public DbSet<Paqueteria> Paqueterias { get; set; }
    public DbSet<Paquete> Paquetes { get; set; }
    public DbSet<Guia> Guias { get; set; }
    public DbSet<BitacoraGuia> BitacoraGuias { get; set; }
    public DbSet<NotificacionCliente> NotificacionClientes { get; set; }
    public DbSet<DireccionGuia> DireccionesGuia { get; set; }
    #endregion

    #region Seguridad
    public DbSet<Usuario> Usuarios { get; set; }
    #endregion

    #region Cancelaciones
    public DbSet<MotivoCancelacion> MotivosCancelaciones { get; set; }
    public DbSet<CancelacionGuia> CancelacionGuias { get; set; }
    #endregion

    #region Contructores
    /// <summary>
    /// Inicializa una instancia del objeto sin datos externos
    /// </summary>
    public TiuiDBContext()
    {
    }
    public TiuiDBContext(DbContextOptions<TiuiDBContext> optionsBuilder, IConfiguration configuration)
           : base(optionsBuilder)
    {
      _configuration = configuration;
    }
    #endregion
    #region Metodos
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        optionsBuilder.UseNpgsql(this._configuration["ConnectionTiuiDB"]);
      }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Usuario>().HasIndex(x => x.NombreUsuario).IsUnique();
      modelBuilder.Entity<Paqueteria>().HasQueryFilter(x => x.Activo == true);
      modelBuilder.Entity<TiuiAmigo>().Property(x => x.TipoProceso).HasDefaultValue(ETipoFlujoGuia.CONSOLIDADO);
      modelBuilder.Entity<TiuiAmigo>().HasIndex(x => x.Codigo).IsUnique();
      modelBuilder.Entity<Guia>().HasIndex(x => x.Folio).IsUnique();
      modelBuilder.Entity<Evidencia>().HasIndex(x => x.id).IsUnique();
      modelBuilder.Entity<GuiaInfoSuscriptionDTO>().HasIndex(x => x.GuiaId).IsUnique();
      //modelBuilder.Entity<DireccionGuia>().HasIndex(x => x.DireccionGuiaId).IsUnique();

      InitConfiguration(modelBuilder);
      InitEstatus(modelBuilder);
      InitPaqueterias(modelBuilder);
      InitMotivosCancelacion(modelBuilder);
      base.OnModelCreating(modelBuilder);
    }
    /// <summary>
    /// Inicializa los datos de configuración de la aplicación
    /// </summary>
    /// <param name="modelBuilder"></param>
    private static void InitConfiguration(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ConfiguracionApp>().HasData(
                      new ConfiguracionApp
                      {
                        ConfiguracionAppId = 1,
                        CorreoElectronicoAdministrativo = "",
                        IVA = 0.16m,
                        SeguroMercancia = 0.02m,
                        ComisionCobroContraEntrega = 0.05m
                      }
                      );
    }
    /// <summary>
    /// Inicializa los datos de las paqueterias
    /// </summary>
    /// <param name="modelBuilder">Builder que genera los modelos</param>
    private static void InitPaqueterias(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Paqueteria>().HasData(
          new Paqueteria { PaqueteriaId = 1, Nombre = "Tipo 1 (24-48 horas)", Descripcion = "Tiui", CostoEnvio = 100, FechaRegistro = DateTime.UtcNow, Activo = true, MaximoDiasDeEntrega = 2 },
          new Paqueteria { PaqueteriaId = 2, Nombre = "Tipo 2 (24-96 horas)", Descripcion = "Tiui", CostoEnvio = 100, FechaRegistro = DateTime.UtcNow, Activo = true, MaximoDiasDeEntrega = 4 }
          );
    }
    /// <summary>
    /// Inicializa los estatus para los paquetes
    /// </summary>
    /// <param name="modelBuilder">Builder para generar los modelos</param>
    private static void InitEstatus(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Estatus>().HasData(
                    new Estatus
                    {
                      Nombre = "Tu envío ha sido documentado consolidado",
                      EstatusId = (int)EEstatusGuia.DOCUMENTADO_CONSOLIDADO,
                      Activo = true,
                      Proceso = "Preparando",
                      TipoFlujo = ETipoFlujoGuia.CONSOLIDADO,
                      Descripcion = "Envío documentado (consolidado)"
                    },
                    new Estatus
                    {
                      Nombre = "Tu envío está en camino a CDMX",
                      EstatusId = (int)EEstatusGuia.CAMINO_CEDIS_CDMX,
                      Activo = true,
                      Proceso = "Preparando",
                      TipoFlujo = ETipoFlujoGuia.CONSOLIDADO,
                      Descripcion = "En camino a CDMX"
                    },
                    new Estatus
                    {
                      Nombre = "Tu envío se recibió en CEDIS Tiui CDMX",
                      EstatusId = (int)EEstatusGuia.RECIBIDO_EN_CEDIS_CDMX,
                      Activo = true,
                      Proceso = "Preparando",
                      TipoFlujo = ETipoFlujoGuia.CONSOLIDADO,
                      Descripcion = "Recepción de envío CEDIS Tiui CDMX"
                    },
                    new Estatus
                    {
                      Nombre = "Tu envío está en CEDIS Tiui CDMX consolidado",
                      EstatusId = (int)EEstatusGuia.EN_CEDIS_CDMX,
                      Activo = true,
                      Proceso = "Preparando",
                      TipoFlujo = ETipoFlujoGuia.CONSOLIDADO,
                      Descripcion = "En CEDIS Tiui CDMX (consolidado)"
                    },
                    new Estatus
                    {
                      Nombre = "Tu envío ha sido documentado fulfillment",
                      EstatusId = (int)EEstatusGuia.DOCUMENTADO_FULFILLMENT,
                      Activo = true,
                      Proceso = "Preparando",
                      TipoFlujo = ETipoFlujoGuia.FULFILMENT,
                      Descripcion = "Envío documentado (fulfillment)"
                    },
                    new Estatus
                    {
                      Nombre = "Estamos preparando tu paquete",
                      EstatusId = (int)EEstatusGuia.PREPARACION,
                      Activo = true,
                      Proceso = "Preparando",
                      TipoFlujo = ETipoFlujoGuia.FULFILMENT,
                      Descripcion = "Preparando paquete"
                    },
                    new Estatus
                    {
                      Nombre = "Tu envío está en CEDIS Tiui CDMX fulfillment",
                      EstatusId = (int)EEstatusGuia.FULFILLMENT_EN_CEDIS_CDMX,
                      Activo = true,
                      Proceso = "Preparando",
                      TipoFlujo = ETipoFlujoGuia.FULFILMENT,
                      Descripcion = "En CEDIS Tiui CDMX (fulfillment)"
                    },
                    new Estatus
                    {
                      Nombre = "Oops! El inventario se ha agotado. Estamos en espera de que el proveedor reabastezca tu producto.",
                      EstatusId = (int)EEstatusGuia.PRODUCTO_AGOTADO,
                      Activo = true,
                      Proceso = "Preparando",
                      TipoFlujo = ETipoFlujoGuia.FULFILMENT,
                      Descripcion = "En espera de producto"
                    },
                    new Estatus
                    {
                      Nombre = "Tu envío está en ruta de entrega",
                      EstatusId = (int)EEstatusGuia.RUTA_ENTREGA,
                      Activo = true,
                      Proceso = "En camino",
                      TipoFlujo = ETipoFlujoGuia.TODOS,
                      Descripcion = "En ruta de entrega"
                    },
                    new Estatus
                    {
                      Nombre = "Se realizó el 1er intento de entrega",
                      EstatusId = (int)EEstatusGuia.PRIMER_INTENTO_ENTREGA,
                      Activo = true,
                      Proceso = "En camino",
                      TipoFlujo = ETipoFlujoGuia.TODOS,
                      Descripcion = "1er intento de entrega"
                    },
                    new Estatus
                    {
                      Nombre = "Tu envío se reagendó",
                      EstatusId = (int)EEstatusGuia.REAGENDADO,
                      Activo = true,
                      Proceso = "En camino",
                      TipoFlujo = ETipoFlujoGuia.TODOS,
                      Descripcion = "Envío reagendado"
                    },
                    new Estatus
                    {
                      Nombre = "Envío Cancelado",
                      EstatusId = (int)EEstatusGuia.CANCELADO,
                      Activo = true,
                      Proceso = "En camino",
                      TipoFlujo = ETipoFlujoGuia.TODOS,
                      Descripcion = "Cancelado"
                    },
                    new Estatus
                    {
                      Nombre = "Entregado",
                      EstatusId = (int)EEstatusGuia.ENTREGAGO,
                      Activo = true,
                      Proceso = "Celebrando",
                      TipoFlujo = ETipoFlujoGuia.TODOS,
                      Descripcion = "Entregado"
                    },
                    new Estatus
                    {
                      Nombre = "Conciliado",
                      EstatusId = (int)EEstatusGuia.CONCILIADO,
                      Activo = true,
                      Proceso = "Celebrando",
                      TipoFlujo = ETipoFlujoGuia.TODOS,
                      Descripcion = "Conciliado"
                    },
                    new Estatus
                    {
                      Nombre = "Pagado",
                      EstatusId = (int)EEstatusGuia.PAGADO,
                      Activo = true,
                      Proceso = "Celebrando",
                      TipoFlujo = ETipoFlujoGuia.TODOS,
                      Descripcion = "Pagado"
                    },
                    new Estatus
                    {
                      Nombre = "Envío documentado recolección",
                      EstatusId = (int)EEstatusGuia.DOCUMENTADO_RECOLECCION,
                      Activo = true,
                      Proceso = "Preparando",
                      TipoFlujo = ETipoFlujoGuia.RECOLECCION,
                      Descripcion = "Envío documentado (recolección)"
                    },
                    new Estatus
                    {
                      Nombre = "En camino a recolección",
                      EstatusId = (int)EEstatusGuia.EN_CAMINO_RECOLECCION,
                      Activo = true,
                      Proceso = "Preparando",
                      TipoFlujo = ETipoFlujoGuia.RECOLECCION,
                      Descripcion = "En camino a recolección"
                    },
                    new Estatus
                    {
                      Nombre = "Envío recolectado",
                      EstatusId = (int)EEstatusGuia.ENVIO_RECOLECTADO,
                      Activo = true,
                      Proceso = "Preparando",
                      TipoFlujo = ETipoFlujoGuia.RECOLECCION,
                      Descripcion = "Envío recolectado"
                    },
                    new Estatus
                    {
                      Nombre = "Tu envío está en CEDIS Tiui CDMX recolección",
                      EstatusId = (int)EEstatusGuia.EN_CEDIS_CDMX_RECOLECCION,
                      Activo = true,
                      Proceso = "Preparando",
                      TipoFlujo = ETipoFlujoGuia.RECOLECCION,
                      Descripcion = "En CEDIS Tiui CDMX (recolección)"
                    },
                    new Estatus
                    {
                      Nombre = "Estamos preprando la ruta",
                      EstatusId = (int)EEstatusGuia.PREPARANDO_RUTA,
                      Activo = true,
                      Proceso = "En camino",
                      TipoFlujo = ETipoFlujoGuia.TODOS,
                      Descripcion = "Preparando Ruta"
                    },
                    new Estatus
                    {
                      Nombre = "No alcanzamos a visitar tu domicilio.",
                      EstatusId = (int)EEstatusGuia.NO_VISITADO,
                      Activo = true,
                      Proceso = "En camino",
                      TipoFlujo = ETipoFlujoGuia.TODOS,
                      Descripcion = "No visitado"
                    },
                    new Estatus
                    {
                      Nombre = "Enviamos tu envio a revision.",
                      EstatusId = (int)EEstatusGuia.ENVIO_PARA_REVISION,
                      Activo = true,
                      Proceso = "En camino",
                      TipoFlujo = ETipoFlujoGuia.TODOS,
                      Descripcion = "Envio para revision"
                    },
                new Estatus
                {
                  Nombre = "En espera para devolución",
                  EstatusId = (int)EEstatusGuia.EN_ESPERA_DEVOLUCION,
                  Activo = true,
                  Proceso = "En camino",
                  TipoFlujo = ETipoFlujoGuia.TODOS,
                  Descripcion = "Para devolución"
                },
                new Estatus
                {
                  Nombre = "Devuelto",
                  EstatusId = (int)EEstatusGuia.DEVUELTO,
                  Activo = true,
                  Proceso = "Celebrando",
                  TipoFlujo = ETipoFlujoGuia.TODOS,
                  Descripcion = "Envio para revision"
                },
                new Estatus
                {
                  Nombre = "Recolección no visitada",
                  EstatusId = (int)EEstatusGuia.RECOLECCION_NO_VISITADO,
                  Activo = true,
                  Proceso = "Preparando",
                  TipoFlujo = ETipoFlujoGuia.RECOLECCION,
                  Descripcion = "Envio para revision"
                },
                new Estatus
                {
                  Nombre = "Reprogramar recolección",
                  EstatusId = (int)EEstatusGuia.RECOLECCION_REPROGRAMADA,
                  Activo = true,
                  Proceso = "Preparando",
                  TipoFlujo = ETipoFlujoGuia.RECOLECCION,
                  Descripcion = "Envio para revision"
                },
                new Estatus
                {
                  Nombre = "Intento recolección",
                  EstatusId = (int)EEstatusGuia.INTENTO_RECOLECCION,
                  Activo = true,
                  Proceso = "Preparando",
                  TipoFlujo = ETipoFlujoGuia.RECOLECCION,
                  Descripcion = "Intento Recolección"
                }
                );
    }
    private static void InitMotivosCancelacion(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<MotivoCancelacion>().HasData(
        new MotivoCancelacion { MotivoCancelacionId = 1, Activo = true, Descripcion = "No tiene dinero", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 2, Activo = true, Descripcion = "Encontró el producto mas barato", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 3, Activo = true, Descripcion = "Retraso en entrega", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 4, Activo = true, Descripcion = "No se puede contactar con el cliente", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 5, Activo = true, Descripcion = "No hay inventario", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 6, Activo = true, Descripcion = "No reconoce la compra", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 7, Activo = true, Descripcion = "Cliente ya no quiere el producto", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 8, Activo = true, Descripcion = "Por segundo intento de entrega", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 9, Activo = true, Descripcion = "Por rechazo de producto", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 10, Activo = true, Descripcion = "Por falta de dinero en la entrega", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 11, Activo = true, Descripcion = "No se localizó al cliente", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 101, Activo = true, Descripcion = "No tiene dinero", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 102, Activo = true, Descripcion = "Encontró el producto mas barato", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 103, Activo = true, Descripcion = "Retraso en entrega", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 104, Activo = true, Descripcion = "No se puede contactar con el cliente", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 105, Activo = true, Descripcion = "No hay inventario", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 106, Activo = true, Descripcion = "No reconoce la compra", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 107, Activo = true, Descripcion = "Cliente ya no quiere el producto", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 108, Activo = true, Descripcion = "Fuera de cobertura", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 109, Activo = true, Descripcion = "Cancelacion TiuiAmigo", TipoCancelacion = ETipoCancelacion.ANTES_DE_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 201, Activo = true, Descripcion = "Por Intento de entrega", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 202, Activo = true, Descripcion = "Por rechazo de producto en domicilio", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 203, Activo = true, Descripcion = "Por falta de dinero en la entrega", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 204, Activo = true, Descripcion = "No responde el telefono y/o no abre la puerta", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 205, Activo = true, Descripcion = "Cancelacion TiuiAmigo", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 206, Activo = true, Descripcion = "No reconoce la compra", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 207, Activo = true, Descripcion = "Retraso en entrega", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 208, Activo = true, Descripcion = "No tiene dinero", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 209, Activo = true, Descripcion = "Fuera de cobertura", TipoCancelacion = ETipoCancelacion.EN_RUTA },
        new MotivoCancelacion { MotivoCancelacionId = 210, Activo = true, Descripcion = "Cliente ya no quiere el producto", TipoCancelacion = ETipoCancelacion.EN_RUTA }
  );
    }
    #endregion
  }
}
