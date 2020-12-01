using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UpdateCommonService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 2, 21, 23, 25, 56, 7, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 2, 21, 23, 22, 3, 823, DateTimeKind.Local));

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "CommonTelecomservices",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 2, 21, 23, 22, 3, 823, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 2, 21, 23, 25, 56, 7, DateTimeKind.Local));

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "CommonTelecomservices",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
