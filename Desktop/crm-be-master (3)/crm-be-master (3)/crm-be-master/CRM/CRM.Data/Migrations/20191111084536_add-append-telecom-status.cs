using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class addappendtelecomstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TelecomserviceContractAppendices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 11, 15, 45, 35, 425, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 11, 11, 34, 23, 970, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TelecomserviceContractAppendices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 11, 11, 34, 23, 970, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 11, 15, 45, 35, 425, DateTimeKind.Local));
        }
    }
}
