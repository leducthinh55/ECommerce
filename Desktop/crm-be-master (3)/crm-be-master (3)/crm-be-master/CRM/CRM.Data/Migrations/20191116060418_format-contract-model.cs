using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class formatcontractmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CustomerWorkFlowCode",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DownSquare",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "OtherPrice",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ServiceAmount",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "UsingSquare",
                table: "Contracts",
                newName: "UnitServicePrice");

            migrationBuilder.RenameColumn(
                name: "UpSquare",
                table: "Contracts",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "ContractDate",
                table: "Contracts",
                newName: "StartDateService");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 16, 13, 4, 18, 112, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 15, 10, 16, 59, 533, DateTimeKind.Local));

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Contracts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerWorkflowId",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateRent",
                table: "Contracts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CustomerId",
                table: "Contracts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Customers_CustomerId",
                table: "Contracts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Customers_CustomerId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_CustomerId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CustomerWorkflowId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "StartDateRent",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "UnitServicePrice",
                table: "Contracts",
                newName: "UsingSquare");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Contracts",
                newName: "UpSquare");

            migrationBuilder.RenameColumn(
                name: "StartDateService",
                table: "Contracts",
                newName: "ContractDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 15, 10, 16, 59, 533, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 16, 13, 4, 18, 112, DateTimeKind.Local));

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Contracts",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerWorkFlowCode",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DownSquare",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherPrice",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ServiceAmount",
                table: "Contracts",
                nullable: true);
        }
    }
}
