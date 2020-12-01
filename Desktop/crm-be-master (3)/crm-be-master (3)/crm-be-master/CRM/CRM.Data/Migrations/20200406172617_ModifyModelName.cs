using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class ModifyModelName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalLecturersOutSide",
                table: "Personnels",
                newName: "TotalLecturersOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturersInSide",
                table: "Personnels",
                newName: "TotalLecturersInside");

            migrationBuilder.RenameColumn(
                name: "TotalFemaleEmployeeOutside",
                table: "Personnels",
                newName: "TotalFeMaleEmployeeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalFemaleEmployeeInside",
                table: "Personnels",
                newName: "TotalFeMaleEmployeeInside");

            migrationBuilder.RenameColumn(
                name: "TotalFemaleEmployee",
                table: "Personnels",
                newName: "TotalFeMaleEmployee");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsidecUniversity",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsideUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsidecOrther",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsideOrther");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsidecMaster",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsideMaster");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsidecIntermediate",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsideIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsidecDoctor",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsideDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsidecCollege",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsideCollege");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeInsideOrther",
                table: "Personnels",
                newName: "TotalDomesticEmployeeInsideOther");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 4, 7, 0, 26, 17, 115, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 4, 4, 20, 50, 41, 619, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalLecturersOutside",
                table: "Personnels",
                newName: "TotalLecturersOutSide");

            migrationBuilder.RenameColumn(
                name: "TotalLecturersInside",
                table: "Personnels",
                newName: "TotalLecturersInSide");

            migrationBuilder.RenameColumn(
                name: "TotalFeMaleEmployeeOutside",
                table: "Personnels",
                newName: "TotalFemaleEmployeeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalFeMaleEmployeeInside",
                table: "Personnels",
                newName: "TotalFemaleEmployeeInside");

            migrationBuilder.RenameColumn(
                name: "TotalFeMaleEmployee",
                table: "Personnels",
                newName: "TotalFemaleEmployee");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsideUniversity",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsidecUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsideOrther",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsidecOrther");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsideMaster",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsidecMaster");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsideIntermediate",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsidecIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsideDoctor",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsidecDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsideCollege",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsidecCollege");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeInsideOther",
                table: "Personnels",
                newName: "TotalDomesticEmployeeInsideOrther");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2020, 4, 4, 20, 50, 41, 619, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 4, 7, 0, 26, 17, 115, DateTimeKind.Local));
        }
    }
}
