using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class aabbccd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 3, 28, 16, 13, 57, 219, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 3, 28, 16, 10, 26, 12, DateTimeKind.Local));

            migrationBuilder.AlterColumn<double>(
                name: "Square",
                table: "ContractAppendices",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 3, 28, 16, 10, 26, 12, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 3, 28, 16, 13, 57, 219, DateTimeKind.Local));

            migrationBuilder.AlterColumn<double>(
                name: "Square",
                table: "ContractAppendices",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
