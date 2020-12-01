using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class add_telecom_service_contract_appendix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "TelecomserviceContractAppendices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractAppendixId = table.Column<Guid>(nullable: false),
                    TelecomserviceId = table.Column<Guid>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelecomserviceContractAppendices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelecomserviceContractAppendices_ContractTelecomAppendices_ContractAppendixId",
                        column: x => x.ContractAppendixId,
                        principalTable: "ContractTelecomAppendices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelecomserviceContractAppendices_Telecomservices_TelecomserviceId",
                        column: x => x.TelecomserviceId,
                        principalTable: "Telecomservices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelecomserviceContractAppendices_ContractAppendixId",
                table: "TelecomserviceContractAppendices",
                column: "ContractAppendixId");

            migrationBuilder.CreateIndex(
                name: "IX_TelecomserviceContractAppendices_TelecomserviceId",
                table: "TelecomserviceContractAppendices",
                column: "TelecomserviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelecomserviceContractAppendices");

        
        }
    }
}
