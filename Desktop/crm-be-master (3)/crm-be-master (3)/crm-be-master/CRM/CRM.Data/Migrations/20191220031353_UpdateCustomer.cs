using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UpdateCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ContractTelecoms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ContractTelecoms");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "ContractTelecoms");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "ContractTelecoms");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 12, 20, 10, 13, 52, 85, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 12, 18, 12, 53, 29, 769, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 12, 18, 12, 53, 29, 769, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 12, 20, 10, 13, 52, 85, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ContractTelecoms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ContractTelecoms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "ContractTelecoms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "ContractTelecoms",
                nullable: true);
        }
    }
}
