using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class cocontract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "CooperationContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ParnerId = table.Column<Guid>(nullable: false),
                    DateSinged = table.Column<DateTime>(nullable: false),
                    Percentage = table.Column<int>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CooperationContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CooperationContracts_Customers_ParnerId",
                        column: x => x.ParnerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoContractTelService",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<Guid>(nullable: false),
                    ServiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoContractTelService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoContractTelService_CooperationContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "CooperationContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoContractTelService_Telecomservices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Telecomservices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCoContract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CooperationContractId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCoContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCoContract_CooperationContracts_CooperationContractId",
                        column: x => x.CooperationContractId,
                        principalTable: "CooperationContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCoContractServiceItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SubContractId = table.Column<Guid>(nullable: false),
                    ServiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCoContractServiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCoContractServiceItem_SubCoContract_SubContractId",
                        column: x => x.SubContractId,
                        principalTable: "SubCoContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoContractTelService_ContractId",
                table: "CoContractTelService",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_CoContractTelService_ServiceId",
                table: "CoContractTelService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CooperationContracts_ParnerId",
                table: "CooperationContracts",
                column: "ParnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCoContract_CooperationContractId",
                table: "SubCoContract",
                column: "CooperationContractId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCoContractServiceItem_SubContractId",
                table: "SubCoContractServiceItem",
                column: "SubContractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoContractTelService");

            migrationBuilder.DropTable(
                name: "SubCoContractServiceItem");

            migrationBuilder.DropTable(
                name: "SubCoContract");

            migrationBuilder.DropTable(
                name: "CooperationContracts");

            migrationBuilder.DropColumn(
                name: "DateService",
                table: "ContractAppendices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 2, 21, 23, 25, 56, 7, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 2, 25, 15, 1, 14, 971, DateTimeKind.Local));
        }
    }
}
