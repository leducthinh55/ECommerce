using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class building_floor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

           
          

            migrationBuilder.AddColumn<string>(
                name: "Building",
                table: "ContractAppendices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Floor",
                table: "ContractAppendices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "ContractAppendices",
                nullable: true);

      
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Building",
                table: "ContractAppendices");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "ContractAppendices");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "ContractAppendices");

        }
    }
}
