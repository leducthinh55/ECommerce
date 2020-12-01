using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class CreateCommonTelecomService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommonName",
                table: "Telecomservices");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Telecomservices");

            migrationBuilder.AddColumn<Guid>(
                name: "CommonTelecomserviceId",
                table: "Telecomservices",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 2, 21, 23, 22, 3, 823, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 2, 21, 17, 11, 9, 567, DateTimeKind.Local));

            migrationBuilder.CreateTable(
                name: "CommonTelecomservices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonTelecomservices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Telecomservices_CommonTelecomserviceId",
                table: "Telecomservices",
                column: "CommonTelecomserviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Telecomservices_CommonTelecomservices_CommonTelecomserviceId",
                table: "Telecomservices",
                column: "CommonTelecomserviceId",
                principalTable: "CommonTelecomservices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telecomservices_CommonTelecomservices_CommonTelecomserviceId",
                table: "Telecomservices");

            migrationBuilder.DropTable(
                name: "CommonTelecomservices");

            migrationBuilder.DropIndex(
                name: "IX_Telecomservices_CommonTelecomserviceId",
                table: "Telecomservices");

            migrationBuilder.DropColumn(
                name: "CommonTelecomserviceId",
                table: "Telecomservices");

            migrationBuilder.AddColumn<string>(
                name: "CommonName",
                table: "Telecomservices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Telecomservices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 2, 21, 17, 11, 9, 567, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 2, 21, 23, 22, 3, 823, DateTimeKind.Local));
        }
    }
}
