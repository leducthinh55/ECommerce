using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class ModifyPersonelhuy051220192 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalLecturerPartTimeOutside",
                table: "Personnels",
                newName: "TotalSoftwareEmployeeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerPartTimeInside",
                table: "Personnels",
                newName: "TotalSoftwareEmployeeInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerPartTime",
                table: "Personnels",
                newName: "TotalSoftwareEmployee");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalUniversity",
                table: "Personnels",
                newName: "TotalSoftwareAlumnusOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalProfessor",
                table: "Personnels",
                newName: "TotalSoftwareAlumnusInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalOrther",
                table: "Personnels",
                newName: "TotalSoftwareAlumnus");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalMaster",
                table: "Personnels",
                newName: "TotalPartTimeLecturersOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalIntermediate",
                table: "Personnels",
                newName: "TotalPartTimeLecturersInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalDoctor",
                table: "Personnels",
                newName: "TotalPartTimeLecturers");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalCollege",
                table: "Personnels",
                newName: "TotalPartTimeEmployeeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternationalAssociateProfessor",
                table: "Personnels",
                newName: "TotalPartTimeEmployeeInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideInternational",
                table: "Personnels",
                newName: "TotalPartTimeEmployee");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticUniversity",
                table: "Personnels",
                newName: "TotalOtherEmployeeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticProfessor",
                table: "Personnels",
                newName: "TotalOtherEmployeeInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticOrther",
                table: "Personnels",
                newName: "TotalOtherEmployee");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticMaster",
                table: "Personnels",
                newName: "TotalOtherAlumnusOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticIntermediate",
                table: "Personnels",
                newName: "TotalOtherAlumnusInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticDoctor",
                table: "Personnels",
                newName: "TotalOtherAlumnus");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticCollege",
                table: "Personnels",
                newName: "TotalOfficialLecturersOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomesticAssociateProfessor",
                table: "Personnels",
                newName: "TotalOfficialLecturersInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutsideDomestic",
                table: "Personnels",
                newName: "TotalOfficialLecturers");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOutSide",
                table: "Personnels",
                newName: "TotalOfficialEmployeeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOfficalOutside",
                table: "Personnels",
                newName: "TotalOfficialEmployeeInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOfficalInside",
                table: "Personnels",
                newName: "TotalOfficialEmployee");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerOffical",
                table: "Personnels",
                newName: "TotalMaleLecturersOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerMaleOutside",
                table: "Personnels",
                newName: "TotalMaleLecturersInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerMaleInside",
                table: "Personnels",
                newName: "TotalMaleLecturers");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerMale",
                table: "Personnels",
                newName: "TotalMaleEmployeeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInternationalInsideProfessor",
                table: "Personnels",
                newName: "TotalMaleEmployeeInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInternationalInsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalMaleEmployee");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInternationalInside",
                table: "Personnels",
                newName: "TotalLecturersOutSide");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInternational",
                table: "Personnels",
                newName: "TotalLecturersInSide");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideInternationalUniversity",
                table: "Personnels",
                newName: "TotalLecturers");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideInternationalOrther",
                table: "Personnels",
                newName: "TotalInternationalLecturersOutsideUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideInternationalMaster",
                table: "Personnels",
                newName: "TotalInternationalLecturersOutsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideInternationalIntermediate",
                table: "Personnels",
                newName: "TotalInternationalLecturersOutsideOrther");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideInternationalDoctor",
                table: "Personnels",
                newName: "TotalInternationalLecturersOutsideMaster");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideInternationalCollege",
                table: "Personnels",
                newName: "TotalInternationalLecturersOutsideIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideDomesticUniversity",
                table: "Personnels",
                newName: "TotalInternationalLecturersOutsideDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideDomesticOrther",
                table: "Personnels",
                newName: "TotalInternationalLecturersOutsideCollege");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideDomesticMaster",
                table: "Personnels",
                newName: "TotalInternationalLecturersOutsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideDomesticIntermediate",
                table: "Personnels",
                newName: "TotalInternationalLecturersOutside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideDomesticDoctor",
                table: "Personnels",
                newName: "TotalInternationalLecturersInsideUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInsideDomesticCollege",
                table: "Personnels",
                newName: "TotalInternationalLecturersInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerInSide",
                table: "Personnels",
                newName: "TotalInternationalLecturersInsideOrther");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerFemaleOutside",
                table: "Personnels",
                newName: "TotalInternationalLecturersInsideMaster");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerFemaleInside",
                table: "Personnels",
                newName: "TotalInternationalLecturersInsideIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerFemale",
                table: "Personnels",
                newName: "TotalInternationalLecturersInsideDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerDomesticInsideProfessor",
                table: "Personnels",
                newName: "TotalInternationalLecturersInsideCollege");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerDomesticInsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalInternationalLecturersInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerDomesticInside",
                table: "Personnels",
                newName: "TotalInternationalLecturersInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturerDomestic",
                table: "Personnels",
                newName: "TotalInternationalLecturers");

            migrationBuilder.RenameColumn(
                name: "TotalLecturer",
                table: "Personnels",
                newName: "TotalInternationalEmployeeOutsideUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeSoftwareOutside",
                table: "Personnels",
                newName: "TotalInternationalEmployeeOutsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeSoftwareInside",
                table: "Personnels",
                newName: "TotalInternationalEmployeeOutsideOrther");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeSoftware",
                table: "Personnels",
                newName: "TotalInternationalEmployeeOutsideMaster");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeePartTimeOutside",
                table: "Personnels",
                newName: "TotalInternationalEmployeeOutsideIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeePartTimeInside",
                table: "Personnels",
                newName: "TotalInternationalEmployeeOutsideDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeePartTime",
                table: "Personnels",
                newName: "TotalInternationalEmployeeOutsideCollege");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalUniversity",
                table: "Personnels",
                newName: "TotalInternationalEmployeeOutsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalProfessor",
                table: "Personnels",
                newName: "TotalInternationalEmployeeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalOrther",
                table: "Personnels",
                newName: "TotalInternationalEmployeeInsideUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalMaster",
                table: "Personnels",
                newName: "TotalInternationalEmployeeInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalIntermediate",
                table: "Personnels",
                newName: "TotalInternationalEmployeeInsideOrther");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalDoctor",
                table: "Personnels",
                newName: "TotalInternationalEmployeeInsideMaster");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalCollege",
                table: "Personnels",
                newName: "TotalInternationalEmployeeInsideIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternationalAssociateProfessor",
                table: "Personnels",
                newName: "TotalInternationalEmployeeInsideDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideInternational",
                table: "Personnels",
                newName: "TotalInternationalEmployeeInsideCollege");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticUniversity",
                table: "Personnels",
                newName: "TotalInternationalEmployeeInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticProfessor",
                table: "Personnels",
                newName: "TotalInternationalEmployeeInside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticOrther",
                table: "Personnels",
                newName: "TotalInternationalEmployee");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticMaster",
                table: "Personnels",
                newName: "TotalFemaleLecturersOutside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticIntermediate",
                table: "Personnels",
                newName: "TotalFemaleLecturersInside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticDoctor",
                table: "Personnels",
                newName: "TotalFemaleLecturers");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticCollege",
                table: "Personnels",
                newName: "TotalFemaleEmployeeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomesticAssociateProfessor",
                table: "Personnels",
                newName: "TotalFemaleEmployeeInside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOutsideDomestic",
                table: "Personnels",
                newName: "TotalFemaleEmployee");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOtherOutside",
                table: "Personnels",
                newName: "TotalDomesticLecturersOutsideUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOtherInside",
                table: "Personnels",
                newName: "TotalDomesticLecturersOutsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOther",
                table: "Personnels",
                newName: "TotalDomesticLecturersOutsideOrther");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOfficalOutside",
                table: "Personnels",
                newName: "TotalDomesticLecturersOutsideMaster");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOfficalInside",
                table: "Personnels",
                newName: "TotalDomesticLecturersOutsideIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeOffical",
                table: "Personnels",
                newName: "TotalDomesticLecturersOutsideDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeMaleOutside",
                table: "Personnels",
                newName: "TotalDomesticLecturersOutsideCollege");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeMaleInside",
                table: "Personnels",
                newName: "TotalDomesticLecturersOutsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeMale",
                table: "Personnels",
                newName: "TotalDomesticLecturersOutside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInternationalInsideProfessor",
                table: "Personnels",
                newName: "TotalDomesticLecturersInsideUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInternationalInsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalDomesticLecturersInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInternationalInside",
                table: "Personnels",
                newName: "TotalDomesticLecturersInsideOrther");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInternational",
                table: "Personnels",
                newName: "TotalDomesticLecturersInsideMaster");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideInternationalUniversity",
                table: "Personnels",
                newName: "TotalDomesticLecturersInsideIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideInternationalOrther",
                table: "Personnels",
                newName: "TotalDomesticLecturersInsideDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideInternationalMaster",
                table: "Personnels",
                newName: "TotalDomesticLecturersInsideCollege");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideInternationalIntermediate",
                table: "Personnels",
                newName: "TotalDomesticLecturersInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideInternationalDoctor",
                table: "Personnels",
                newName: "TotalDomesticLecturersInside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideInternationalCollege",
                table: "Personnels",
                newName: "TotalDomesticLecturers");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideDomesticUniversity",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsidecUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideDomesticOrther",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsidecOrther");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideDomesticMaster",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsidecMaster");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideDomesticIntermediate",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsidecIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideDomesticDoctor",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsidecDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeInsideDomesticCollege",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsidecCollege");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeFemaleOutside",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeFemaleInside",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeFemale",
                table: "Personnels",
                newName: "TotalDomesticEmployeeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeDomesticInsideProfessor",
                table: "Personnels",
                newName: "TotalDomesticEmployeeInsideUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeDomesticInsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalDomesticEmployeeInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeDomesticInside",
                table: "Personnels",
                newName: "TotalDomesticEmployeeInsideOrther");

            migrationBuilder.RenameColumn(
                name: "TotalEmployeeDomestic",
                table: "Personnels",
                newName: "TotalDomesticEmployeeInsideMaster");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusSofware",
                table: "Personnels",
                newName: "TotalDomesticEmployeeInsideIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusOutsideSofware",
                table: "Personnels",
                newName: "TotalDomesticEmployeeInsideDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusOutsideOther",
                table: "Personnels",
                newName: "TotalDomesticEmployeeInsideCollege");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusOther",
                table: "Personnels",
                newName: "TotalDomesticEmployeeInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusInsideSofware",
                table: "Personnels",
                newName: "TotalDomesticEmployeeInside");

            migrationBuilder.RenameColumn(
                name: "TotalAlumnusInsideOther",
                table: "Personnels",
                newName: "TotalDomesticEmployee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 12, 5, 15, 48, 41, 205, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 12, 5, 15, 11, 20, 662, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalSoftwareEmployeeOutside",
                table: "Personnels",
                newName: "TotalLecturerPartTimeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalSoftwareEmployeeInside",
                table: "Personnels",
                newName: "TotalLecturerPartTimeInside");

            migrationBuilder.RenameColumn(
                name: "TotalSoftwareEmployee",
                table: "Personnels",
                newName: "TotalLecturerPartTime");

            migrationBuilder.RenameColumn(
                name: "TotalSoftwareAlumnusOutside",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalSoftwareAlumnusInside",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalSoftwareAlumnus",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalOrther");

            migrationBuilder.RenameColumn(
                name: "TotalPartTimeLecturersOutside",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalMaster");

            migrationBuilder.RenameColumn(
                name: "TotalPartTimeLecturersInside",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalPartTimeLecturers",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalPartTimeEmployeeOutside",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalCollege");

            migrationBuilder.RenameColumn(
                name: "TotalPartTimeEmployeeInside",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternationalAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalPartTimeEmployee",
                table: "Personnels",
                newName: "TotalLecturerOutsideInternational");

            migrationBuilder.RenameColumn(
                name: "TotalOtherEmployeeOutside",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalOtherEmployeeInside",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalOtherEmployee",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticOrther");

            migrationBuilder.RenameColumn(
                name: "TotalOtherAlumnusOutside",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticMaster");

            migrationBuilder.RenameColumn(
                name: "TotalOtherAlumnusInside",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalOtherAlumnus",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalOfficialLecturersOutside",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticCollege");

            migrationBuilder.RenameColumn(
                name: "TotalOfficialLecturersInside",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomesticAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalOfficialLecturers",
                table: "Personnels",
                newName: "TotalLecturerOutsideDomestic");

            migrationBuilder.RenameColumn(
                name: "TotalOfficialEmployeeOutside",
                table: "Personnels",
                newName: "TotalLecturerOutSide");

            migrationBuilder.RenameColumn(
                name: "TotalOfficialEmployeeInside",
                table: "Personnels",
                newName: "TotalLecturerOfficalOutside");

            migrationBuilder.RenameColumn(
                name: "TotalOfficialEmployee",
                table: "Personnels",
                newName: "TotalLecturerOfficalInside");

            migrationBuilder.RenameColumn(
                name: "TotalMaleLecturersOutside",
                table: "Personnels",
                newName: "TotalLecturerOffical");

            migrationBuilder.RenameColumn(
                name: "TotalMaleLecturersInside",
                table: "Personnels",
                newName: "TotalLecturerMaleOutside");

            migrationBuilder.RenameColumn(
                name: "TotalMaleLecturers",
                table: "Personnels",
                newName: "TotalLecturerMaleInside");

            migrationBuilder.RenameColumn(
                name: "TotalMaleEmployeeOutside",
                table: "Personnels",
                newName: "TotalLecturerMale");

            migrationBuilder.RenameColumn(
                name: "TotalMaleEmployeeInside",
                table: "Personnels",
                newName: "TotalLecturerInternationalInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalMaleEmployee",
                table: "Personnels",
                newName: "TotalLecturerInternationalInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalLecturersOutSide",
                table: "Personnels",
                newName: "TotalLecturerInternationalInside");

            migrationBuilder.RenameColumn(
                name: "TotalLecturersInSide",
                table: "Personnels",
                newName: "TotalLecturerInternational");

            migrationBuilder.RenameColumn(
                name: "TotalLecturers",
                table: "Personnels",
                newName: "TotalLecturerInsideInternationalUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersOutsideUniversity",
                table: "Personnels",
                newName: "TotalLecturerInsideInternationalOrther");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersOutsideProfessor",
                table: "Personnels",
                newName: "TotalLecturerInsideInternationalMaster");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersOutsideOrther",
                table: "Personnels",
                newName: "TotalLecturerInsideInternationalIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersOutsideMaster",
                table: "Personnels",
                newName: "TotalLecturerInsideInternationalDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersOutsideIntermediate",
                table: "Personnels",
                newName: "TotalLecturerInsideInternationalCollege");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersOutsideDoctor",
                table: "Personnels",
                newName: "TotalLecturerInsideDomesticUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersOutsideCollege",
                table: "Personnels",
                newName: "TotalLecturerInsideDomesticOrther");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersOutsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalLecturerInsideDomesticMaster");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersOutside",
                table: "Personnels",
                newName: "TotalLecturerInsideDomesticIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersInsideUniversity",
                table: "Personnels",
                newName: "TotalLecturerInsideDomesticDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersInsideProfessor",
                table: "Personnels",
                newName: "TotalLecturerInsideDomesticCollege");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersInsideOrther",
                table: "Personnels",
                newName: "TotalLecturerInSide");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersInsideMaster",
                table: "Personnels",
                newName: "TotalLecturerFemaleOutside");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersInsideIntermediate",
                table: "Personnels",
                newName: "TotalLecturerFemaleInside");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersInsideDoctor",
                table: "Personnels",
                newName: "TotalLecturerFemale");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersInsideCollege",
                table: "Personnels",
                newName: "TotalLecturerDomesticInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersInsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalLecturerDomesticInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturersInside",
                table: "Personnels",
                newName: "TotalLecturerDomesticInside");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalLecturers",
                table: "Personnels",
                newName: "TotalLecturerDomestic");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeOutsideUniversity",
                table: "Personnels",
                newName: "TotalLecturer");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeOutsideProfessor",
                table: "Personnels",
                newName: "TotalEmployeeSoftwareOutside");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeOutsideOrther",
                table: "Personnels",
                newName: "TotalEmployeeSoftwareInside");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeOutsideMaster",
                table: "Personnels",
                newName: "TotalEmployeeSoftware");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeOutsideIntermediate",
                table: "Personnels",
                newName: "TotalEmployeePartTimeOutside");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeOutsideDoctor",
                table: "Personnels",
                newName: "TotalEmployeePartTimeInside");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeOutsideCollege",
                table: "Personnels",
                newName: "TotalEmployeePartTime");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeOutsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeOutside",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeInsideUniversity",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalOrther");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeInsideProfessor",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalMaster");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeInsideOrther",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeInsideMaster",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeInsideIntermediate",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalCollege");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeInsideDoctor",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternationalAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeInsideCollege",
                table: "Personnels",
                newName: "TotalEmployeeOutsideInternational");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeInsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployeeInside",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalInternationalEmployee",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticOrther");

            migrationBuilder.RenameColumn(
                name: "TotalFemaleLecturersOutside",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticMaster");

            migrationBuilder.RenameColumn(
                name: "TotalFemaleLecturersInside",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalFemaleLecturers",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalFemaleEmployeeOutside",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticCollege");

            migrationBuilder.RenameColumn(
                name: "TotalFemaleEmployeeInside",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomesticAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalFemaleEmployee",
                table: "Personnels",
                newName: "TotalEmployeeOutsideDomestic");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersOutsideUniversity",
                table: "Personnels",
                newName: "TotalEmployeeOtherOutside");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersOutsideProfessor",
                table: "Personnels",
                newName: "TotalEmployeeOtherInside");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersOutsideOrther",
                table: "Personnels",
                newName: "TotalEmployeeOther");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersOutsideMaster",
                table: "Personnels",
                newName: "TotalEmployeeOfficalOutside");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersOutsideIntermediate",
                table: "Personnels",
                newName: "TotalEmployeeOfficalInside");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersOutsideDoctor",
                table: "Personnels",
                newName: "TotalEmployeeOffical");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersOutsideCollege",
                table: "Personnels",
                newName: "TotalEmployeeMaleOutside");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersOutsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalEmployeeMaleInside");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersOutside",
                table: "Personnels",
                newName: "TotalEmployeeMale");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersInsideUniversity",
                table: "Personnels",
                newName: "TotalEmployeeInternationalInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersInsideProfessor",
                table: "Personnels",
                newName: "TotalEmployeeInternationalInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersInsideOrther",
                table: "Personnels",
                newName: "TotalEmployeeInternationalInside");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersInsideMaster",
                table: "Personnels",
                newName: "TotalEmployeeInternational");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersInsideIntermediate",
                table: "Personnels",
                newName: "TotalEmployeeInsideInternationalUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersInsideDoctor",
                table: "Personnels",
                newName: "TotalEmployeeInsideInternationalOrther");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersInsideCollege",
                table: "Personnels",
                newName: "TotalEmployeeInsideInternationalMaster");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersInsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalEmployeeInsideInternationalIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturersInside",
                table: "Personnels",
                newName: "TotalEmployeeInsideInternationalDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticLecturers",
                table: "Personnels",
                newName: "TotalEmployeeInsideInternationalCollege");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsidecUniversity",
                table: "Personnels",
                newName: "TotalEmployeeInsideDomesticUniversity");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsidecOrther",
                table: "Personnels",
                newName: "TotalEmployeeInsideDomesticOrther");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsidecMaster",
                table: "Personnels",
                newName: "TotalEmployeeInsideDomesticMaster");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsidecIntermediate",
                table: "Personnels",
                newName: "TotalEmployeeInsideDomesticIntermediate");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsidecDoctor",
                table: "Personnels",
                newName: "TotalEmployeeInsideDomesticDoctor");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsidecCollege",
                table: "Personnels",
                newName: "TotalEmployeeInsideDomesticCollege");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsideProfessor",
                table: "Personnels",
                newName: "TotalEmployeeFemaleOutside");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalEmployeeFemaleInside");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeOutside",
                table: "Personnels",
                newName: "TotalEmployeeFemale");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeInsideUniversity",
                table: "Personnels",
                newName: "TotalEmployeeDomesticInsideProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeInsideProfessor",
                table: "Personnels",
                newName: "TotalEmployeeDomesticInsideAssociateProfessor");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeInsideOrther",
                table: "Personnels",
                newName: "TotalEmployeeDomesticInside");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeInsideMaster",
                table: "Personnels",
                newName: "TotalEmployeeDomestic");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeInsideIntermediate",
                table: "Personnels",
                newName: "TotalAlumnusSofware");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeInsideDoctor",
                table: "Personnels",
                newName: "TotalAlumnusOutsideSofware");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeInsideCollege",
                table: "Personnels",
                newName: "TotalAlumnusOutsideOther");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeInsideAssociateProfessor",
                table: "Personnels",
                newName: "TotalAlumnusOther");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployeeInside",
                table: "Personnels",
                newName: "TotalAlumnusInsideSofware");

            migrationBuilder.RenameColumn(
                name: "TotalDomesticEmployee",
                table: "Personnels",
                newName: "TotalAlumnusInsideOther");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 12, 5, 15, 11, 20, 662, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 12, 5, 15, 48, 41, 205, DateTimeKind.Local));
        }
    }
}
