using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class add_contract_telecom_appendix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.CreateTable(
                name: "ContractTelecomAppendix",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ContractCode = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    ContractTelecomId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTelecomAppendix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractTelecomAppendix_contractTelecoms_ContractTelecomId",
                        column: x => x.ContractTelecomId,
                        principalTable: "contractTelecoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractTelecomAppendix_ContractTelecomId",
                table: "ContractTelecomAppendix",
                column: "ContractTelecomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractTelecomAppendix");

        }
    }
}
