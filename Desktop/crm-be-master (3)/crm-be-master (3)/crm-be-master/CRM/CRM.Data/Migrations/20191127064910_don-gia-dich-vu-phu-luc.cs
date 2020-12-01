using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class dongiadichvuphuluc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelUpPrice",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "AreaRevolution",
                table: "ContractAppendices");

            migrationBuilder.RenameColumn(
                name: "PriceRevolution",
                table: "ContractAppendices",
                newName: "SquareRevolution");

          
            migrationBuilder.AlterColumn<decimal>(
                name: "UnitServicePrice",
                table: "Contracts",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Contracts",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Square",
                table: "Contracts",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LevelUpUnitPrice",
                table: "Contracts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "LevelUpUnitServicePrice",
                table: "Contracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "ContractAppendices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitServicePrice",
                table: "ContractAppendices",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelUpUnitPrice",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LevelUpUnitServicePrice",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "ContractAppendices");

            migrationBuilder.DropColumn(
                name: "UnitServicePrice",
                table: "ContractAppendices");

            migrationBuilder.RenameColumn(
                name: "SquareRevolution",
                table: "ContractAppendices",
                newName: "PriceRevolution");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 26, 14, 26, 47, 790, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 27, 13, 49, 9, 622, DateTimeKind.Local));

            migrationBuilder.AlterColumn<double>(
                name: "UnitServicePrice",
                table: "Contracts",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "Contracts",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Square",
                table: "Contracts",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<double>(
                name: "LevelUpPrice",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AreaRevolution",
                table: "ContractAppendices",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
