using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class hd_kd_11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "Floor_2",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Floor_3",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Floor_4",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LevelUpUnitPrice_2",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LevelUpUnitPrice_3",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LevelUpUnitPrice_4",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LevelUpUnitServicePrice_2",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LevelUpUnitServicePrice_3",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LevelUpUnitServicePrice_4",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Room_2",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Room_3",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Room_4",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Square_2",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Square_3",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Square_4",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateRent_2",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateRent_3",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateRent_4",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateService_2",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateService_3",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateService_4",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice_2",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice_3",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice_4",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitServicePrice_2",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitServicePrice_3",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitServicePrice_4",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpPriceDate_2",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpPriceDate_3",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpPriceDate_4",
                table: "Contracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Floor_2",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Floor_3",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Floor_4",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LevelUpUnitPrice_2",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LevelUpUnitPrice_3",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LevelUpUnitPrice_4",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LevelUpUnitServicePrice_2",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LevelUpUnitServicePrice_3",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LevelUpUnitServicePrice_4",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Room_2",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Room_3",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Room_4",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Square_2",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Square_3",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Square_4",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "StartDateRent_2",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "StartDateRent_3",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "StartDateRent_4",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "StartDateService_2",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "StartDateService_3",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "StartDateService_4",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UnitPrice_2",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UnitPrice_3",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UnitPrice_4",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UnitServicePrice_2",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UnitServicePrice_3",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UnitServicePrice_4",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UpPriceDate_2",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UpPriceDate_3",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UpPriceDate_4",
                table: "Contracts");

            
        }
    }
}
