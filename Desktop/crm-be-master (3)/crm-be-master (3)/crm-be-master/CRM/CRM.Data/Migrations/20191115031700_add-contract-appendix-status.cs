using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class addcontractappendixstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 15, 10, 16, 59, 533, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 15, 10, 1, 31, 531, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ContractAppendices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ContractAppendices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 15, 10, 1, 31, 531, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 15, 10, 16, 59, 533, DateTimeKind.Local));
        }
    }
}
