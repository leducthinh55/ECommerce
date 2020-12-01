using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class contractappendixrecode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateService",
                table: "ContractAppendices");

            migrationBuilder.DropColumn(
                name: "IsIncrease",
                table: "ContractAppendices");

            migrationBuilder.RenameColumn(
                name: "SquareRevolution",
                table: "ContractAppendices",
                newName: "Square");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 3, 26, 16, 54, 46, 960, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 3, 13, 2, 40, 47, 399, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Square",
                table: "ContractAppendices",
                newName: "SquareRevolution");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 3, 13, 2, 40, 47, 399, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 3, 26, 16, 54, 46, 960, DateTimeKind.Local));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateService",
                table: "ContractAppendices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncrease",
                table: "ContractAppendices",
                nullable: true);
        }
    }
}
