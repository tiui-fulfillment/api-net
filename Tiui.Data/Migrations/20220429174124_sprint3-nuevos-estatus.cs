using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tiui.Data.Migrations
{
    public partial class sprint3nuevosestatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Estatus",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoFlujo",
                table: "Estatus",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 1,
                column: "Descripcion",
                value: "Envío documentado (consolidado)");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 2,
                column: "Descripcion",
                value: "En camino a CDMX");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 3,
                column: "Descripcion",
                value: "Recepción de envío CEDIS Tiui CDMX");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 4,
                column: "Descripcion",
                value: "En CEDIS Tiui CDMX (consolidado)");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 5,
                columns: new[] { "Descripcion", "TipoFlujo" },
                values: new object[] { "Envío documentado (fulfillment)", 1 });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 6,
                columns: new[] { "Descripcion", "TipoFlujo" },
                values: new object[] { "Preparando paquete", 1 });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 7,
                columns: new[] { "Descripcion", "TipoFlujo" },
                values: new object[] { "En CEDIS Tiui CDMX (fulfillment)", 1 });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 8,
                columns: new[] { "Descripcion", "TipoFlujo" },
                values: new object[] { "En espera de producto", 1 });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 9,
                columns: new[] { "Descripcion", "TipoFlujo" },
                values: new object[] { "En ruta de entrega", 99 });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 10,
                columns: new[] { "Descripcion", "TipoFlujo" },
                values: new object[] { "1er intento de entrega", 99 });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 11,
                columns: new[] { "Descripcion", "TipoFlujo" },
                values: new object[] { "Envío reagendado", 99 });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 12,
                columns: new[] { "Descripcion", "TipoFlujo" },
                values: new object[] { "Cancelado", 99 });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 13,
                columns: new[] { "Descripcion", "TipoFlujo" },
                values: new object[] { "Entregado", 99 });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 14,
                columns: new[] { "Descripcion", "TipoFlujo" },
                values: new object[] { "Conciliado", 99 });

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 15,
                columns: new[] { "Descripcion", "TipoFlujo" },
                values: new object[] { "Pagado", 99 });

            migrationBuilder.InsertData(
                table: "Estatus",
                columns: new[] { "EstatusId", "Activo", "Descripcion", "Nombre", "Proceso", "TipoFlujo" },
                values: new object[,]
                {
                    { 16, true, "Envío documentado (recolección)", "Envío documentado recolección", "Preparando", 2 },
                    { 17, true, "En camino a recolección", "En camino a recolección", "Preparando", 2 },
                    { 18, true, "Envío recolectado", "Envío recolectado", "Preparando", 2 },
                    { 19, true, "En CEDIS Tiui CDMX (recolección)", "Tu envío está en CEDIS Tiui CDMX recolección", "Preparando", 2 }
                });

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1913));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1919));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 3,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1923));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 4,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1926));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 5,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1929));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 6,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1932));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 7,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1936));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 8,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1939));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 9,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1942));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 10,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1946));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 11,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1949));

            migrationBuilder.UpdateData(
                table: "Paqueterias",
                keyColumn: "PaqueteriaId",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1876));

            migrationBuilder.UpdateData(
                table: "Paqueterias",
                keyColumn: "PaqueteriaId",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2022, 4, 29, 12, 41, 23, 626, DateTimeKind.Local).AddTicks(1884));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 19);

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Estatus");

            migrationBuilder.DropColumn(
                name: "TipoFlujo",
                table: "Estatus");

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2228));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2231));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 3,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2233));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 4,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2236));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 5,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2238));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 6,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2240));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 7,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2242));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 8,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2244));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 9,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2247));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 10,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2249));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 11,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2251));

            migrationBuilder.UpdateData(
                table: "Paqueterias",
                keyColumn: "PaqueteriaId",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2201));

            migrationBuilder.UpdateData(
                table: "Paqueterias",
                keyColumn: "PaqueteriaId",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 25, 13, 43, 1, 8, DateTimeKind.Local).AddTicks(2207));
        }
    }
}
