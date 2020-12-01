using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UpdateCustomerHuy20012020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignDayActivities",
                table: "Customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 1, 20, 0, 7, 0, 232, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 12, 24, 15, 48, 43, 653, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 12, 24, 15, 48, 43, 653, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 1, 20, 0, 7, 0, 232, DateTimeKind.Local));

            migrationBuilder.AddColumn<DateTime>(
                name: "SignDayActivities",
                table: "Customers",
                nullable: true);
        }
    }
}
