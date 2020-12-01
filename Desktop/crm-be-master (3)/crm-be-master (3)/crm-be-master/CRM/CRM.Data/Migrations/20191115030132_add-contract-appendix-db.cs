using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class addcontractappendixdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 15, 10, 1, 31, 531, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 11, 16, 28, 44, 466, DateTimeKind.Local));

            migrationBuilder.CreateTable(
                name: "ContractAppendices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    IsIncrease = table.Column<bool>(nullable: false),
                    AreaRevolution = table.Column<double>(nullable: false),
                    PriceIncrease = table.Column<double>(nullable: false),
                    ContractId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAppendices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAppendices_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractAppendices_ContractId",
                table: "ContractAppendices",
                column: "ContractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractAppendices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 11, 16, 28, 44, 466, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 15, 10, 1, 31, 531, DateTimeKind.Local));
        }
    }
}
