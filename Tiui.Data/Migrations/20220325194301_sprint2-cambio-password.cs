using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tiui.Data.Migrations
{
    public partial class sprint2cambiopassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodigoReestablecerPassword",
                table: "Usuarios",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoReestablecerPassword",
                table: "Usuarios");

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
    }
}
