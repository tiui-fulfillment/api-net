using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tiui.Data.Migrations
{
    public partial class sprint2statuscancelacionesguia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 18);

            migrationBuilder.CreateTable(
                name: "MotivosCancelaciones",
                columns: table => new
                {
                    MotivoCancelacionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    TipoCancelacion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivosCancelaciones", x => x.MotivoCancelacionId);
                });

            migrationBuilder.CreateTable(
                name: "CancelacionGuias",
                columns: table => new
                {
                    CancelacionGuiaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    GuiaId = table.Column<long>(type: "bigint", nullable: true),
                    MotivoCancelacionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelacionGuias", x => x.CancelacionGuiaId);
                    table.ForeignKey(
                        name: "FK_CancelacionGuias_Guias_GuiaId",
                        column: x => x.GuiaId,
                        principalTable: "Guias",
                        principalColumn: "GuiaId");
                    table.ForeignKey(
                        name: "FK_CancelacionGuias_MotivosCancelaciones_MotivoCancelacionId",
                        column: x => x.MotivoCancelacionId,
                        principalTable: "MotivosCancelaciones",
                        principalColumn: "MotivoCancelacionId");
                });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 8,
                column: "Nombre",
                value: "Oops! El inventario se ha agotado. Estamos en espera de que el proveedor reabastezca tu producto.");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 11,
                column: "Nombre",
                value: "Tu envío se reagendó");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 12,
                column: "Nombre",
                value: "Envío Cancelado");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 13,
                columns: new[] { "Nombre", "Proceso" },
                values: new object[] { "Entregado", "Celebrando" });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 14,
                columns: new[] { "Nombre", "Proceso" },
                values: new object[] { "Conciliado", "Celebrando" });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 15,
                columns: new[] { "Nombre", "Proceso" },
                values: new object[] { "Pagado", "Celebrando" });

            migrationBuilder.InsertData(
                table: "MotivosCancelaciones",
                columns: new[] { "MotivoCancelacionId", "Activo", "Descripcion", "FechaRegistro", "TipoCancelacion" },
                values: new object[,]
                {
                    { 1, true, "No tiene dinero", new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7396), 1 },
                    { 2, true, "Encontró el producto mas barato", new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7405), 1 },
                    { 3, true, "Retraso en entrega", new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7408), 1 },
                    { 4, true, "No se puede contactar con el cliente", new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7411), 1 },
                    { 5, true, "No hay inventario", new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7414), 1 },
                    { 6, true, "No reconoce la compra", new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7417), 1 },
                    { 7, true, "Cliente ya no quiere el producto", new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7420), 1 },
                    { 8, true, "Por segundo intento de entrega", new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7422), 2 },
                    { 9, true, "Por rechazo de producto", new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7425), 2 },
                    { 10, true, "Por falta de dinero en la entrega", new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7428), 2 },
                    { 11, true, "No se localizó al cliente", new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7431), 2 }
                });

            migrationBuilder.UpdateData(
                table: "Paqueterias",
                keyColumn: "PaqueteriaId",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7333));

            migrationBuilder.UpdateData(
                table: "Paqueterias",
                keyColumn: "PaqueteriaId",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7341));

            migrationBuilder.CreateIndex(
                name: "IX_CancelacionGuias_GuiaId",
                table: "CancelacionGuias",
                column: "GuiaId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelacionGuias_MotivoCancelacionId",
                table: "CancelacionGuias",
                column: "MotivoCancelacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelacionGuias");

            migrationBuilder.DropTable(
                name: "MotivosCancelaciones");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 8,
                column: "Nombre",
                value: "Oops! El inventario se ha agotado. Estamos en espera de que el proveedor reabastezca tu producto. ");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 11,
                column: "Nombre",
                value: "Se realizó el 2do intento de entrega");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 12,
                column: "Nombre",
                value: "Tu envío se reagendó");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 13,
                columns: new[] { "Nombre", "Proceso" },
                values: new object[] { "Tu envío ha sido cancelado", "En camino" });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 14,
                columns: new[] { "Nombre", "Proceso" },
                values: new object[] { "Cancelado en ruta", "En camino" });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 15,
                columns: new[] { "Nombre", "Proceso" },
                values: new object[] { "Rechazado", "En camino" });

            migrationBuilder.InsertData(
                table: "Estatus",
                columns: new[] { "EstatusId", "Activo", "Nombre", "Proceso" },
                values: new object[,]
                {
                    { 16, true, "Entregado", "Celebrando" },
                    { 17, true, "Conciliado", "Celebrando" },
                    { 18, true, "Pagado", "Celebrando" }
                });

            migrationBuilder.UpdateData(
                table: "Paqueterias",
                keyColumn: "PaqueteriaId",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2022, 2, 21, 16, 56, 38, 886, DateTimeKind.Local).AddTicks(7897));

            migrationBuilder.UpdateData(
                table: "Paqueterias",
                keyColumn: "PaqueteriaId",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2022, 2, 21, 16, 56, 38, 886, DateTimeKind.Local).AddTicks(7910));
        }
    }
}
