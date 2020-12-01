using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class update_dashBoardChart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DashBoardCharts_Customers_CustomerId",
                table: "DashBoardCharts");

            migrationBuilder.DropIndex(
                name: "IX_DashBoardCharts_CustomerId",
                table: "DashBoardCharts");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "DashBoardCharts");

            migrationBuilder.DropColumn(
                name: "TotalInvestors",
                table: "DashBoardCharts");

            migrationBuilder.AddColumn<long>(
                name: "TotalAlumnus",
                table: "DashBoardCharts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TotalDomestic",
                table: "DashBoardCharts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TotalEmployee",
                table: "DashBoardCharts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TotalInternational",
                table: "DashBoardCharts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TotalPeoples",
                table: "DashBoardCharts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TotalSoftWare",
                table: "DashBoardCharts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 15, 21, 3, 0, 220, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 15, 17, 14, 36, 338, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAlumnus",
                table: "DashBoardCharts");

            migrationBuilder.DropColumn(
                name: "TotalDomestic",
                table: "DashBoardCharts");

            migrationBuilder.DropColumn(
                name: "TotalEmployee",
                table: "DashBoardCharts");

            migrationBuilder.DropColumn(
                name: "TotalInternational",
                table: "DashBoardCharts");

            migrationBuilder.DropColumn(
                name: "TotalPeoples",
                table: "DashBoardCharts");

            migrationBuilder.DropColumn(
                name: "TotalSoftWare",
                table: "DashBoardCharts");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "DashBoardCharts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalInvestors",
                table: "DashBoardCharts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 15, 17, 14, 36, 338, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 15, 21, 3, 0, 220, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_DashBoardCharts_CustomerId",
                table: "DashBoardCharts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DashBoardCharts_Customers_CustomerId",
                table: "DashBoardCharts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
