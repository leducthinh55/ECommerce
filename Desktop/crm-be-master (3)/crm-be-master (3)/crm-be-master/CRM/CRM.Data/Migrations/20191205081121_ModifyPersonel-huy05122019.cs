using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class ModifyPersonelhuy05122019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentTotalOutside",
                table: "Personnels",
                newName: "TotalStudentOutside");

            migrationBuilder.RenameColumn(
                name: "StudentTotalInside",
                table: "Personnels",
                newName: "TotalStudentInside");

            migrationBuilder.RenameColumn(
                name: "StudentTotal",
                table: "Personnels",
                newName: "TotalStudent");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalPartTimeOutside",
                table: "Personnels",
                newName: "TotalLecturerPartTimeOutside");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalPartTimeInside",
                table: "Personnels",
                newName: "TotalLecturerPartTimeInside");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalPartTime",
                table: "Personnels",
                newName: "TotalLecturerPartTime");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideInternationalUniversity",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalUniversity");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideInternationalProfessor",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalProfessor");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideInternationalOrther",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalOrther");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideInternationalMaster",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalMaster");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideInternationalIntermediate",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalIntermediate");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideInternationalDoctor",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalDoctor");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideInternationalCollege",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalCollege");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideInternationalAssociateProfessor",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideInternational",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternational");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideDomesticUniversity",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticUniversity");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideDomesticProfessor",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticProfessor");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideDomesticOrther",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticOrther");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideDomesticMaster",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticMaster");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideDomesticIntermediate",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticIntermediate");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideDomesticDoctor",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticDoctor");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideDomesticCollege",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticCollege");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideDomesticAssociateProfessor",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutsideDomestic",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomestic");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOutSide",
                table: "Personnels",
                newName: "TotalLecturerOutSide");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOfficalOutside",
                table: "Personnels",
                newName: "TotalLecturerOfficalOutside");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOfficalInside",
                table: "Personnels",
                newName: "TotalLecturerOfficalInside");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalOffical",
                table: "Personnels",
                newName: "TotalLecturerOffical");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalMaleOutside",
                table: "Personnels",
                newName: "TotalLecturerMaleOutside");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalMaleInside",
                table: "Personnels",
                newName: "TotalLecturerMaleInside");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalMale",
                table: "Personnels",
                newName: "TotalLecturerMale");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInternationalInsideProfessor",
                table: "Personnels",
                newName: "TotalLecturerInternationalInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInternationalInsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalLecturerInternationalInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInternationalInside",
                table: "Personnels",
                newName: "TotalLecturerInternationalInside");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInternational",
                table: "Personnels",
                newName: "TotalLecturerInternational");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInsideInternationalUniversity",
                table: "Personnels",
                newName: "TotalLecturerInsideInternationalUniversity");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInsideInternationalOrther",
                table: "Personnels",
                newName: "TotalLecturerInsideInternationalOrther");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInsideInternationalMaster",
                table: "Personnels",
                newName: "TotalLecturerInsideInternationalMaster");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInsideInternationalIntermediate",
                table: "Personnels",
                newName: "TotalLecturerInsideInternationalIntermediate");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInsideInternationalDoctor",
                table: "Personnels",
                newName: "TotalLecturerInsideInternationalDoctor");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInsideInternationalCollege",
                table: "Personnels",
                newName: "TotalLecturerInsideInternationalCollege");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInsideDomesticUniversity",
                table: "Personnels",
                newName: "TotalLecturerInsideDomesticUniversity");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInsideDomesticOrther",
                table: "Personnels",
                newName: "TotalLecturerInsideDomesticOrther");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInsideDomesticMaster",
                table: "Personnels",
                newName: "TotalLecturerInsideDomesticMaster");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInsideDomesticIntermediate",
                table: "Personnels",
                newName: "TotalLecturerInsideDomesticIntermediate");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInsideDomesticDoctor",
                table: "Personnels",
                newName: "TotalLecturerInsideDomesticDoctor");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInsideDomesticCollege",
                table: "Personnels",
                newName: "TotalLecturerInsideDomesticCollege");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalInSide",
                table: "Personnels",
                newName: "TotalLecturerInSide");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalFemaleOutside",
                table: "Personnels",
                newName: "TotalLecturerFemaleOutside");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalFemaleInside",
                table: "Personnels",
                newName: "TotalLecturerFemaleInside");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalFemale",
                table: "Personnels",
                newName: "TotalLecturerFemale");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalDomesticInsideProfessor",
                table: "Personnels",
                newName: "TotalLecturerDomesticInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalDomesticInsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalLecturerDomesticInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalDomesticInside",
                table: "Personnels",
                newName: "TotalLecturerDomesticInside");

            migrationBuilder.RenameColumn(
                name: "LecturerTotalDomestic",
                table: "Personnels",
                newName: "TotalLecturerDomestic");

            migrationBuilder.RenameColumn(
                name: "LecturerTotal",
                table: "Personnels",
                newName: "TotalLecturer");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalSoftwareOutside",
                table: "Personnels",
                newName: "TotalEmployeeSoftwareOutside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalSoftwareInside",
                table: "Personnels",
                newName: "TotalEmployeeSoftwareInside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalSoftware",
                table: "Personnels",
                newName: "TotalEmployeeSoftware");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalPartTimeOutside",
                table: "Personnels",
                newName: "TotalEmployeePartTimeOutside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalPartTimeInside",
                table: "Personnels",
                newName: "TotalEmployeePartTimeInside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalPartTime",
                table: "Personnels",
                newName: "TotalEmployeePartTime");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideInternationalUniversity",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalUniversity");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideInternationalProfessor",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalProfessor");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideInternationalOrther",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalOrther");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideInternationalMaster",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalMaster");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideInternationalIntermediate",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalIntermediate");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideInternationalDoctor",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalDoctor");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideInternationalCollege",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalCollege");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideInternationalAssociateProfessor",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideInternational",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternational");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideDomesticUniversity",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticUniversity");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideDomesticProfessor",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticProfessor");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideDomesticOrther",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticOrther");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideDomesticMaster",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticMaster");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideDomesticIntermediate",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticIntermediate");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideDomesticDoctor",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticDoctor");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideDomesticCollege",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticCollege");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideDomesticAssociateProfessor",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutsideDomestic",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomestic");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOutSide",
                table: "Personnels",
                newName: "TotalEmployeeOutSide");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOtherOutside",
                table: "Personnels",
                newName: "TotalEmployeeOtherOutside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOtherInside",
                table: "Personnels",
                newName: "TotalEmployeeOtherInside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOther",
                table: "Personnels",
                newName: "TotalEmployeeOther");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOfficalOutside",
                table: "Personnels",
                newName: "TotalEmployeeOfficalOutside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOfficalInside",
                table: "Personnels",
                newName: "TotalEmployeeOfficalInside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalOffical",
                table: "Personnels",
                newName: "TotalEmployeeOffical");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalMaleOutside",
                table: "Personnels",
                newName: "TotalEmployeeMaleOutside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalMaleInside",
                table: "Personnels",
                newName: "TotalEmployeeMaleInside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalMale",
                table: "Personnels",
                newName: "TotalEmployeeMale");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInternationalInsideProfessor",
                table: "Personnels",
                newName: "TotalEmployeeInternationalInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInternationalInsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalEmployeeInternationalInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInternationalInside",
                table: "Personnels",
                newName: "TotalEmployeeInternationalInside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInternational",
                table: "Personnels",
                newName: "TotalEmployeeInternational");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInsideInternationalUniversity",
                table: "Personnels",
                newName: "TotalEmployeeInsideInternationalUniversity");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInsideInternationalOrther",
                table: "Personnels",
                newName: "TotalEmployeeInsideInternationalOrther");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInsideInternationalMaster",
                table: "Personnels",
                newName: "TotalEmployeeInsideInternationalMaster");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInsideInternationalIntermediate",
                table: "Personnels",
                newName: "TotalEmployeeInsideInternationalIntermediate");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInsideInternationalDoctor",
                table: "Personnels",
                newName: "TotalEmployeeInsideInternationalDoctor");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInsideInternationalCollege",
                table: "Personnels",
                newName: "TotalEmployeeInsideInternationalCollege");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInsideDomesticUniversity",
                table: "Personnels",
                newName: "TotalEmployeeInsideDomesticUniversity");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInsideDomesticOrther",
                table: "Personnels",
                newName: "TotalEmployeeInsideDomesticOrther");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInsideDomesticMaster",
                table: "Personnels",
                newName: "TotalEmployeeInsideDomesticMaster");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInsideDomesticIntermediate",
                table: "Personnels",
                newName: "TotalEmployeeInsideDomesticIntermediate");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInsideDomesticDoctor",
                table: "Personnels",
                newName: "TotalEmployeeInsideDomesticDoctor");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInsideDomesticCollege",
                table: "Personnels",
                newName: "TotalEmployeeInsideDomesticCollege");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalInSide",
                table: "Personnels",
                newName: "TotalEmployeeInSide");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalFemaleOutside",
                table: "Personnels",
                newName: "TotalEmployeeFemaleOutside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalFemaleInside",
                table: "Personnels",
                newName: "TotalEmployeeFemaleInside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalFemale",
                table: "Personnels",
                newName: "TotalEmployeeFemale");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalDomesticInsideProfessor",
                table: "Personnels",
                newName: "TotalEmployeeDomesticInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalDomesticInsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalEmployeeDomesticInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalDomesticInside",
                table: "Personnels",
                newName: "TotalEmployeeDomesticInside");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotalDomestic",
                table: "Personnels",
                newName: "TotalEmployeeDomestic");

            migrationBuilder.RenameColumn(
                name: "EmployeeTotal",
                table: "Personnels",
                newName: "TotalEmployee");

            migrationBuilder.RenameColumn(
                name: "AlumnusTotalSofware",
                table: "Personnels",
                newName: "TotalAlumnusSofware");

            migrationBuilder.RenameColumn(
                name: "AlumnusTotalOutsideSofware",
                table: "Personnels",
                newName: "TotalAlumnusOutsideSofware");

            migrationBuilder.RenameColumn(
                name: "AlumnusTotalOutsideOther",
                table: "Personnels",
                newName: "TotalAlumnusOutsideOther");

            migrationBuilder.RenameColumn(
                name: "AlumnusTotalOutside",
                table: "Personnels",
                newName: "TotalAlumnusOutside");

            migrationBuilder.RenameColumn(
                name: "AlumnusTotalOther",
                table: "Personnels",
                newName: "TotalAlumnusOther");

            migrationBuilder.RenameColumn(
                name: "AlumnusTotalInsideSofware",
                table: "Personnels",
                newName: "TotalAlumnusInsideSofware");

            migrationBuilder.RenameColumn(
                name: "AlumnusTotalInsideOther",
                table: "Personnels",
                newName: "TotalAlumnusInsideOther");

            migrationBuilder.RenameColumn(
                name: "AlumnusTotalInside",
                table: "Personnels",
                newName: "TotalAlumnusInside");

            migrationBuilder.RenameColumn(
                name: "AlumnusTotal",
                table: "Personnels",
                newName: "TotalAlumnus");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 12, 5, 15, 11, 20, 662, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 12, 5, 14, 19, 39, 958, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalStudentOutside",
                table: "Personnels",
                newName: "StudentTotalOutside");

            migrationBuilder.RenameColumn(
                name: "TotalStudentInside",
                table: "Personnels",
                newName: "StudentTotalInside");

            migrationBuilder.RenameColumn(
                name: "TotalStudent",
                table: "Personnels",
                newName: "StudentTotal");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerPartTimeOutside",
                table: "Personnels",
                newName: "LecturerTotalPartTimeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerPartTimeInside",
                table: "Personnels",
                newName: "LecturerTotalPartTimeInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerPartTime",
                table: "Personnels",
                newName: "LecturerTotalPartTime");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalUniversity",
                table: "Personnels",
                newName: "LecturerTotalOutsideInternationalUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalProfessor",
                table: "Personnels",
                newName: "LecturerTotalOutsideInternationalProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalOrther",
                table: "Personnels",
                newName: "LecturerTotalOutsideInternationalOrther");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalMaster",
                table: "Personnels",
                newName: "LecturerTotalOutsideInternationalMaster");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalIntermediate",
                table: "Personnels",
                newName: "LecturerTotalOutsideInternationalIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalDoctor",
                table: "Personnels",
                newName: "LecturerTotalOutsideInternationalDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalCollege",
                table: "Personnels",
                newName: "LecturerTotalOutsideInternationalCollege");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalAssociateProfessor",
                table: "Personnels",
                newName: "LecturerTotalOutsideInternationalAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternational",
                table: "Personnels",
                newName: "LecturerTotalOutsideInternational");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticUniversity",
                table: "Personnels",
                newName: "LecturerTotalOutsideDomesticUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticProfessor",
                table: "Personnels",
                newName: "LecturerTotalOutsideDomesticProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticOrther",
                table: "Personnels",
                newName: "LecturerTotalOutsideDomesticOrther");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticMaster",
                table: "Personnels",
                newName: "LecturerTotalOutsideDomesticMaster");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticIntermediate",
                table: "Personnels",
                newName: "LecturerTotalOutsideDomesticIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticDoctor",
                table: "Personnels",
                newName: "LecturerTotalOutsideDomesticDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticCollege",
                table: "Personnels",
                newName: "LecturerTotalOutsideDomesticCollege");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticAssociateProfessor",
                table: "Personnels",
                newName: "LecturerTotalOutsideDomesticAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomestic",
                table: "Personnels",
                newName: "LecturerTotalOutsideDomestic");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutSide",
                table: "Personnels",
                newName: "LecturerTotalOutSide");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOfficalOutside",
                table: "Personnels",
                newName: "LecturerTotalOfficalOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOfficalInside",
                table: "Personnels",
                newName: "LecturerTotalOfficalInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOffical",
                table: "Personnels",
                newName: "LecturerTotalOffical");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerMaleOutside",
                table: "Personnels",
                newName: "LecturerTotalMaleOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerMaleInside",
                table: "Personnels",
                newName: "LecturerTotalMaleInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerMale",
                table: "Personnels",
                newName: "LecturerTotalMale");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInternationalInsideProfessor",
                table: "Personnels",
                newName: "LecturerTotalInternationalInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInternationalInsideAssociateProfessor",
                table: "Personnels",
                newName: "LecturerTotalInternationalInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInternationalInside",
                table: "Personnels",
                newName: "LecturerTotalInternationalInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInternational",
                table: "Personnels",
                newName: "LecturerTotalInternational");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideInternationalUniversity",
                table: "Personnels",
                newName: "LecturerTotalInsideInternationalUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideInternationalOrther",
                table: "Personnels",
                newName: "LecturerTotalInsideInternationalOrther");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideInternationalMaster",
                table: "Personnels",
                newName: "LecturerTotalInsideInternationalMaster");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideInternationalIntermediate",
                table: "Personnels",
                newName: "LecturerTotalInsideInternationalIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideInternationalDoctor",
                table: "Personnels",
                newName: "LecturerTotalInsideInternationalDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideInternationalCollege",
                table: "Personnels",
                newName: "LecturerTotalInsideInternationalCollege");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideDomesticUniversity",
                table: "Personnels",
                newName: "LecturerTotalInsideDomesticUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideDomesticOrther",
                table: "Personnels",
                newName: "LecturerTotalInsideDomesticOrther");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideDomesticMaster",
                table: "Personnels",
                newName: "LecturerTotalInsideDomesticMaster");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideDomesticIntermediate",
                table: "Personnels",
                newName: "LecturerTotalInsideDomesticIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideDomesticDoctor",
                table: "Personnels",
                newName: "LecturerTotalInsideDomesticDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideDomesticCollege",
                table: "Personnels",
                newName: "LecturerTotalInsideDomesticCollege");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInSide",
                table: "Personnels",
                newName: "LecturerTotalInSide");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerFemaleOutside",
                table: "Personnels",
                newName: "LecturerTotalFemaleOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerFemaleInside",
                table: "Personnels",
                newName: "LecturerTotalFemaleInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerFemale",
                table: "Personnels",
                newName: "LecturerTotalFemale");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerDomesticInsideProfessor",
                table: "Personnels",
                newName: "LecturerTotalDomesticInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerDomesticInsideAssociateProfessor",
                table: "Personnels",
                newName: "LecturerTotalDomesticInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerDomesticInside",
                table: "Personnels",
                newName: "LecturerTotalDomesticInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerDomestic",
                table: "Personnels",
                newName: "LecturerTotalDomestic");

            migrationBuilder.RenameColumn(
                name: "TotalLecturer",
                table: "Personnels",
                newName: "LecturerTotal");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeSoftwareOutside",
                table: "Personnels",
                newName: "EmployeeTotalSoftwareOutside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeSoftwareInside",
                table: "Personnels",
                newName: "EmployeeTotalSoftwareInside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeSoftware",
                table: "Personnels",
                newName: "EmployeeTotalSoftware");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeePartTimeOutside",
                table: "Personnels",
                newName: "EmployeeTotalPartTimeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeePartTimeInside",
                table: "Personnels",
                newName: "EmployeeTotalPartTimeInside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeePartTime",
                table: "Personnels",
                newName: "EmployeeTotalPartTime");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalUniversity",
                table: "Personnels",
                newName: "EmployeeTotalOutsideInternationalUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalProfessor",
                table: "Personnels",
                newName: "EmployeeTotalOutsideInternationalProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalOrther",
                table: "Personnels",
                newName: "EmployeeTotalOutsideInternationalOrther");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalMaster",
                table: "Personnels",
                newName: "EmployeeTotalOutsideInternationalMaster");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalIntermediate",
                table: "Personnels",
                newName: "EmployeeTotalOutsideInternationalIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalDoctor",
                table: "Personnels",
                newName: "EmployeeTotalOutsideInternationalDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalCollege",
                table: "Personnels",
                newName: "EmployeeTotalOutsideInternationalCollege");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalAssociateProfessor",
                table: "Personnels",
                newName: "EmployeeTotalOutsideInternationalAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternational",
                table: "Personnels",
                newName: "EmployeeTotalOutsideInternational");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticUniversity",
                table: "Personnels",
                newName: "EmployeeTotalOutsideDomesticUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticProfessor",
                table: "Personnels",
                newName: "EmployeeTotalOutsideDomesticProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticOrther",
                table: "Personnels",
                newName: "EmployeeTotalOutsideDomesticOrther");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticMaster",
                table: "Personnels",
                newName: "EmployeeTotalOutsideDomesticMaster");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticIntermediate",
                table: "Personnels",
                newName: "EmployeeTotalOutsideDomesticIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticDoctor",
                table: "Personnels",
                newName: "EmployeeTotalOutsideDomesticDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticCollege",
                table: "Personnels",
                newName: "EmployeeTotalOutsideDomesticCollege");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticAssociateProfessor",
                table: "Personnels",
                newName: "EmployeeTotalOutsideDomesticAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomestic",
                table: "Personnels",
                newName: "EmployeeTotalOutsideDomestic");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutSide",
                table: "Personnels",
                newName: "EmployeeTotalOutSide");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOtherOutside",
                table: "Personnels",
                newName: "EmployeeTotalOtherOutside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOtherInside",
                table: "Personnels",
                newName: "EmployeeTotalOtherInside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOther",
                table: "Personnels",
                newName: "EmployeeTotalOther");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOfficalOutside",
                table: "Personnels",
                newName: "EmployeeTotalOfficalOutside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOfficalInside",
                table: "Personnels",
                newName: "EmployeeTotalOfficalInside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOffical",
                table: "Personnels",
                newName: "EmployeeTotalOffical");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeMaleOutside",
                table: "Personnels",
                newName: "EmployeeTotalMaleOutside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeMaleInside",
                table: "Personnels",
                newName: "EmployeeTotalMaleInside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeMale",
                table: "Personnels",
                newName: "EmployeeTotalMale");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInternationalInsideProfessor",
                table: "Personnels",
                newName: "EmployeeTotalInternationalInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInternationalInsideAssociateProfessor",
                table: "Personnels",
                newName: "EmployeeTotalInternationalInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInternationalInside",
                table: "Personnels",
                newName: "EmployeeTotalInternationalInside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInternational",
                table: "Personnels",
                newName: "EmployeeTotalInternational");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideInternationalUniversity",
                table: "Personnels",
                newName: "EmployeeTotalInsideInternationalUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideInternationalOrther",
                table: "Personnels",
                newName: "EmployeeTotalInsideInternationalOrther");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideInternationalMaster",
                table: "Personnels",
                newName: "EmployeeTotalInsideInternationalMaster");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideInternationalIntermediate",
                table: "Personnels",
                newName: "EmployeeTotalInsideInternationalIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideInternationalDoctor",
                table: "Personnels",
                newName: "EmployeeTotalInsideInternationalDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideInternationalCollege",
                table: "Personnels",
                newName: "EmployeeTotalInsideInternationalCollege");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideDomesticUniversity",
                table: "Personnels",
                newName: "EmployeeTotalInsideDomesticUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideDomesticOrther",
                table: "Personnels",
                newName: "EmployeeTotalInsideDomesticOrther");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideDomesticMaster",
                table: "Personnels",
                newName: "EmployeeTotalInsideDomesticMaster");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideDomesticIntermediate",
                table: "Personnels",
                newName: "EmployeeTotalInsideDomesticIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideDomesticDoctor",
                table: "Personnels",
                newName: "EmployeeTotalInsideDomesticDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideDomesticCollege",
                table: "Personnels",
                newName: "EmployeeTotalInsideDomesticCollege");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInSide",
                table: "Personnels",
                newName: "EmployeeTotalInSide");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeFemaleOutside",
                table: "Personnels",
                newName: "EmployeeTotalFemaleOutside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeFemaleInside",
                table: "Personnels",
                newName: "EmployeeTotalFemaleInside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeFemale",
                table: "Personnels",
                newName: "EmployeeTotalFemale");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeDomesticInsideProfessor",
                table: "Personnels",
                newName: "EmployeeTotalDomesticInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeDomesticInsideAssociateProfessor",
                table: "Personnels",
                newName: "EmployeeTotalDomesticInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeDomesticInside",
                table: "Personnels",
                newName: "EmployeeTotalDomesticInside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeDomestic",
                table: "Personnels",
                newName: "EmployeeTotalDomestic");

            migrationBuilder.RenameColumn(
                name: "TotalEmployee",
                table: "Personnels",
                newName: "EmployeeTotal");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusSofware",
                table: "Personnels",
                newName: "AlumnusTotalSofware");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusOutsideSofware",
                table: "Personnels",
                newName: "AlumnusTotalOutsideSofware");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusOutsideOther",
                table: "Personnels",
                newName: "AlumnusTotalOutsideOther");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusOutside",
                table: "Personnels",
                newName: "AlumnusTotalOutside");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusOther",
                table: "Personnels",
                newName: "AlumnusTotalOther");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusInsideSofware",
                table: "Personnels",
                newName: "AlumnusTotalInsideSofware");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusInsideOther",
                table: "Personnels",
                newName: "AlumnusTotalInsideOther");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusInside",
                table: "Personnels",
                newName: "AlumnusTotalInside");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnus",
                table: "Personnels",
                newName: "AlumnusTotal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 12, 5, 14, 19, 39, 958, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 12, 5, 15, 11, 20, 662, DateTimeKind.Local));
        }
    }
}
