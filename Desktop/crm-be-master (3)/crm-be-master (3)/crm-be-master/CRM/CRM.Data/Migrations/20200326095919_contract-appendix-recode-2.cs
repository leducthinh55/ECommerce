using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class contractappendixrecode2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 3, 26, 16, 59, 19, 200, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 3, 26, 16, 54, 46, 960, DateTimeKind.Local));

            migrationBuilder.AlterColumn<double>(
                name: "Square",
                table: "ContractAppendices",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "ContractAppendices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "ContractAppendices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 3, 26, 16, 54, 46, 960, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 3, 26, 16, 59, 19, 200, DateTimeKind.Local));

            migrationBuilder.AlterColumn<double>(
                name: "Square",
                table: "ContractAppendices",
                nullable: true,
                oldClrType: typeof(double));
        }
    }
}
