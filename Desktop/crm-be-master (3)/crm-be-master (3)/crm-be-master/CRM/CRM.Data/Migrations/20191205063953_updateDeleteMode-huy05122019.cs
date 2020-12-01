using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class updateDeleteModehuy05122019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amounts_Customers_CustomerId",
                table: "Amounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Customers_CustomerId",
                table: "Owners");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Customers_CustomerId",
                table: "Personnels");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 12, 5, 13, 39, 52, 323, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 12, 5, 12, 24, 41, 431, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_Amounts_Customers_CustomerId",
                table: "Amounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Customers_CustomerId",
                table: "Owners",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Customers_CustomerId",
                table: "Personnels",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amounts_Customers_CustomerId",
                table: "Amounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Customers_CustomerId",
                table: "Owners");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Customers_CustomerId",
                table: "Personnels");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 12, 5, 12, 24, 41, 431, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 12, 5, 13, 39, 52, 323, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_Amounts_Customers_CustomerId",
                table: "Amounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Customers_CustomerId",
                table: "Owners",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Customers_CustomerId",
                table: "Personnels",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
