using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class abcde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 9, 1, 13, 5, 13, 894, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 16, 19, 55, 30, 893, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 16, 19, 55, 30, 893, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 9, 1, 13, 5, 13, 894, DateTimeKind.Local));
        }
    }
}
