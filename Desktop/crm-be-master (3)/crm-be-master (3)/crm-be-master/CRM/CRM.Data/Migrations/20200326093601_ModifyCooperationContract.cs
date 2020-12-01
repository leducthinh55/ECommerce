using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class ModifyCooperationContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "CooperationContracts");

            migrationBuilder.AddColumn<Guid>(
                name: "CoContractTelServiceId",
                table: "SubCoContractServiceItem",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TelecomserviceId",
                table: "SubCoContractServiceItem",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 3, 26, 16, 36, 0, 55, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 3, 13, 2, 40, 47, 399, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "AppendixLink",
                table: "CoContractTelService",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "CoContractTelService",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "CoContractTelService",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "CoContractTelService",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Percentage",
                table: "CoContractTelService",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoContractTelServiceId",
                table: "SubCoContractServiceItem");

            migrationBuilder.DropColumn(
                name: "TelecomserviceId",
                table: "SubCoContractServiceItem");

            migrationBuilder.DropColumn(
                name: "AppendixLink",
                table: "CoContractTelService");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "CoContractTelService");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "CoContractTelService");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "CoContractTelService");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "CoContractTelService");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 3, 13, 2, 40, 47, 399, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 3, 26, 16, 36, 0, 55, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "Percentage",
                table: "CooperationContracts",
                nullable: false,
                defaultValue: 0);
        }
    }
}
