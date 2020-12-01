using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class ModifyPersonelhuy051220193 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalExportVndRevenueOutsidde",
                table: "Amounts",
                newName: "TotalExportVndRevenueOutside");

            migrationBuilder.RenameColumn(
                name: "TotalExportVndRevenueInsidde",
                table: "Amounts",
                newName: "TotalExportVndRevenueInside");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 12, 5, 15, 52, 40, 535, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 12, 5, 15, 48, 41, 205, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalExportVndRevenueOutside",
                table: "Amounts",
                newName: "TotalExportVndRevenueOutsidde");

            migrationBuilder.RenameColumn(
                name: "TotalExportVndRevenueInside",
                table: "Amounts",
                newName: "TotalExportVndRevenueInsidde");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 12, 5, 15, 48, 41, 205, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 12, 5, 15, 52, 40, 535, DateTimeKind.Local));
        }
    }
}
