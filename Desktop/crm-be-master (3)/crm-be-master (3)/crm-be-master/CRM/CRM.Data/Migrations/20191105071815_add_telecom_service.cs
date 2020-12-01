using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class add_telecom_service : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractTelecomAppendix_contractTelecoms_ContractTelecomId",
                table: "ContractTelecomAppendix");

            migrationBuilder.DropForeignKey(
                name: "FK_contractTelecoms_Customers_CustomerId",
                table: "contractTelecoms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contractTelecoms",
                table: "contractTelecoms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractTelecomAppendix",
                table: "ContractTelecomAppendix");

            migrationBuilder.RenameTable(
                name: "contractTelecoms",
                newName: "ContractTelecoms");

            migrationBuilder.RenameTable(
                name: "ContractTelecomAppendix",
                newName: "ContractTelecomAppendices");

            migrationBuilder.RenameIndex(
                name: "IX_contractTelecoms_CustomerId",
                table: "ContractTelecoms",
                newName: "IX_ContractTelecoms_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractTelecomAppendix_ContractTelecomId",
                table: "ContractTelecomAppendices",
                newName: "IX_ContractTelecomAppendices_ContractTelecomId");

          

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractTelecoms",
                table: "ContractTelecoms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractTelecomAppendices",
                table: "ContractTelecomAppendices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Telecomservices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telecomservices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelecomserviceParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TelecomServiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelecomserviceParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelecomserviceParameters_Telecomservices_TelecomServiceId",
                        column: x => x.TelecomServiceId,
                        principalTable: "Telecomservices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelecomserviceParameters_TelecomServiceId",
                table: "TelecomserviceParameters",
                column: "TelecomServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTelecomAppendices_ContractTelecoms_ContractTelecomId",
                table: "ContractTelecomAppendices",
                column: "ContractTelecomId",
                principalTable: "ContractTelecoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTelecoms_Customers_CustomerId",
                table: "ContractTelecoms",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractTelecomAppendices_ContractTelecoms_ContractTelecomId",
                table: "ContractTelecomAppendices");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractTelecoms_Customers_CustomerId",
                table: "ContractTelecoms");

            migrationBuilder.DropTable(
                name: "TelecomserviceParameters");

            migrationBuilder.DropTable(
                name: "Telecomservices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractTelecoms",
                table: "ContractTelecoms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractTelecomAppendices",
                table: "ContractTelecomAppendices");

            migrationBuilder.RenameTable(
                name: "ContractTelecoms",
                newName: "contractTelecoms");

            migrationBuilder.RenameTable(
                name: "ContractTelecomAppendices",
                newName: "ContractTelecomAppendix");

            migrationBuilder.RenameIndex(
                name: "IX_ContractTelecoms_CustomerId",
                table: "contractTelecoms",
                newName: "IX_contractTelecoms_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractTelecomAppendices_ContractTelecomId",
                table: "ContractTelecomAppendix",
                newName: "IX_ContractTelecomAppendix_ContractTelecomId");

           

            migrationBuilder.AddPrimaryKey(
                name: "PK_contractTelecoms",
                table: "contractTelecoms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractTelecomAppendix",
                table: "ContractTelecomAppendix",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTelecomAppendix_contractTelecoms_ContractTelecomId",
                table: "ContractTelecomAppendix",
                column: "ContractTelecomId",
                principalTable: "contractTelecoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_contractTelecoms_Customers_CustomerId",
                table: "contractTelecoms",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
