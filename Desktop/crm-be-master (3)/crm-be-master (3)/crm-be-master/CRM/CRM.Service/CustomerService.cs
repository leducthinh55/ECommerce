using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Linq;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using CRM.Service.Utils;
using System.Reflection;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Globalization;
using System.Diagnostics;

namespace CRM.Service
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Customer> GetCustomers(Expression<Func<Customer, bool>> where);
        IQueryable<Customer> _GetCustomers(Expression<Func<Customer, bool>> where);


        Customer GetCustomer(Guid id);
        void CreateCustomer(Customer Customer, string username);
        void EditCustomer(Customer newCustomer, Customer oldCustomer, string username);
        void RemoveCustomer(Guid id);
        Task<string> UploadPhoto(IFormFile file);
        void SaveCustomer();
        void DeleteCustomer(Customer customer);
        void AdaptChange(Customer customer, DateTime SelectedDate);
        FileSupport GetExcel();
        void ExportCustomerToExcel(string downloadUrl, string name);
        void ImportCustomerFromExcel(string Url);
    }

    public class CustomerService : ICustomerService
    {
        string propertyName = "Tên đơn vị|ĐV_Tên viết tắt|ĐV_Quốc tịch|ĐV_Phân loại quốc tịch|ĐV_Vốn điều lệ (vnđ)|ĐV_Vốn điều lệ (usd)|Vốn đầu tư_đăng ký (vnđ)|Vốn đầu tư_thực hiện (vnđ)|ĐV_Thị trường hoạt động|ĐV_Phân loại thị trường|ĐV_Năm bắt đầu HĐ tại QTSC|ĐV_Năm kết thúc HĐ tại QTSC|ĐV_Tên giao dịch|ĐV_Loại hình doanh nghiệp|ĐV_Phân loại doanh nghiệp|ĐV_Phân loại đơn vị|ĐV_Chi tiết đơn vị|ĐV_Lĩnh vực hoạt động|ĐV_Hoạt động chính|ĐV_Sản phẩm tiêu biểu|ĐV_Địa chỉ|ĐV_Địa chỉ_Tỉnh/tp|ĐV_Cơ quan chủ quản|ĐV_ĐC_Tòa nhà|ĐV_ĐC_Tầng|ĐV_ĐC_Phòng|ĐV_Điện thoại bàn|ĐV_TEL|ĐV_FAX|ĐV_EMAIL|ĐV_WEBSITE|ĐV_Giấy CNĐT số|ĐV_Ngày cấp Giấy CNĐT|ĐV_Lần thay đổi Giấy CNĐT|DV_Giấy phép ĐKKD số|ĐV_Ngày cấp Giấy phép ĐKKD|ĐV_Lần thay đổi Giấy phép ĐKKD|ĐV_Mã số thuế|ĐV_Ngày hoạt động/thành lập|ĐV_SỐ HĐ|ĐV_Ngày ký HĐ|ĐV_Ngày hết hạn HĐ|ĐV_Thành viên Công viên Phần mềm Quang Trung|Ghi chú|Tên|Chức danh|Giới tính|Ngày sinh|Quốc tịch|SĐT|email|Tên chủ sở hữu|Mã số doanh nghiệp|Nơi cấp|Ngày cấp|Địa chỉ trụ sở chính|Người đại diện pháp luật|Chức danh|Giới tính|Ngày sinh|Quốc tịch|Tổng cộng nhân viên|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng nhân viên nam|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng nhân viên nữ|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng nhân viên|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng nhân viên|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng chuyên viên phẩn mềm|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng nhân viên khác|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng nhân viên trình độ quốc tế|Tổng nhân viên trình độ trong nước|Tổng nhân viên trình độ trong nước bên ngoài Công viên phần mềm Quang Trung|Giáo sư|Phó Giáo sư|Tiến sĩ|Thạc sĩ|Đại học|Cao đẳng|Trung cấp|Khác|Tổng nhân viên trình độ trong nước nội khu Công viên phần mềm Quang Trung|Giáo sư|Phó Giáo sư|Tiến sĩ|Thạc sĩ|Đại học|Cao đẳng|Trung cấp|Khác|Tổng nhân viên trình độ quốc tế bên ngoài Công viên phần mềm Quang Trung|Giáo sư|Phó Giáo sư|Tiến sĩ|Thạc sĩ|Đại học|Cao đẳng|Trung cấp|Khác|Tổng nhân viên trình độ quốc tế nội khu Công viên phần mềm Quang Trung|Giáo sư|Phó Giáo sư|Tiến sĩ|Thạc sĩ|Đại học|Cao đẳng|Trung cấp|Khác|Tổng cộng giảng viên|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng giảng viên nam|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng giảng viên nữ|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng giảng viên|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng giảng viên|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng giảng viên trình độ quốc tế|Tổng giảng viên trình độ trong nước|Tổng giảng viên trình độ trong nước bên ngoài Công viên phần mềm Quang Trung|Giáo sư|Phó Giáo sư|Tiến sĩ|Thạc sĩ|Đại học|Cao đẳng|Trung cấp|Khác|Tổng giảng viên trình độ trong nước nội khu Công viên phần mềm Quang Trung|Giáo sư|Phó Giáo sư|Tiến sĩ|Thạc sĩ|Đại học|Cao đẳng|Trung cấp|Khác|Tổng giảng viên trình độ quốc tế bên ngoài Công viên phần mềm Quang Trung|Giáo sư|Phó Giáo sư|Tiến sĩ|Thạc sĩ|Đại học|Cao đẳng|Trung cấp|Khác|Tổng giảng viên trình độ quốc tế nội khu Công viên phần mềm Quang Trung|Giáo sư|Phó Giáo sư|Tiến sĩ|Thạc sĩ|Đại học|Cao đẳng|Trung cấp|Khác|Tổng cộng sinh viên|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng sinh viên công nghệ|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng sinh viên khác|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng cộng học viên|Bên ngoài Công viên phần mềm Quang Trung|Nội khu Công viên phần mềm Quang Trung|Tổng doanh thu (vnđ)|Tổng doanh thu (usd)|Bên ngoài Công viên phần mềm Quang Trung (vnđ)|Nội khu Công viên phần mềm Quang Trung (vnđ)|Bên ngoài Công viên phần mềm Quang Trung (usd)|Nội khu Công viên phần mềm Quang Trung (usd)|Tổng doanh thu nội địa (vnđ)|Tổng doanh thu nội địa (usd)|Bên ngoài Công viên phần mềm Quang Trung (vnđ)|Nội khu Công viên phần mềm Quang Trung (vnđ)|Bên ngoài Công viên phần mềm Quang Trung (usd)|Nội khu Công viên phần mềm Quang Trung (usd)|Tổng doanh thu xuất khẩu (vnđ)|Tổng doanh thu xuất khẩu (usd)|Bên ngoài Công viên phần mềm Quang Trung (vnđ)|Nội khu Công viên phần mềm Quang Trung (vnđ)|Bên ngoài Công viên phần mềm Quang Trung (usd)|Nội khu Công viên phần mềm Quang Trung (usd)|Tỷ lệ doanh thu xuất khẩu trên tổng doanh thu";
        string propertyInCode = "Name|ShortName|Country|CountryType|VondieuleVND|VondieuleUSD|VondautuRegister|VondautuProcess|MarketActive|MarketType|StartYear|EndYear|TransactionName|BusinessType|CompanyType|ObjectType|ObjectDetail|Carrer|MainCarrer|ProductHighlight|Address|AddressProvince|Agency|AddressBuilding|AddressFloor|AddressRoom|TableTel|Tel|Fax|Email|Website|NumberOfInvestmentCertificate|DateOfIssuingInvestmentCertificate|TimeOfChangeInvestmentCertificates|NumberOfBusinessLicense|DateOfIssuingBusinessLicense|TimeOfChangeBusinessLicense|TaxCode|ActiveDay|NumberOfActivities|SignDayActivities|ExpirationDateActivities|MemberOfQuangTrungSoftware|Note|Name|Position|Gender|Birthday|Country|PhoneNumber|Email|Name|CompanyCode|IssuingCompanyPlace|IssuingCompanyDate|AddressMainTown|LegalRepresentativePeople|Position|Gender|Birthday|Country|TotalEmployee|TotalEmployeeOutSide|TotalEmployeeInSide|TotalMaleEmployee|TotalMaleEmployeeOutside|TotalMaleEmployeeInside|TotalFeMaleEmployee|TotalFeMaleEmployeeOutside|TotalFeMaleEmployeeInside|TotalOfficialEmployee|TotalOfficialEmployeeOutside|TotalOfficialEmployeeInside|TotalPartTimeEmployee|TotalPartTimeEmployeeOutside|TotalPartTimeEmployeeInside|TotalSoftwareEmployee|TotalSoftwareEmployeeOutside|TotalSoftwareEmployeeInside|TotalOtherEmployee|TotalOtherEmployeeOutside|TotalOtherEmployeeInside|TotalInternationalEmployee|TotalDomesticEmployee|TotalDomesticEmployeeOutside|TotalDomesticEmployeeOutsideProfessor|TotalDomesticEmployeeOutsideAssociateProfessor|TotalDomesticEmployeeOutsideDoctor|TotalDomesticEmployeeOutsideMaster|TotalDomesticEmployeeOutsideUniversity|TotalDomesticEmployeeOutsideCollege|TotalDomesticEmployeeOutsideIntermediate|TotalDomesticEmployeeOutsideOther|TotalDomesticEmployeeInside|TotalDomesticEmployeeInsideProfessor|TotalDomesticEmployeeInsideAssociateProfessor|TotalDomesticEmployeeInsideDoctor|TotalDomesticEmployeeInsideMaster|TotalDomesticEmployeeInsideUniversity|TotalDomesticEmployeeInsideCollege|TotalDomesticEmployeeInsideIntermediate|TotalDomesticEmployeeInsideOther|TotalInternationalEmployeeOutside|TotalInternationalEmployeeOutsideProfessor|TotalInternationalEmployeeOutsideAssociateProfessor|TotalInternationalEmployeeOutsideDoctor|TotalInternationalEmployeeOutsideMaster|TotalInternationalEmployeeOutsideUniversity|TotalInternationalEmployeeOutsideCollege|TotalInternationalEmployeeOutsideIntermediate|TotalInternationalEmployeeOutsideOrther|TotalInternationalEmployeeInside|TotalInternationalEmployeeInsideProfessor|TotalInternationalEmployeeInsideAssociateProfessor|TotalInternationalEmployeeInsideDoctor|TotalInternationalEmployeeInsideMaster|TotalInternationalEmployeeInsideUniversity|TotalInternationalEmployeeInsideCollege|TotalInternationalEmployeeInsideIntermediate|TotalInternationalEmployeeInsideOrther|TotalLecturers|TotalLecturersOutside|TotalLecturersInside|TotalMaleLecturers|TotalMaleLecturersOutside|TotalMaleLecturersInside|TotalFemaleLecturers|TotalFemaleLecturersOutside|TotalFemaleLecturersInside|TotalOfficialLecturers|TotalOfficialLecturersOutside|TotalOfficialLecturersInside|TotalPartTimeLecturers|TotalPartTimeLecturersOutside|TotalPartTimeLecturersInside|TotalInternationalLecturers|TotalDomesticLecturers|TotalDomesticLecturersOutside|TotalDomesticLecturersOutsideProfessor|TotalDomesticLecturersOutsideAssociateProfessor|TotalDomesticLecturersOutsideDoctor|TotalDomesticLecturersOutsideMaster|TotalDomesticLecturersOutsideUniversity|TotalDomesticLecturersOutsideCollege|TotalDomesticLecturersOutsideIntermediate|TotalDomesticLecturersOutsideOrther|TotalDomesticLecturersInside|TotalDomesticLecturersInsideProfessor|TotalDomesticLecturersInsideAssociateProfessor|TotalDomesticLecturersInsideDoctor|TotalDomesticLecturersInsideMaster|TotalDomesticLecturersInsideUniversity|TotalDomesticLecturersInsideCollege|TotalDomesticLecturersInsideIntermediate|TotalDomesticLecturersInsideOrther|TotalInternationalLecturersOutside|TotalInternationalLecturersOutsideProfessor|TotalInternationalLecturersOutsideAssociateProfessor|TotalInternationalLecturersOutsideDoctor|TotalInternationalLecturersOutsideMaster|TotalInternationalLecturersOutsideUniversity|TotalInternationalLecturersOutsideCollege|TotalInternationalLecturersOutsideIntermediate|TotalInternationalLecturersOutsideOrther|TotalInternationalLecturersInside|TotalInternationalLecturersInsideProfessor|TotalInternationalLecturersInsideAssociateProfessor|TotalInternationalLecturersInsideDoctor|TotalInternationalLecturersInsideMaster|TotalInternationalLecturersInsideUniversity|TotalInternationalLecturersInsideCollege|TotalInternationalLecturersInsideIntermediate|TotalInternationalLecturersInsideOrther|TotalAlumnus|TotalAlumnusOutside|TotalAlumnusInside|TotalSoftwareAlumnus|TotalSoftwareAlumnusOutside|TotalSoftwareAlumnusInside|TotalOtherAlumnus|TotalOtherAlumnusOutside|TotalOtherAlumnusInside|TotalStudent|TotalStudentOutside|TotalStudentInside|TotalVndRevenue|TotalUsdRevenue|TotalVndRevenueOutside|TotalVndRevenueInside|TotalUsdRevenueOutside|TotalUsdRevenueInside|TotalDomesticVndRevenue|TotalDomesticUsdRevenue|TotalDomesticVndRevenueOutside|TotalDomesticVndRevenueInside|TotalDomesticUsdRevenueOutside|TotalDomesticUsdRevenueInside|TotalExportVndRevenue|TotalExportUsdRevenue|TotalExportVndRevenueOutside|TotalExportVndRevenueInside|TotalExportUsdRevenueOutside|TotalExportUsdRevenueInside|RatioOfExportRevenue";
        string SHEETNAME = "Duplication";
        int CustomerNum = 44;
        int DeputyNum = 7;
        int OwnerNum = 10;
        int PersonnelNum = 124;
        int AmountNum = 19;
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionLogService _transactionLogService;
        string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                   "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
                   "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                   "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                   "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, ITransactionLogService transactionLogService)
        {
            _CustomerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _transactionLogService = transactionLogService;
        }

        public void AdaptChange(Customer customer, DateTime selectedDate)
        {
            _transactionLogService.AdaptChange<Customer>(selectedDate, customer, customer.Id);
            _transactionLogService.AdaptChange<Owner>(selectedDate, customer.Owner, customer.Owner.Id);
            _transactionLogService.AdaptChange<Personnel>(selectedDate, customer.Personnel, customer.Personnel.Id);
            _transactionLogService.AdaptChange<Amount>(selectedDate, customer.Amount, customer.Amount.Id);
            _transactionLogService.AdaptChange<Deputy>(selectedDate, customer.Deputy, customer.Deputy.Id);
        }

        public void CreateCustomer(Customer Customer, string username)
        {
            if (Customer.Amount == null) Customer.Amount = new Amount();
            if (Customer.Owner == null) Customer.Owner = new Owner();
            if (Customer.Personnel == null) Customer.Personnel = new Personnel();
            if (Customer.Deputy == null) Customer.Deputy = new Deputy();
            _CustomerRepository.Add(Customer);
            SaveCustomer();
            Customer.Code = Customer.No.ToString("00000000");
            SaveCustomer();
            _transactionLogService.UpdateTransaction<Customer>(null, Customer, username);
            //TODO : Xem xet lai de tang performance
            _transactionLogService.UpdateTransaction<Amount>(null, Customer.Amount, username);
            _transactionLogService.UpdateTransaction<Deputy>(null, Customer.Deputy, username);
            _transactionLogService.UpdateTransaction<Personnel>(null, Customer.Personnel, username);
            _transactionLogService.UpdateTransaction<Owner>(null, Customer.Owner, username);
        }

        public void EditCustomer(Customer newCustomer, Customer oldCustomer, string username)
        {
            //var entity = _CustomerRepository.GetById(newCustomer.Id);
            //entity = newCustomer;
            _CustomerRepository.Update(newCustomer);
            //var newCustomer = customer.Adapt<CustomerDetailVM>().Adapt<Customer>();

            if (!_transactionLogService.IsTheSame<Customer>(oldCustomer, newCustomer))
            {
                _transactionLogService.UpdateTransaction<Customer>(oldCustomer, newCustomer, username);
            }
            if (!_transactionLogService.IsTheSame<Owner>(oldCustomer.Owner, newCustomer.Owner))
            {
                _transactionLogService.UpdateTransaction<Owner>(oldCustomer.Owner, newCustomer.Owner, username);
            }
            if (!_transactionLogService.IsTheSame<Personnel>(oldCustomer.Personnel, newCustomer.Personnel))
            {
                _transactionLogService.UpdateTransaction<Personnel>(oldCustomer.Personnel, newCustomer.Personnel, username);
            }
            if (!_transactionLogService.IsTheSame<Amount>(oldCustomer.Amount, newCustomer.Amount))
            {
                _transactionLogService.UpdateTransaction<Amount>(oldCustomer.Amount, newCustomer.Amount, username);
            }
            if (!_transactionLogService.IsTheSame<Deputy>(oldCustomer.Deputy, newCustomer.Deputy))
            {
                _transactionLogService.UpdateTransaction<Deputy>(oldCustomer.Deputy, newCustomer.Deputy, username);
            }
        }

        public Customer GetCustomer(Guid id)
        {
            return _CustomerRepository.GetById(id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _CustomerRepository.GetAll();
        }

        public void RemoveCustomer(Guid id)
        {
            var entity = _CustomerRepository.GetById(id);
            _CustomerRepository.Delete(entity);
        }
        public async Task<string> UploadPhoto(IFormFile file)
        {
            var result = await _CustomerRepository.UploadImage(file);
            return result;
        }

        public void SaveCustomer()
        {
            _unitOfWork.Commit();
        }

        public void DeleteCustomer(Customer customer)
        {
            _CustomerRepository.Delete(customer);
        }

        public IEnumerable<Customer> GetCustomers(Expression<Func<Customer, bool>> where)
        {
            return _CustomerRepository.GetMany(where);
        }

        public IQueryable<Customer> _GetCustomers(Expression<Func<Customer, bool>> where)
        {
            return _CustomerRepository._GetMany(where);
        }

        public FileSupport GetExcel()
        {
            var customers = _CustomerRepository.GetMany(c => c.Name.Contains("ha")).ToList();
            var stream = new MemoryStream();

            try
            {
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                    // mutual
                    workSheet.Row(1).Height = 20;
                    workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(1).Style.Font.Bold = true;

                    int row = 1;
                    string[] propertyNames = propertyName.Split('|');
                    foreach (var propertyName in propertyNames)
                    {
                        workSheet.Cells[1, row].Value = propertyName;
                        row++;
                    }
                    row = 1;
                    string[] propertyInCodes = propertyInCode.Split('|');
                    foreach (var propertyName in propertyInCodes)
                    {
                        workSheet.Cells[2, row].Value = propertyName;
                        row++;
                    }

                    int recordIndex = 3;
                    var check = "";
                    foreach (var customer in customers)
                    {
                        row = 1;
                        foreach (var propertyName in propertyInCodes.Take(CustomerNum))
                        {
                            if (customer.GetType().GetProperty(propertyName) == null)
                            {
                                check += propertyName + ", ";
                                row++;
                            }
                            else
                            {
                                workSheet.Cells[recordIndex, row].Value = customer.GetType().GetProperty(propertyName).GetValue(customer, null);

                                //Cot tiep theo
                                row++;
                            }
                        }
                        //Cong don so dong skip
                        int numskip = CustomerNum;
                        foreach (var propertyName in propertyInCodes.Skip(numskip).Take(DeputyNum))
                        {
                            workSheet.Cells[recordIndex, row].Value = customer.Deputy.GetType().GetProperty(propertyName).GetValue(customer.Deputy, null);
                            row++;
                        }
                        numskip += DeputyNum;
                        foreach (var propertyName in propertyInCodes.Skip(numskip).Take(OwnerNum))
                        {
                            workSheet.Cells[recordIndex, row].Value = customer.Owner.GetType().GetProperty(propertyName).GetValue(customer.Owner, null);
                            row++;
                        }
                        numskip += OwnerNum;
                        foreach (var propertyName in propertyInCodes.Skip(numskip).Take(PersonnelNum))
                        {
                            if (customer.Personnel.GetType().GetProperty(propertyName) == null)
                            {
                                check += propertyName + ", ";
                                row++;
                            }
                            else
                            {
                                workSheet.Cells[recordIndex, row].Value = customer.Personnel.GetType().GetProperty(propertyName).GetValue(customer.Personnel, null);
                                //Cot tiep theo
                                row++;
                            }
                        }
                        numskip += PersonnelNum;
                        foreach (var propertyName in propertyInCodes.Skip(numskip).Take(AmountNum))
                        {
                            workSheet.Cells[recordIndex, row].Value = customer.Amount.GetType().GetProperty(propertyName).GetValue(customer.Amount, null);
                            row++;
                        }
                        recordIndex++;
                    }

                    package.Save();
                }
                stream.Position = 0;
                return new FileSupport
                {
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    FileName = $"CustomerList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx",
                    Stream = stream
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void ExportCustomerToExcel(string downloadUrl, string name)
        {
            string templeteUrl = "Document/Files/ExcelTemplete/ExportTemplate.xlsx";
            FileInfo file = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), templeteUrl)).CopyTo(downloadUrl, true);
            //return;
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), templeteUrl)).CopyTo(downloadUrl, true);
            }
            name = name != null ? name : "";

            var customers = _CustomerRepository.GetMany(c => c.Name.Contains(name)).ToList();

            try
            {
                using (var package = new ExcelPackage(file))
                {
                    var workSheet = package.Workbook.Worksheets.Where(_ => _.Name.Equals(SHEETNAME)).FirstOrDefault();

                    #region Delete (Create new Excel and format)
                    //var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                    // mutual
                    //workSheet.Row(1).Height = 20;
                    //workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //workSheet.Row(1).Style.Font.Bold = true;

                    //Create Header of Excel (Will delete)
                    //int row = 1;
                    //string[] propertyNames = propertyName.Split('|');
                    //foreach (var propertyName in propertyNames)
                    //{
                    //    workSheet.Cells[1, row].Value = propertyName;
                    //    row++;
                    //}
                    //row = 1;
                    //string[] propertyInCodes = propertyInCode.Split('|');
                    //foreach (var propertyName in propertyInCodes)
                    //{
                    //    workSheet.Cells[2, row].Value = propertyName;
                    //    row++;
                    //}
                    #endregion

                    //Get PropertyCode On Template
                    int column = 1;
                    var value = workSheet.Cells[6, column++].Value;
                    propertyInCode = value.ToString() + "|";
                    while (value != null)
                    {
                        value = workSheet.Cells[6, column++].Value;
                        if (value != null)
                        {
                            propertyInCode += value.ToString() + "|";
                        }
                    }
                    propertyInCode = propertyInCode.Remove(propertyInCode.Length - 1);
                    int row = 1;
                    string[] propertyInCodes = propertyInCode.Split('|');

                    int recordIndex = 7;
                    var check = "";
                    foreach (var customer in customers)
                    {

                        var deputy = customer.Deputy;
                        var owner = customer.Owner;
                        var personnel = customer.Personnel;
                        var amount = customer.Amount;
                        row = 1;
                        foreach (var propertyName in propertyInCodes.Take(CustomerNum))
                        {
                            if (customer.GetType().GetProperty(propertyName) == null)
                            {
                                check += propertyName + ", ";
                                row++;
                            }
                            else
                            {
                                workSheet.Cells[recordIndex, row].Value = getObjectValue<Customer>(propertyName, customer);
                                //Cot tiep theo
                                row++;
                            }
                        }
                        //Cong don so dong skip
                        int numskip = CustomerNum;
                        foreach (var propertyName in propertyInCodes.Skip(numskip).Take(DeputyNum))
                        {
                            workSheet.Cells[recordIndex, row].Value = getObjectValue<Deputy>(propertyName, deputy);
                            row++;
                        }
                        numskip += DeputyNum;
                        foreach (var propertyName in propertyInCodes.Skip(numskip).Take(OwnerNum))
                        {
                            workSheet.Cells[recordIndex, row].Value = getObjectValue<Owner>(propertyName, owner);
                            row++;
                        }
                        numskip += OwnerNum;
                        foreach (var propertyName in propertyInCodes.Skip(numskip).Take(PersonnelNum))
                        {
                            workSheet.Cells[recordIndex, row].Value = getObjectValue<Personnel>(propertyName, personnel);
                            row++;
                        }
                        numskip += PersonnelNum;
                        foreach (var propertyName in propertyInCodes.Skip(numskip).Take(AmountNum))
                        {
                            //workSheet.Cells[recordIndex, row].Value = amount.GetType().GetProperty(propertyName).GetValue(amount, null);
                            workSheet.Cells[recordIndex, row].Value = getObjectValue<Amount>(propertyName, amount);
                            row++;
                        }
                        recordIndex++;
                    }
                    package.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private object getObjectValue<T>(String propertyName, T model) where T : class
        {
            object result = null;
            var property = model.GetType().GetProperty(propertyName.Trim());
            switch (typeof(T).Name)
            {
                case "Customer":
                    if (propertyName.Equals("MarketType") ||
                       propertyName.Equals("MarketActive") ||
                       propertyName.Equals("ObjectType") ||
                       propertyName.Equals("CompanyType"))
                    {
                        result = property.GetValue(model, null);
                        if (!String.IsNullOrEmpty((String)result))
                        {
                            var b = JsonConvert.DeserializeObject((String)result).ToString();
                            result = "";
                            var a = b.Trim(new Char[] { '\t', '\n', '[', ']', '\"', '\r' }).Trim().Split(new char[] { '\"' });
                            if (a.Length % 2 == 0)
                            {
                                for (int i = 0; i < a.Length; i++)
                                {
                                    result += a[i + 1];
                                    result += i == a.Length - 2 ? "" : ", ";
                                    i++;
                                }
                            }
                            else
                            {
                                for (int i = 1; i < a.Length; i++)
                                {
                                    result += a[i];
                                    result += i == a.Length - 1 ? "" : ", ";
                                    i++;
                                }
                            }
                        }
                        break;
                    }
                    if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
                    {
                        var a = property.GetValue(model, null);
                        result = a != null ? ((DateTime)a).Date.ToString("yyyy/MM/dd") : null;
                        break;
                    }
                    result = property.GetValue(model, null);
                    break;
                case "Amount":
                case "Owner":
                case "Personnel":
                case "Deputy":
                default:
                    //Convert DateTime
                    if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
                    {
                        var a = property.GetValue(model, null);
                        result = a != null ? ((DateTime)a).Date.ToString("yyyy/MM/dd") : null;
                        break;
                    }

                    //Gender with int Type
                    if (property.PropertyType == typeof(int) && propertyName.Equals("Gender"))
                    {
                        result = (int)property.GetValue(model, null) == 0 ? "Nam" : "Nu";
                        break;
                    }
                    result = property.GetValue(model, null);
                    break;
            }
            return result;
        }

        private void SetObjectValue<T>(T obj, string propertyName, object value, int row, int column) where T : class
        {

            try
            {
                PropertyInfo prop = obj.GetType().GetProperty(propertyName.Trim(), BindingFlags.Public | BindingFlags.Instance);
                if (null == prop || !prop.CanWrite)
                {
                    return;
                }
                Type typeValue = prop.PropertyType;
                if (typeValue == typeof(string))
                {
                    if (value == null)
                    {
                        return;
                    }
                    value = value.ToString().Trim();
                }
                else if (typeValue == typeof(int))
                {
                    value = value == null ? 0 : Convert.ToInt32(value);
                }
                else if (typeValue == typeof(decimal))
                {
                    value = value == null ? Convert.ToDecimal(0) : Convert.ToDecimal(value);
                }
                else if (typeValue == typeof(DateTime?) || prop.PropertyType == typeof(DateTime))
                {
                    if (value == null)
                    {
                        return;
                    }
                    if (String.IsNullOrWhiteSpace(value.ToString()))
                    {
                        return;
                    }
                    DateTime dateValue;
                    if (DateTime.TryParseExact(value.ToString(), formats,
                            new CultureInfo("en-US"),
                              DateTimeStyles.None,
                              out dateValue))
                    {
                        value = DateTime.Parse(value.ToString());
                    }
                    else
                    {
                        long dateNum = long.Parse(value.ToString());
                        value = DateTime.FromOADate(dateNum);
                    }
                }

                prop.SetValue(obj, value, null);

            }
            catch (Exception e)
            {
                String path = Path.Combine(Directory.GetCurrentDirectory(), "Document/Files/Log.txt");
                String log = $"Dòng: {row}, cột: {column}, nhập không đúng định dạng\n";
                File.AppendAllText(path, log);
                Console.WriteLine(e);
            }


        }

        public void ImportCustomerFromExcel(String FileName)
        {
            var listCustomer = _CustomerRepository.GetAll().ToLookup(_ => _.Name.ToLower());
            string templeteUrl = "Document/Du-lieu-KH_16.01.2020.xlsx";
            FileInfo file = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), templeteUrl));
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Duplication"];
                int totalRows = workSheet.Dimension.Rows;

                String[] propertyName = new String[204];
                for (int column = 1; column <= 204; column++)
                {
                    var x = workSheet.Cells[6, column].Value;
                    propertyName[column - 1] = x.ToString();
                }

                for (int row = 7; row <= totalRows; row++)
                {
                    var cusName = workSheet.Cells[row, 1].Value;
                    if (cusName == null)
                    {
                        continue;
                    }
                    else if(listCustomer.Contains(cusName.ToString().ToLower()))
                    {
                        continue;
                    }
                    Customer customer = new Customer();
                    Deputy deputy = new Deputy();
                    Owner owner = new Owner();
                    Personnel personnel = new Personnel();
                    Amount amount = new Amount();

                    for (int column = 1; column <= 44; column++)
                    {
                        var value = workSheet.Cells[row, column].Value;
                        SetObjectValue(customer, propertyName[column - 1], value, row, column);
                    }

                    for (int column = 45; column <= 51; column++)
                    {
                        var value = workSheet.Cells[row, column].Value;
                        SetObjectValue(deputy, propertyName[column - 1], value, row, column);
                    }
                    customer.Deputy = deputy;

                    for (int column = 52; column <= 61; column++)
                    {
                        var value = workSheet.Cells[row, column].Value;
                        SetObjectValue(owner, propertyName[column - 1], value, row, column);
                    }
                    customer.Owner = owner;

                    for (int column = 62; column <= 185; column++)
                    {
                        var value = workSheet.Cells[row, column].Value;
                        SetObjectValue(personnel, propertyName[column - 1], value, row, column);
                    }
                    customer.Personnel = personnel;

                    for (int column = 186; column <= 204; column++)
                    {
                        var value = workSheet.Cells[row, column].Value;
                        SetObjectValue(amount, propertyName[column - 1], value, row, column);
                    }
                    customer.Amount = amount;

                    _CustomerRepository.Add(customer);
                    SaveCustomer();
                    customer.Code = customer.No.ToString("00000000");
                    SaveCustomer();
                }
            }
        }
    }
}
