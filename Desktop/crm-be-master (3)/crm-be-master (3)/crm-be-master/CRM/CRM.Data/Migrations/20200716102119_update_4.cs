using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class update_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 16, 17, 21, 18, 577, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 16, 17, 19, 31, 424, DateTimeKind.Local));

            migrationBuilder.CreateTable(
                name: "DashBoardCharts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Time = table.Column<DateTime>(nullable: false),
                    VondautuSoftwares = table.Column<decimal>(nullable: false),
                    VondautuInvestors = table.Column<decimal>(nullable: false),
                    TotalSoftWare = table.Column<long>(nullable: false),
                    TotalDomestic = table.Column<long>(nullable: false),
                    TotalInternational = table.Column<long>(nullable: false),
                    TotalPeoples = table.Column<long>(nullable: false),
                    TotalEmployee = table.Column<long>(nullable: false),
                    TotalAlumnus = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashBoardCharts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DashBoardCharts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 16, 17, 19, 31, 424, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 16, 17, 21, 18, 577, DateTimeKind.Local));
        }
    }
}
