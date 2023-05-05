using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tiui.Data.Migrations
{
    public partial class sprint2updatestatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaRegistro",
                table: "Guias",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<long>(
                name: "BitacoraGuiaId",
                table: "BitacoraGuias",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 1,
                column: "Nombre",
                value: "Tu envío ha sido documentado consolidado");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 4,
                column: "Nombre",
                value: "Tu envío está en CEDIS Tiui CDMX consolidado");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 5,
                column: "Nombre",
                value: "Tu envío ha sido documentado fulfillment");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 7,
                column: "Nombre",
                value: "Tu envío está en CEDIS Tiui CDMX fulfillment");

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4889));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4895));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 3,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4898));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 4,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4901));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 5,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4904));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 6,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4907));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 7,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4910));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 8,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4913));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 9,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4916));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 10,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4920));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 11,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4923));

            migrationBuilder.UpdateData(
                table: "Paqueterias",
                keyColumn: "PaqueteriaId",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4850));

            migrationBuilder.UpdateData(
                table: "Paqueterias",
                keyColumn: "PaqueteriaId",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 17, 20, 0, 58, 363, DateTimeKind.Local).AddTicks(4858));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaRegistro",
                table: "Guias",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BitacoraGuiaId",
                table: "BitacoraGuias",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 1,
                column: "Nombre",
                value: "Tu envío ha sido documentado");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 4,
                column: "Nombre",
                value: "Tu envío está en CEDIS Tiui CDMX");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 5,
                column: "Nombre",
                value: "Tu envío ha sido documentado");

            migrationBuilder.UpdateData(
                table: "Estatus",
                keyColumn: "EstatusId",
                keyValue: 7,
                column: "Nombre",
                value: "Tu envío está en CEDIS Tiui CDMX");

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7396));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7405));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 3,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7408));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 4,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7411));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 5,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7414));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 6,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7417));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 7,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 8,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7422));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 9,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7425));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 10,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7428));

            migrationBuilder.UpdateData(
                table: "MotivosCancelaciones",
                keyColumn: "MotivoCancelacionId",
                keyValue: 11,
                column: "FechaRegistro",
                value: new DateTime(2022, 3, 3, 17, 45, 30, 892, DateTimeKind.Local).AddTicks(7431));

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
        }
    }
}
