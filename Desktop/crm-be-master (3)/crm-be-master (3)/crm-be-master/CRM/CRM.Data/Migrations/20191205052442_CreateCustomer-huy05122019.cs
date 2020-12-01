using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class CreateCustomerhuy05122019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEstablish",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeputyGender",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Investment",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "StaffCount",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TotalInvestment",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "YearStarted",
                table: "Customers",
                newName: "TimeOfChangeInvestmentCertificates");

            migrationBuilder.RenameColumn(
                name: "YearEnded",
                table: "Customers",
                newName: "TimeOfChangeBusinessLicense");

            migrationBuilder.RenameColumn(
                name: "Room",
                table: "Customers",
                newName: "StartYear");

            migrationBuilder.RenameColumn(
                name: "MainCareer",
                table: "Customers",
                newName: "ObjectDetail");

            migrationBuilder.RenameColumn(
                name: "InvestmentCertificateTime",
                table: "Customers",
                newName: "NumberOfInvestmentCertificate");

            migrationBuilder.RenameColumn(
                name: "InvestmentCertificateDate",
                table: "Customers",
                newName: "SignDayActivities");

            migrationBuilder.RenameColumn(
                name: "InvestmentCertificate",
                table: "Customers",
                newName: "NumberOfBusinessLicense");

            migrationBuilder.RenameColumn(
                name: "Floor",
                table: "Customers",
                newName: "NumberOfActivities");

            migrationBuilder.RenameColumn(
                name: "DeputyTel",
                table: "Customers",
                newName: "MemberOfQuangTrungSoftware");

            migrationBuilder.RenameColumn(
                name: "DeputyPosition",
                table: "Customers",
                newName: "MarketActive");

            migrationBuilder.RenameColumn(
                name: "DeputyNation",
                table: "Customers",
                newName: "MainCarrer");

            migrationBuilder.RenameColumn(
                name: "DeputyName",
                table: "Customers",
                newName: "EndYear");

            migrationBuilder.RenameColumn(
                name: "DeputyMail",
                table: "Customers",
                newName: "Carrer");

            migrationBuilder.RenameColumn(
                name: "ContractNoDateRegister",
                table: "Customers",
                newName: "ExpirationDateActivities");

            migrationBuilder.RenameColumn(
                name: "ContractNoDateOut",
                table: "Customers",
                newName: "DateOfIssuingInvestmentCertificate");

            migrationBuilder.RenameColumn(
                name: "ContractNo",
                table: "Customers",
                newName: "BusinessType");

            migrationBuilder.RenameColumn(
                name: "Career",
                table: "Customers",
                newName: "Agency");

            migrationBuilder.RenameColumn(
                name: "BusinessLicenseTime",
                table: "Customers",
                newName: "AddressRoom");

            migrationBuilder.RenameColumn(
                name: "BusinessLicenseDate",
                table: "Customers",
                newName: "DateOfIssuingBusinessLicense");

            migrationBuilder.RenameColumn(
                name: "BusinessLicense",
                table: "Customers",
                newName: "AddressProvince");

            migrationBuilder.RenameColumn(
                name: "Building",
                table: "Customers",
                newName: "AddressFloor");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveDay",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 12, 5, 12, 24, 41, 431, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "AddressBuilding",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AmountId",
                table: "Customers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Customers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PersonnelId",
                table: "Customers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "VondautuProcess",
                table: "Customers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VondautuRegister",
                table: "Customers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VondieuleUSD",
                table: "Customers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VondieuleVND",
                table: "Customers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Amounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TotalVndRevenue = table.Column<decimal>(nullable: false),
                    TotalUsdRevenue = table.Column<decimal>(nullable: false),
                    TotalVndRevenueOutside = table.Column<decimal>(nullable: false),
                    TotalVndRevenueInside = table.Column<decimal>(nullable: false),
                    TotalUsdRevenueOutside = table.Column<decimal>(nullable: false),
                    TotalUsdRevenueInside = table.Column<decimal>(nullable: false),
                    TotalDomesticVndRevenue = table.Column<decimal>(nullable: false),
                    TotalDomesticUsdRevenue = table.Column<decimal>(nullable: false),
                    TotalDomesticVndRevenueOutside = table.Column<decimal>(nullable: false),
                    TotalDomesticVndRevenueInside = table.Column<decimal>(nullable: false),
                    TotalDomesticUsdRevenueOutside = table.Column<decimal>(nullable: false),
                    TotalDomesticUsdRevenueInside = table.Column<decimal>(nullable: false),
                    TotalExportVndRevenue = table.Column<decimal>(nullable: false),
                    TotalExportUsdRevenue = table.Column<decimal>(nullable: false),
                    TotalExportVndRevenueOutsidde = table.Column<decimal>(nullable: false),
                    TotalExportVndRevenueInsidde = table.Column<decimal>(nullable: false),
                    TotalExportUsdRevenueOutside = table.Column<decimal>(nullable: false),
                    TotalExportUsdRevenueInside = table.Column<decimal>(nullable: false),
                    RatioOfExportRevenue = table.Column<decimal>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CompanyCode = table.Column<string>(nullable: true),
                    IssuingCompanyPlace = table.Column<string>(nullable: true),
                    IssuingCompanyDate = table.Column<string>(nullable: true),
                    AddressMainTown = table.Column<string>(nullable: true),
                    LegalRepresentativePeople = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Birthday = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owners_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personnels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeTotal = table.Column<int>(nullable: false),
                    EmployeeTotalOutSide = table.Column<int>(nullable: false),
                    EmployeeTotalInSide = table.Column<int>(nullable: false),
                    EmployeeTotalMale = table.Column<int>(nullable: false),
                    EmployeeTotalMaleOutside = table.Column<int>(nullable: false),
                    EmployeeTotalMaleInside = table.Column<int>(nullable: false),
                    EmployeeTotalFemale = table.Column<int>(nullable: false),
                    EmployeeTotalFemaleOutside = table.Column<int>(nullable: false),
                    EmployeeTotalFemaleInside = table.Column<int>(nullable: false),
                    EmployeeTotalOffical = table.Column<int>(nullable: false),
                    EmployeeTotalOfficalOutside = table.Column<int>(nullable: false),
                    EmployeeTotalOfficalInside = table.Column<int>(nullable: false),
                    EmployeeTotalPartTime = table.Column<int>(nullable: false),
                    EmployeeTotalPartTimeOutside = table.Column<int>(nullable: false),
                    EmployeeTotalPartTimeInside = table.Column<int>(nullable: false),
                    EmployeeTotalSoftware = table.Column<int>(nullable: false),
                    EmployeeTotalSoftwareOutside = table.Column<int>(nullable: false),
                    EmployeeTotalSoftwareInside = table.Column<int>(nullable: false),
                    EmployeeTotalOther = table.Column<int>(nullable: false),
                    EmployeeTotalOtherOutside = table.Column<int>(nullable: false),
                    EmployeeTotalOtherInside = table.Column<int>(nullable: false),
                    EmployeeTotalInternational = table.Column<int>(nullable: false),
                    EmployeeTotalDomestic = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideDomestic = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideDomesticProfessor = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideDomesticAssociateProfessor = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideDomesticDoctor = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideDomesticMaster = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideDomesticUniversity = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideDomesticCollege = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideDomesticIntermediate = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideDomesticOrther = table.Column<int>(nullable: false),
                    EmployeeTotalDomesticInside = table.Column<int>(nullable: false),
                    EmployeeTotalDomesticInsideProfessor = table.Column<int>(nullable: false),
                    EmployeeTotalDomesticInsideAssociateProfessor = table.Column<int>(nullable: false),
                    EmployeeTotalInsideDomesticDoctor = table.Column<int>(nullable: false),
                    EmployeeTotalInsideDomesticMaster = table.Column<int>(nullable: false),
                    EmployeeTotalInsideDomesticUniversity = table.Column<int>(nullable: false),
                    EmployeeTotalInsideDomesticCollege = table.Column<int>(nullable: false),
                    EmployeeTotalInsideDomesticIntermediate = table.Column<int>(nullable: false),
                    EmployeeTotalInsideDomesticOrther = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideInternational = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideInternationalProfessor = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideInternationalAssociateProfessor = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideInternationalDoctor = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideInternationalMaster = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideInternationalUniversity = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideInternationalCollege = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideInternationalIntermediate = table.Column<int>(nullable: false),
                    EmployeeTotalOutsideInternationalOrther = table.Column<int>(nullable: false),
                    EmployeeTotalInternationalInside = table.Column<int>(nullable: false),
                    EmployeeTotalInternationalInsideProfessor = table.Column<int>(nullable: false),
                    EmployeeTotalInternationalInsideAssociateProfessor = table.Column<int>(nullable: false),
                    EmployeeTotalInsideInternationalDoctor = table.Column<int>(nullable: false),
                    EmployeeTotalInsideInternationalMaster = table.Column<int>(nullable: false),
                    EmployeeTotalInsideInternationalUniversity = table.Column<int>(nullable: false),
                    EmployeeTotalInsideInternationalCollege = table.Column<int>(nullable: false),
                    EmployeeTotalInsideInternationalIntermediate = table.Column<int>(nullable: false),
                    EmployeeTotalInsideInternationalOrther = table.Column<int>(nullable: false),
                    LecturerTotal = table.Column<int>(nullable: false),
                    LecturerTotalOutSide = table.Column<int>(nullable: false),
                    LecturerTotalInSide = table.Column<int>(nullable: false),
                    LecturerTotalMale = table.Column<int>(nullable: false),
                    LecturerTotalMaleOutside = table.Column<int>(nullable: false),
                    LecturerTotalMaleInside = table.Column<int>(nullable: false),
                    LecturerTotalFemale = table.Column<int>(nullable: false),
                    LecturerTotalFemaleOutside = table.Column<int>(nullable: false),
                    LecturerTotalFemaleInside = table.Column<int>(nullable: false),
                    LecturerTotalOffical = table.Column<int>(nullable: false),
                    LecturerTotalOfficalOutside = table.Column<int>(nullable: false),
                    LecturerTotalOfficalInside = table.Column<int>(nullable: false),
                    LecturerTotalPartTime = table.Column<int>(nullable: false),
                    LecturerTotalPartTimeOutside = table.Column<int>(nullable: false),
                    LecturerTotalPartTimeInside = table.Column<int>(nullable: false),
                    LecturerTotalInternational = table.Column<int>(nullable: false),
                    LecturerTotalDomestic = table.Column<int>(nullable: false),
                    LecturerTotalOutsideDomestic = table.Column<int>(nullable: false),
                    LecturerTotalOutsideDomesticProfessor = table.Column<int>(nullable: false),
                    LecturerTotalOutsideDomesticAssociateProfessor = table.Column<int>(nullable: false),
                    LecturerTotalOutsideDomesticDoctor = table.Column<int>(nullable: false),
                    LecturerTotalOutsideDomesticMaster = table.Column<int>(nullable: false),
                    LecturerTotalOutsideDomesticUniversity = table.Column<int>(nullable: false),
                    LecturerTotalOutsideDomesticCollege = table.Column<int>(nullable: false),
                    LecturerTotalOutsideDomesticIntermediate = table.Column<int>(nullable: false),
                    LecturerTotalOutsideDomesticOrther = table.Column<int>(nullable: false),
                    LecturerTotalDomesticInside = table.Column<int>(nullable: false),
                    LecturerTotalDomesticInsideProfessor = table.Column<int>(nullable: false),
                    LecturerTotalDomesticInsideAssociateProfessor = table.Column<int>(nullable: false),
                    LecturerTotalInsideDomesticDoctor = table.Column<int>(nullable: false),
                    LecturerTotalInsideDomesticMaster = table.Column<int>(nullable: false),
                    LecturerTotalInsideDomesticUniversity = table.Column<int>(nullable: false),
                    LecturerTotalInsideDomesticCollege = table.Column<int>(nullable: false),
                    LecturerTotalInsideDomesticIntermediate = table.Column<int>(nullable: false),
                    LecturerTotalInsideDomesticOrther = table.Column<int>(nullable: false),
                    LecturerTotalOutsideInternational = table.Column<int>(nullable: false),
                    LecturerTotalOutsideInternationalProfessor = table.Column<int>(nullable: false),
                    LecturerTotalOutsideInternationalAssociateProfessor = table.Column<int>(nullable: false),
                    LecturerTotalOutsideInternationalDoctor = table.Column<int>(nullable: false),
                    LecturerTotalOutsideInternationalMaster = table.Column<int>(nullable: false),
                    LecturerTotalOutsideInternationalUniversity = table.Column<int>(nullable: false),
                    LecturerTotalOutsideInternationalCollege = table.Column<int>(nullable: false),
                    LecturerTotalOutsideInternationalIntermediate = table.Column<int>(nullable: false),
                    LecturerTotalOutsideInternationalOrther = table.Column<int>(nullable: false),
                    LecturerTotalInternationalInside = table.Column<int>(nullable: false),
                    LecturerTotalInternationalInsideProfessor = table.Column<int>(nullable: false),
                    LecturerTotalInternationalInsideAssociateProfessor = table.Column<int>(nullable: false),
                    LecturerTotalInsideInternationalDoctor = table.Column<int>(nullable: false),
                    LecturerTotalInsideInternationalMaster = table.Column<int>(nullable: false),
                    LecturerTotalInsideInternationalUniversity = table.Column<int>(nullable: false),
                    LecturerTotalInsideInternationalCollege = table.Column<int>(nullable: false),
                    LecturerTotalInsideInternationalIntermediate = table.Column<int>(nullable: false),
                    LecturerTotalInsideInternationalOrther = table.Column<int>(nullable: false),
                    AlumnusTotal = table.Column<int>(nullable: false),
                    AlumnusTotalOutside = table.Column<int>(nullable: false),
                    AlumnusTotalInside = table.Column<int>(nullable: false),
                    AlumnusTotalSofware = table.Column<int>(nullable: false),
                    AlumnusTotalOutsideSofware = table.Column<int>(nullable: false),
                    AlumnusTotalInsideSofware = table.Column<int>(nullable: false),
                    AlumnusTotalOther = table.Column<int>(nullable: false),
                    AlumnusTotalOutsideOther = table.Column<int>(nullable: false),
                    AlumnusTotalInsideOther = table.Column<int>(nullable: false),
                    StudentTotal = table.Column<int>(nullable: false),
                    StudentTotalOutside = table.Column<int>(nullable: false),
                    StudentTotalInside = table.Column<int>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnels_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amounts_CustomerId",
                table: "Amounts",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_CustomerId",
                table: "Owners",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_CustomerId",
                table: "Personnels",
                column: "CustomerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amounts");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Personnels");

            migrationBuilder.DropColumn(
                name: "ActiveDay",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AddressBuilding",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AmountId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PersonnelId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "VondautuProcess",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "VondautuRegister",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "VondieuleUSD",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "VondieuleVND",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "TimeOfChangeInvestmentCertificates",
                table: "Customers",
                newName: "YearStarted");

            migrationBuilder.RenameColumn(
                name: "TimeOfChangeBusinessLicense",
                table: "Customers",
                newName: "YearEnded");

            migrationBuilder.RenameColumn(
                name: "StartYear",
                table: "Customers",
                newName: "Room");

            migrationBuilder.RenameColumn(
                name: "SignDayActivities",
                table: "Customers",
                newName: "InvestmentCertificateDate");

            migrationBuilder.RenameColumn(
                name: "ObjectDetail",
                table: "Customers",
                newName: "MainCareer");

            migrationBuilder.RenameColumn(
                name: "NumberOfInvestmentCertificate",
                table: "Customers",
                newName: "InvestmentCertificateTime");

            migrationBuilder.RenameColumn(
                name: "NumberOfBusinessLicense",
                table: "Customers",
                newName: "InvestmentCertificate");

            migrationBuilder.RenameColumn(
                name: "NumberOfActivities",
                table: "Customers",
                newName: "Floor");

            migrationBuilder.RenameColumn(
                name: "MemberOfQuangTrungSoftware",
                table: "Customers",
                newName: "DeputyTel");

            migrationBuilder.RenameColumn(
                name: "MarketActive",
                table: "Customers",
                newName: "DeputyPosition");

            migrationBuilder.RenameColumn(
                name: "MainCarrer",
                table: "Customers",
                newName: "DeputyNation");

            migrationBuilder.RenameColumn(
                name: "ExpirationDateActivities",
                table: "Customers",
                newName: "ContractNoDateRegister");

            migrationBuilder.RenameColumn(
                name: "EndYear",
                table: "Customers",
                newName: "DeputyName");

            migrationBuilder.RenameColumn(
                name: "DateOfIssuingInvestmentCertificate",
                table: "Customers",
                newName: "ContractNoDateOut");

            migrationBuilder.RenameColumn(
                name: "DateOfIssuingBusinessLicense",
                table: "Customers",
                newName: "BusinessLicenseDate");

            migrationBuilder.RenameColumn(
                name: "Carrer",
                table: "Customers",
                newName: "DeputyMail");

            migrationBuilder.RenameColumn(
                name: "BusinessType",
                table: "Customers",
                newName: "ContractNo");

            migrationBuilder.RenameColumn(
                name: "Agency",
                table: "Customers",
                newName: "Career");

            migrationBuilder.RenameColumn(
                name: "AddressRoom",
                table: "Customers",
                newName: "BusinessLicenseTime");

            migrationBuilder.RenameColumn(
                name: "AddressProvince",
                table: "Customers",
                newName: "BusinessLicense");

            migrationBuilder.RenameColumn(
                name: "AddressFloor",
                table: "Customers",
                newName: "Building");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEstablish",
                table: "Customers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 28, 15, 3, 57, 490, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "DeputyGender",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Investment",
                table: "Customers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StaffCount",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalInvestment",
                table: "Customers",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
