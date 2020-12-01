using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class reftelecomcontracttocustomerwf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 11, 16, 28, 44, 466, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 11, 15, 45, 35, 425, DateTimeKind.Local));

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerWorkflowId",
                table: "ContractTelecoms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerWorkflowId",
                table: "ContractTelecoms");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 11, 15, 45, 35, 425, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 11, 16, 28, 44, 466, DateTimeKind.Local));
        }
    }
}
