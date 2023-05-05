using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tiui.Data.Migrations
{
    public partial class dbinitialsprint1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Archivo",
                columns: table => new
                {
                    ArchivoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    NombreReal = table.Column<string>(type: "text", nullable: true),
                    MimeType = table.Column<string>(type: "text", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivo", x => x.ArchivoId);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionApp",
                columns: table => new
                {
                    ConfiguracionAppId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CorreoElectronicoAdministrativo = table.Column<string>(type: "text", nullable: true),
                    IVA = table.Column<decimal>(type: "numeric", nullable: false),
                    SeguroMercancia = table.Column<decimal>(type: "numeric", nullable: false),
                    ComisionCobroContraEntrega = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionApp", x => x.ConfiguracionAppId);
                });

            migrationBuilder.CreateTable(
                name: "Estatus",
                columns: table => new
                {
                    EstatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Proceso = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estatus", x => x.EstatusId);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    PaisId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.PaisId);
                });

            migrationBuilder.CreateTable(
                name: "Paqueterias",
                columns: table => new
                {
                    PaqueteriaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    CostoEnvio = table.Column<decimal>(type: "numeric", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MaximoDiasDeEntrega = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paqueterias", x => x.PaqueteriaId);
                });

            migrationBuilder.CreateTable(
                name: "Paquetes",
                columns: table => new
                {
                    PaqueteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Largo = table.Column<float>(type: "real", nullable: false),
                    Alto = table.Column<float>(type: "real", nullable: false),
                    Ancho = table.Column<float>(type: "real", nullable: false),
                    PesoFisico = table.Column<float>(type: "real", nullable: false),
                    PesoCotizado = table.Column<float>(type: "real", nullable: false),
                    EvidenciaEntregaId = table.Column<int>(type: "integer", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paquetes", x => x.PaqueteId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreUsuario = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    NombreCompleto = table.Column<string>(type: "text", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    TipoUsuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    EstadoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    PaisId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.EstadoId);
                    table.ForeignKey(
                        name: "FK_Estados_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "PaisId");
                });

            migrationBuilder.CreateTable(
                name: "EvidenciaEntrega",
                columns: table => new
                {
                    EvidenciaEntregaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonaRecibe = table.Column<string>(type: "text", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FotoArchivoId = table.Column<long>(type: "bigint", nullable: true),
                    FirmaArchivoId = table.Column<long>(type: "bigint", nullable: true),
                    PaqueteId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvidenciaEntrega", x => x.EvidenciaEntregaId);
                    table.ForeignKey(
                        name: "FK_EvidenciaEntrega_Archivo_FirmaArchivoId",
                        column: x => x.FirmaArchivoId,
                        principalTable: "Archivo",
                        principalColumn: "ArchivoId");
                    table.ForeignKey(
                        name: "FK_EvidenciaEntrega_Archivo_FotoArchivoId",
                        column: x => x.FotoArchivoId,
                        principalTable: "Archivo",
                        principalColumn: "ArchivoId");
                    table.ForeignKey(
                        name: "FK_EvidenciaEntrega_Paquetes_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquetes",
                        principalColumn: "PaqueteId");
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    MunicipioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    EstadoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.MunicipioId);
                    table.ForeignKey(
                        name: "FK_Municipios_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "EstadoId");
                });

            migrationBuilder.CreateTable(
                name: "BitacoraGuias",
                columns: table => new
                {
                    BitacoraGuiaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EstatusAnterior = table.Column<int>(type: "integer", nullable: false),
                    EstatusNuevo = table.Column<int>(type: "integer", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    GuiaId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitacoraGuias", x => x.BitacoraGuiaId);
                });

            migrationBuilder.CreateTable(
                name: "CofiguracionesCajaTiuiAmigo",
                columns: table => new
                {
                    ConfiguracionCajaTiuiAmigoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Largo = table.Column<float>(type: "real", nullable: false),
                    Alto = table.Column<float>(type: "real", nullable: false),
                    Ancho = table.Column<float>(type: "real", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    TiuiAmigoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CofiguracionesCajaTiuiAmigo", x => x.ConfiguracionCajaTiuiAmigoId);
                });

            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    DireccionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Calle = table.Column<string>(type: "text", nullable: true),
                    Cruzamiento = table.Column<string>(type: "text", nullable: true),
                    Numero = table.Column<string>(type: "text", nullable: true),
                    Colonia = table.Column<string>(type: "text", nullable: true),
                    CodigoPostal = table.Column<string>(type: "text", nullable: true),
                    Referencias = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    MunicipioId = table.Column<int>(type: "integer", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Empresa = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "text", nullable: true),
                    TiuiAmigoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.DireccionId);
                    table.ForeignKey(
                        name: "FK_Direcciones_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "MunicipioId");
                });

            migrationBuilder.CreateTable(
                name: "TiuiAmigos",
                columns: table => new
                {
                    TiuiAmigoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombres = table.Column<string>(type: "text", nullable: true),
                    Apellidos = table.Column<string>(type: "text", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "text", nullable: true),
                    DireccionId = table.Column<long>(type: "bigint", nullable: true),
                    Celular = table.Column<string>(type: "text", nullable: true),
                    RazonSocial = table.Column<string>(type: "text", nullable: true),
                    RFC = table.Column<string>(type: "text", nullable: true),
                    DireccionFiscalId = table.Column<long>(type: "bigint", nullable: true),
                    TelefonoContacto = table.Column<string>(type: "text", nullable: true),
                    NombreContacto = table.Column<string>(type: "text", nullable: true),
                    ArchivoId = table.Column<long>(type: "bigint", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: true),
                    TipoProceso = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiuiAmigos", x => x.TiuiAmigoId);
                    table.ForeignKey(
                        name: "FK_TiuiAmigos_Archivo_ArchivoId",
                        column: x => x.ArchivoId,
                        principalTable: "Archivo",
                        principalColumn: "ArchivoId");
                    table.ForeignKey(
                        name: "FK_TiuiAmigos_Direcciones_DireccionFiscalId",
                        column: x => x.DireccionFiscalId,
                        principalTable: "Direcciones",
                        principalColumn: "DireccionId");
                    table.ForeignKey(
                        name: "FK_TiuiAmigos_Direcciones_DireccionId",
                        column: x => x.DireccionId,
                        principalTable: "Direcciones",
                        principalColumn: "DireccionId");
                });

            migrationBuilder.CreateTable(
                name: "Guias",
                columns: table => new
                {
                    GuiaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EsPagoContraEntrega = table.Column<bool>(type: "boolean", nullable: false),
                    ImporteContraEntrega = table.Column<decimal>(type: "numeric", nullable: false),
                    TieneSeguroMercancia = table.Column<bool>(type: "boolean", nullable: false),
                    ImporteCalculoSeguro = table.Column<decimal>(type: "numeric", nullable: false),
                    ImporteSeguroMercancia = table.Column<decimal>(type: "numeric", nullable: false),
                    ImportePaqueteria = table.Column<decimal>(type: "numeric", nullable: false),
                    CostoOperativo = table.Column<decimal>(type: "numeric", nullable: false),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    IVA = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PaqueteId = table.Column<long>(type: "bigint", nullable: true),
                    TiuiAmigoId = table.Column<int>(type: "integer", nullable: true),
                    PaqueteriaId = table.Column<int>(type: "integer", nullable: true),
                    CobroContraEntrega = table.Column<decimal>(type: "numeric", nullable: false),
                    CantidadPaquetes = table.Column<int>(type: "integer", nullable: false),
                    NombreProducto = table.Column<string>(type: "text", nullable: true),
                    Folio = table.Column<string>(type: "text", nullable: true),
                    FechaEstimadaEntrega = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Consecutivo = table.Column<int>(type: "integer", nullable: false),
                    EstatusId = table.Column<int>(type: "integer", nullable: true),
                    TipoProcesoCancelacion = table.Column<int>(type: "integer", nullable: true),
                    ProcesoCancelacion = table.Column<string>(type: "text", nullable: true),
                    FechaReagendado = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FechaConciliacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guias", x => x.GuiaId);
                    table.ForeignKey(
                        name: "FK_Guias_Estatus_EstatusId",
                        column: x => x.EstatusId,
                        principalTable: "Estatus",
                        principalColumn: "EstatusId");
                    table.ForeignKey(
                        name: "FK_Guias_Paqueterias_PaqueteriaId",
                        column: x => x.PaqueteriaId,
                        principalTable: "Paqueterias",
                        principalColumn: "PaqueteriaId");
                    table.ForeignKey(
                        name: "FK_Guias_Paquetes_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquetes",
                        principalColumn: "PaqueteId");
                    table.ForeignKey(
                        name: "FK_Guias_TiuiAmigos_TiuiAmigoId",
                        column: x => x.TiuiAmigoId,
                        principalTable: "TiuiAmigos",
                        principalColumn: "TiuiAmigoId");
                });

            migrationBuilder.CreateTable(
                name: "DireccionesGuia",
                columns: table => new
                {
                    DireccionGuiaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Empresa = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "text", nullable: true),
                    CodigoPostal = table.Column<string>(type: "text", nullable: true),
                    Pais = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    Ciudad = table.Column<string>(type: "text", nullable: true),
                    Colonia = table.Column<string>(type: "text", nullable: true),
                    Calle = table.Column<string>(type: "text", nullable: true),
                    Cruzamiento = table.Column<string>(type: "text", nullable: true),
                    Numero = table.Column<string>(type: "text", nullable: true),
                    Referencias = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    GuiaId = table.Column<long>(type: "bigint", nullable: true),
                    Remitente_GuiaId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DireccionesGuia", x => x.DireccionGuiaId);
                    table.ForeignKey(
                        name: "FK_DireccionesGuia_Guias_GuiaId",
                        column: x => x.GuiaId,
                        principalTable: "Guias",
                        principalColumn: "GuiaId");
                    table.ForeignKey(
                        name: "FK_DireccionesGuia_Guias_Remitente_GuiaId",
                        column: x => x.Remitente_GuiaId,
                        principalTable: "Guias",
                        principalColumn: "GuiaId");
                });

            migrationBuilder.CreateTable(
                name: "NotificacionClientes",
                columns: table => new
                {
                    NotificacionClienteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    GuiaId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacionClientes", x => x.NotificacionClienteId);
                    table.ForeignKey(
                        name: "FK_NotificacionClientes_Guias_GuiaId",
                        column: x => x.GuiaId,
                        principalTable: "Guias",
                        principalColumn: "GuiaId");
                });

            migrationBuilder.InsertData(
                table: "ConfiguracionApp",
                columns: new[] { "ConfiguracionAppId", "ComisionCobroContraEntrega", "CorreoElectronicoAdministrativo", "IVA", "SeguroMercancia" },
                values: new object[] { 1, 0.05m, "", 0.16m, 0.02m });

            migrationBuilder.InsertData(
                table: "Estatus",
                columns: new[] { "EstatusId", "Activo", "Nombre", "Proceso" },
                values: new object[,]
                {
                    { 1, true, "Tu envío ha sido documentado", "Preparando" },
                    { 2, true, "Tu envío está en camino a CDMX", "Preparando" },
                    { 3, true, "Tu envío se recibió en CEDIS Tiui CDMX", "Preparando" },
                    { 4, true, "Tu envío está en CEDIS Tiui CDMX", "Preparando" },
                    { 5, true, "Tu envío ha sido documentado", "Preparando" },
                    { 6, true, "Estamos preparando tu paquete", "Preparando" },
                    { 7, true, "Tu envío está en CEDIS Tiui CDMX", "Preparando" },
                    { 8, true, "Oops! El inventario se ha agotado. Estamos en espera de que el proveedor reabastezca tu producto. ", "Preparando" },
                    { 9, true, "Tu envío está en ruta de entrega", "En camino" },
                    { 10, true, "Se realizó el 1er intento de entrega", "En camino" },
                    { 11, true, "Se realizó el 2do intento de entrega", "En camino" },
                    { 12, true, "Tu envío se reagendó", "En camino" },
                    { 13, true, "Tu envío ha sido cancelado", "En camino" },
                    { 14, true, "Cancelado en ruta", "En camino" },
                    { 15, true, "Rechazado", "En camino" },
                    { 16, true, "Entregado", "Celebrando" },
                    { 17, true, "Conciliado", "Celebrando" },
                    { 18, true, "Pagado", "Celebrando" }
                });

            migrationBuilder.InsertData(
                table: "Paqueterias",
                columns: new[] { "PaqueteriaId", "Activo", "CostoEnvio", "Descripcion", "FechaRegistro", "MaximoDiasDeEntrega", "Nombre" },
                values: new object[,]
                {
                    { 1, true, 100m, "Tiui", new DateTime(2022, 2, 21, 16, 56, 38, 886, DateTimeKind.Local).AddTicks(7897), 2, "Tipo 1 (24-48 horas)" },
                    { 2, true, 100m, "Tiui", new DateTime(2022, 2, 21, 16, 56, 38, 886, DateTimeKind.Local).AddTicks(7910), 4, "Tipo 2 (24-96 horas)" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BitacoraGuias_GuiaId",
                table: "BitacoraGuias",
                column: "GuiaId");

            migrationBuilder.CreateIndex(
                name: "IX_CofiguracionesCajaTiuiAmigo_TiuiAmigoId",
                table: "CofiguracionesCajaTiuiAmigo",
                column: "TiuiAmigoId");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_MunicipioId",
                table: "Direcciones",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_TiuiAmigoId",
                table: "Direcciones",
                column: "TiuiAmigoId");

            migrationBuilder.CreateIndex(
                name: "IX_DireccionesGuia_GuiaId",
                table: "DireccionesGuia",
                column: "GuiaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DireccionesGuia_Remitente_GuiaId",
                table: "DireccionesGuia",
                column: "Remitente_GuiaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estados_PaisId",
                table: "Estados",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_EvidenciaEntrega_FirmaArchivoId",
                table: "EvidenciaEntrega",
                column: "FirmaArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_EvidenciaEntrega_FotoArchivoId",
                table: "EvidenciaEntrega",
                column: "FotoArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_EvidenciaEntrega_PaqueteId",
                table: "EvidenciaEntrega",
                column: "PaqueteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guias_EstatusId",
                table: "Guias",
                column: "EstatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Guias_Folio",
                table: "Guias",
                column: "Folio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guias_PaqueteId",
                table: "Guias",
                column: "PaqueteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guias_PaqueteriaId",
                table: "Guias",
                column: "PaqueteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Guias_TiuiAmigoId",
                table: "Guias",
                column: "TiuiAmigoId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_EstadoId",
                table: "Municipios",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionClientes_GuiaId",
                table: "NotificacionClientes",
                column: "GuiaId");

            migrationBuilder.CreateIndex(
                name: "IX_TiuiAmigos_ArchivoId",
                table: "TiuiAmigos",
                column: "ArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_TiuiAmigos_Codigo",
                table: "TiuiAmigos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TiuiAmigos_DireccionFiscalId",
                table: "TiuiAmigos",
                column: "DireccionFiscalId");

            migrationBuilder.CreateIndex(
                name: "IX_TiuiAmigos_DireccionId",
                table: "TiuiAmigos",
                column: "DireccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NombreUsuario",
                table: "Usuarios",
                column: "NombreUsuario",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BitacoraGuias_Guias_GuiaId",
                table: "BitacoraGuias",
                column: "GuiaId",
                principalTable: "Guias",
                principalColumn: "GuiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CofiguracionesCajaTiuiAmigo_TiuiAmigos_TiuiAmigoId",
                table: "CofiguracionesCajaTiuiAmigo",
                column: "TiuiAmigoId",
                principalTable: "TiuiAmigos",
                principalColumn: "TiuiAmigoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Direcciones_TiuiAmigos_TiuiAmigoId",
                table: "Direcciones",
                column: "TiuiAmigoId",
                principalTable: "TiuiAmigos",
                principalColumn: "TiuiAmigoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direcciones_TiuiAmigos_TiuiAmigoId",
                table: "Direcciones");

            migrationBuilder.DropTable(
                name: "BitacoraGuias");

            migrationBuilder.DropTable(
                name: "CofiguracionesCajaTiuiAmigo");

            migrationBuilder.DropTable(
                name: "ConfiguracionApp");

            migrationBuilder.DropTable(
                name: "DireccionesGuia");

            migrationBuilder.DropTable(
                name: "EvidenciaEntrega");

            migrationBuilder.DropTable(
                name: "NotificacionClientes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Guias");

            migrationBuilder.DropTable(
                name: "Estatus");

            migrationBuilder.DropTable(
                name: "Paqueterias");

            migrationBuilder.DropTable(
                name: "Paquetes");

            migrationBuilder.DropTable(
                name: "TiuiAmigos");

            migrationBuilder.DropTable(
                name: "Archivo");

            migrationBuilder.DropTable(
                name: "Direcciones");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}
