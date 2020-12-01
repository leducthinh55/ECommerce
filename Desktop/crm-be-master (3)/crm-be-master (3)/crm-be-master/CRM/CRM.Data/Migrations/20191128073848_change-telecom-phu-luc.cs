using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class changetelecomphuluc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 28, 14, 38, 47, 808, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 28, 12, 37, 20, 981, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "ContractTelecomAppendices",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAccept",
                table: "ContractTelecomAppendices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ContractTelecomAppendices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ContractTelecomAppendices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAccept",
                table: "ContractTelecomAppendices");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "ContractTelecomAppendices");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ContractTelecomAppendices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 28, 12, 37, 20, 981, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 28, 14, 38, 47, 808, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "ContractTelecomAppendices",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
