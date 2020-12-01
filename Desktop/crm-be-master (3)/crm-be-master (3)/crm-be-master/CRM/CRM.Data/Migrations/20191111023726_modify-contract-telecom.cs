using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class modifycontracttelecom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "ContractTelecoms");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "ContractTelecoms");

            migrationBuilder.DropColumn(
                name: "Functional",
                table: "ContractTelecoms");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ContractTelecoms");

            migrationBuilder.RenameColumn(
                name: "Nation",
                table: "ContractTelecoms",
                newName: "ContractNo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 11, 9, 37, 26, 461, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 5, 14, 23, 23, 357, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractNo",
                table: "ContractTelecoms",
                newName: "Nation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 5, 14, 23, 23, 357, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 11, 9, 37, 26, 461, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ContractTelecoms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ContractTelecoms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Functional",
                table: "ContractTelecoms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "ContractTelecoms",
                nullable: true);
        }
    }
}
