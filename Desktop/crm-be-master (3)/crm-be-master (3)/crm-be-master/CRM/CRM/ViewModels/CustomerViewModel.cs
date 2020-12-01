using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;

namespace CRM.ViewModels
{
    public class CustomerShortVM
    {
        public Guid Id { get; set; }
        [DisplayName("Mã Khách Hàng")]
        public string Code { get; set; }
        [DisplayName("ĐV_Tên")]
        public string Name { get; set; }
    }
    public class CustomerVM
    {
        public Guid Id { get; set; }
        [DisplayName("Mã Khách Hàng")]
        public string Code { get; set; }
        [DisplayName("ĐV_Tên")]
        public string Name { get; set; }
        [DisplayName("ĐV_Tên viết tắt")]
        public string ShortName { get; set; }
        [DisplayName("ĐV_Quốc tịch")]
        public string Country { get; set; }
        [DisplayName("ĐV_Phân loại thị trường")]
        public object MarketType { get; set; }
        [DisplayName("ĐV_Năm bắt đầu HĐ tại QTSC")]
        public DateTime? StartYear { get; set; }
        [DisplayName("ĐV_Năm kết thúc HĐ tại QTSC")]
        public string EndYear { get; set; }
        [DisplayName("ĐV_Loại hình doanh nghiệp")]
        public string BusinessType { get; set; }
        [DisplayName("ĐV_Phân loại doanh nghiệp")]
        public object CompanyType { get; set; }
        [DisplayName("ĐV_Phân loại đối tượng")]
        public object ObjectType { get; set; }
        [DisplayName("ĐV_Địa chỉ")]
        public string Address { get; set; }
        [DisplayName("ĐV_ĐC_Tòa nhà")]
        public string AddressBuilding { get; set; }
        [DisplayName("ĐV_ĐC_Tầng")]
        public string AddressFloor { get; set; }
        [DisplayName("ĐV_ĐC_Phòng")]
        public string AddressRoom { get; set; }
        [DisplayName("ĐV_TEL")]
        public string Tel { get; set; }
        [DisplayName("ĐV_Mã số thuế")]
        public string TaxCode { get; set; }
        [DisplayName("ĐV_Ngày hoạt động/thành lập")]
        public DateTime? ActiveDay { get; set; }
        
        [DisplayName("Ghi chú")]
        public string Note { get; set; }
        public string ProfilePicture { get; set; }

        public Guid CustomerTypeId { get; set; }
        public string IdentityCardId { get; set; }
    }

    public class CustomerDetailVM
    {
        public Guid Id { get; set; }
        public int No { get; set; }
        [DisplayName("Mã Khách Hàng")]
        public string Code { get; set; }
        [DisplayName("ĐV_Tên")]
        public string Name { get; set; }
        [DisplayName("ĐV_Tên viết tắt")]
        public string ShortName { get; set; }
        [DisplayName("ĐV_Quốc tịch")]
        public string Country { get; set; }
        [DisplayName("ĐV_Phân loại quốc tịch")]
        public string CountryType { get; set; }
        [DisplayName("ĐV_Vốn điều lệ (vnđ)")]
        public decimal VondieuleVND { get; set; }
        [DisplayName("ĐV_Vốn điều lệ (usd)")]
        public decimal VondieuleUSD { get; set; }
        [DisplayName("Vốn đầu tư_đăng ký (vnđ)")]
        public decimal VondautuRegister { get; set; }
        [DisplayName("Vốn đầu tư_thực hiện (vnđ)")]
        public decimal VondautuProcess { get; set; }
        
        [DisplayName("ĐV_Năm bắt đầu HĐ tại QTSC")]
        public DateTime? StartYear { get; set; }
        [DisplayName("ĐV_Năm kết thúc HĐ tại QTSC")]
        public string EndYear { get; set; }
        [DisplayName("ĐV_Tên giao dịch")]
        public string TransactionName { get; set; }
        [DisplayName("ĐV_Loại hình doanh nghiệp")]
        public string BusinessType { get; set; }
        [DisplayName("ĐV_Chi tiết đối tượng")]
        public string ObjectDetail { get; set; }
        [DisplayName("ĐV_Lĩnh vực hoạt động")]
        public string Carrer { get; set; }
        [DisplayName("ĐV_Hoạt động chính")]
        public string MainCarrer { get; set; }
        [DisplayName("ĐV_Sản phẩm tiêu biểu")]
        public string ProductHighlight { get; set; }
        [DisplayName("ĐV_Địa chỉ")]
        public string Address { get; set; }
        [DisplayName("ĐV_ĐV_Địa chỉ_Tỉnh/tp")]
        public string AddressProvince { get; set; }
        [DisplayName("ĐV_Thành viên Công viên Phần mềm Quang Trung")]
        public string MemberOfQuangTrungSoftware { get; set; }
        [DisplayName("ĐV_Cơ quan chủ quản")]
        public string Agency { get; set; }
        [DisplayName("ĐV_ĐC_Tòa nhà")]
        public string AddressBuilding { get; set; }
        [DisplayName("ĐV_ĐC_Tầng")]
        public string AddressFloor { get; set; }
        [DisplayName("ĐV_ĐC_Phòng")]
        public string AddressRoom { get; set; }
        public string TableTel { get; set; }
        [DisplayName("ĐV_TEL")]
        public string Tel { get; set; }
        [DisplayName("ĐV_FAX")]
        public string Fax { get; set; }
        [DisplayName("ĐV_EMAIL")]
        public string Email { get; set; }
        [DisplayName("ĐV_WEBSITE")]
        public string Website { get; set; }
        [DisplayName("ĐV_Giấy CNĐT số")]
        public string NumberOfInvestmentCertificate { get; set; }
        [DisplayName("ĐV_Ngày cấp Giấy CNĐT")]
        public DateTime? DateOfIssuingInvestmentCertificate { get; set; }
        [DisplayName("ĐV_Lần thay đổi Giấy CNĐT")]
        public string TimeOfChangeInvestmentCertificates { get; set; }
        [DisplayName("ĐV_Giấy phép ĐKKD số")]
        public string NumberOfBusinessLicense { get; set; }
        [DisplayName("ĐV_Ngày cấp Giấy phép ĐKKD")]
        public DateTime? DateOfIssuingBusinessLicense { get; set; }
        [DisplayName("ĐV_Lần thay đổi Giấy phép ĐKKD")]
        public string TimeOfChangeBusinessLicense { get; set; }
        [DisplayName("ĐV_Mã số thuế")]
        public string TaxCode { get; set; }
        [DisplayName("ĐV_Ngày hoạt động/thành lập")]
        public DateTime? ActiveDay { get; set; }
        [DisplayName("ĐV_SỐ HĐ")]
        public string NumberOfActivities { get; set; }
        [DisplayName("ĐV_Ngày ký HĐ")]
        public DateTime? SignDayActivities { get; set; }
        [DisplayName("ĐV_Ngày hết hạn HĐ")]
        public DateTime? ExpirationDateActivities { get; set; }
        [DisplayName("Ghi chú")]
        public string Note { get; set; }
        public string ProfilePicture { get; set; }

        [DisplayName("ĐV_Phân loại đối tượng")]
        public object ObjectType { get; set; }
        [DisplayName("ĐV_Phân loại thị trường")]
        public object MarketType { get; set; }
        [DisplayName("ĐV_Phân loại doanh nghiệp")]
        public object CompanyType { get; set; }
        [DisplayName("ĐV_Thị trường hoạt động")]
        public object MarketActive { get; set; }

        public Guid? CustomerTypeId { get; set; }
        public string IdentityCardId { get; set; }

        public OwnerVM OwnerVM { get; set; }
        public AmountVM AmountVM { get; set; }
        public PersonnelVM PersonnelVM { get; set; }
        public DeputyVM DeputyVM { get; set; }
    }

    public class ErrorList
    {
        public Object Data { get; set; }
        public string Error { get; set; }
    }

    public class CustomerCML
    {

        //public int No { get; set; }
        //[DisplayName("Mã Khách Hàng")]
        //public string Code { get; set; }
        [DisplayName("ĐV_Tên")]
        public string Name { get; set; }
        [DisplayName("ĐV_Tên viết tắt")]
        public string ShortName { get; set; }
        [DisplayName("ĐV_Quốc tịch")]
        public string Country { get; set; }
        [DisplayName("ĐV_Phân loại quốc tịch")]
        public string CountryType { get; set; }
        [DisplayName("ĐV_Vốn điều lệ (vnđ)")]
        public decimal VondieuleVND { get; set; }
        [DisplayName("ĐV_Vốn điều lệ (usd)")]
        public decimal VondieuleUSD { get; set; }
        [DisplayName("Vốn đầu tư_đăng ký (vnđ)")]
        public decimal VondautuRegister { get; set; }
        [DisplayName("Vốn đầu tư_thực hiện (vnđ)")]
        public decimal VondautuProcess { get; set; }
        
        [DisplayName("ĐV_Năm bắt đầu HĐ tại QTSC")]
        public DateTime? StartYear { get; set; }
        [DisplayName("ĐV_Năm kết thúc HĐ tại QTSC")]
        public string EndYear { get; set; }
        [DisplayName("ĐV_Tên giao dịch")]
        public string TransactionName { get; set; }
        [DisplayName("ĐV_Loại hình doanh nghiệp")]
        public string BusinessType { get; set; }
        [DisplayName("ĐV_Chi tiết đối tượng")]
        public string ObjectDetail { get; set; }
        [DisplayName("ĐV_Lĩnh vực hoạt động")]
        public string Carrer { get; set; }
        [DisplayName("ĐV_Hoạt động chính")]
        public string MainCarrer { get; set; }
        [DisplayName("ĐV_Sản phẩm tiêu biểu")]
        public string ProductHighlight { get; set; }
        [DisplayName("ĐV_Địa chỉ")]
        public string Address { get; set; }
        [DisplayName("ĐV_ĐV_Địa chỉ_Tỉnh/tp")]
        public string AddressProvince { get; set; }
        [DisplayName("ĐV_Thành viên Công viên Phần mềm Quang Trung")]
        public string MemberOfQuangTrungSoftware { get; set; }
        [DisplayName("ĐV_Cơ quan chủ quản")]
        public string Agency { get; set; }
        [DisplayName("ĐV_ĐC_Tòa nhà")]
        public string AddressBuilding { get; set; }
        [DisplayName("ĐV_ĐC_Tầng")]
        public string AddressFloor { get; set; }
        [DisplayName("ĐV_ĐC_Phòng")]
        public string AddressRoom { get; set; }
        public string TableTel { get; set; }
        [DisplayName("ĐV_TEL")]
        public string Tel { get; set; }
        [DisplayName("ĐV_FAX")]
        public string Fax { get; set; }
        [DisplayName("ĐV_EMAIL")]
        public string Email { get; set; }
        [DisplayName("ĐV_WEBSITE")]
        public string Website { get; set; }
        [DisplayName("ĐV_Giấy CNĐT số")]
        public string NumberOfInvestmentCertificate { get; set; }
        [DisplayName("ĐV_Ngày cấp Giấy CNĐT")]
        public DateTime? DateOfIssuingInvestmentCertificate { get; set; }
        [DisplayName("ĐV_Lần thay đổi Giấy CNĐT")]
        public string TimeOfChangeInvestmentCertificates { get; set; }
        [DisplayName("ĐV_Giấy phép ĐKKD số")]
        public string NumberOfBusinessLicense { get; set; }
        [DisplayName("ĐV_Ngày cấp Giấy phép ĐKKD")]
        public DateTime? DateOfIssuingBusinessLicense { get; set; }
        [DisplayName("ĐV_Lần thay đổi Giấy phép ĐKKD")]
        public string TimeOfChangeBusinessLicense { get; set; }
        [DisplayName("ĐV_Mã số thuế")]
        public string TaxCode { get; set; }
        [DisplayName("ĐV_Ngày hoạt động/thành lập")]
        public DateTime? ActiveDay { get; set; }
        [DisplayName("ĐV_SỐ HĐ")]
        public string NumberOfActivities { get; set; }
        [DisplayName("ĐV_Ngày ký HĐ")]
        public DateTime? SignDayActivities { get; set; }
        [DisplayName("ĐV_Ngày hết hạn HĐ")]
        public DateTime? ExpirationDateActivities { get; set; }
        [DisplayName("Ghi chú")]
        public string Note { get; set; }
        public string ProfilePicture { get; set; }

        [DisplayName("ĐV_Phân loại đối tượng")]
        public object ObjectType { get; set; }
        [DisplayName("ĐV_Phân loại thị trường")]
        public object MarketType { get; set; }
        [DisplayName("ĐV_Phân loại doanh nghiệp")]
        public object CompanyType { get; set; }
        [DisplayName("ĐV_Thị trường hoạt động")]
        public object MarketActive { get; set; }

        public OwnerCML OwnerCML { get; set; }
        public AmountCML AmountCML { get; set; }
        public PersonnelCML PersonnelCML { get; set; }
        public DeputyCML DeputyCML { get; set; }
    }

    public class CustomerUM
    {
        public Guid Id { get; set; }
        [DisplayName("ĐV_Tên")]
        public string Name { get; set; }
        [DisplayName("ĐV_Tên viết tắt")]
        public string ShortName { get; set; }
        [DisplayName("ĐV_Quốc tịch")]
        public string Country { get; set; }
        [DisplayName("ĐV_Phân loại quốc tịch")]
        public string CountryType { get; set; }
        [DisplayName("ĐV_Vốn điều lệ (vnđ)")]
        public decimal VondieuleVND { get; set; }
        [DisplayName("ĐV_Vốn điều lệ (usd)")]
        public decimal VondieuleUSD { get; set; }
        [DisplayName("Vốn đầu tư_đăng ký (vnđ)")]
        public decimal VondautuRegister { get; set; }
        [DisplayName("Vốn đầu tư_thực hiện (vnđ)")]
        public decimal VondautuProcess { get; set; }
        
        [DisplayName("ĐV_Năm bắt đầu HĐ tại QTSC")]
        public DateTime? StartYear { get; set; }
        [DisplayName("ĐV_Năm kết thúc HĐ tại QTSC")]
        public string EndYear { get; set; }
        [DisplayName("ĐV_Tên giao dịch")]
        public string TransactionName { get; set; }
        [DisplayName("ĐV_Loại hình doanh nghiệp")]
        public string BusinessType { get; set; }
        
        [DisplayName("ĐV_Chi tiết đối tượng")]
        public string ObjectDetail { get; set; }
        [DisplayName("ĐV_Lĩnh vực hoạt động")]
        public string Carrer { get; set; }
        [DisplayName("ĐV_Hoạt động chính")]
        public string MainCarrer { get; set; }
        [DisplayName("ĐV_Sản phẩm tiêu biểu")]
        public string ProductHighlight { get; set; }
        [DisplayName("ĐV_Địa chỉ")]
        public string Address { get; set; }
        [DisplayName("ĐV_ĐV_Địa chỉ_Tỉnh/tp")]
        public string AddressProvince { get; set; }
        [DisplayName("ĐV_Thành viên Công viên Phần mềm Quang Trung")]
        public string MemberOfQuangTrungSoftware { get; set; }
        [DisplayName("ĐV_Cơ quan chủ quản")]
        public string Agency { get; set; }
        [DisplayName("ĐV_ĐC_Tòa nhà")]
        public string AddressBuilding { get; set; }
        [DisplayName("ĐV_ĐC_Tầng")]
        public string AddressFloor { get; set; }
        [DisplayName("ĐV_ĐC_Phòng")]
        public string AddressRoom { get; set; }
        public string TableTel { get; set; }
        [DisplayName("ĐV_TEL")]
        public string Tel { get; set; }
        [DisplayName("ĐV_FAX")]
        public string Fax { get; set; }
        [DisplayName("ĐV_EMAIL")]
        public string Email { get; set; }
        [DisplayName("ĐV_WEBSITE")]
        public string Website { get; set; }
        [DisplayName("ĐV_Giấy CNĐT số")]
        public string NumberOfInvestmentCertificate { get; set; }
        [DisplayName("ĐV_Ngày cấp Giấy CNĐT")]
        public DateTime? DateOfIssuingInvestmentCertificate { get; set; }
        [DisplayName("ĐV_Lần thay đổi Giấy CNĐT")]
        public string TimeOfChangeInvestmentCertificates { get; set; }
        [DisplayName("ĐV_Giấy phép ĐKKD số")]
        public string NumberOfBusinessLicense { get; set; }
        [DisplayName("ĐV_Ngày cấp Giấy phép ĐKKD")]
        public DateTime? DateOfIssuingBusinessLicense { get; set; }
        [DisplayName("ĐV_Lần thay đổi Giấy phép ĐKKD")]
        public string TimeOfChangeBusinessLicense { get; set; }
        [DisplayName("ĐV_Mã số thuế")]
        public string TaxCode { get; set; }
        [DisplayName("ĐV_Ngày hoạt động/thành lập")]
        public DateTime? ActiveDay { get; set; }
        [DisplayName("ĐV_SỐ HĐ")]
        public string NumberOfActivities { get; set; }
        [DisplayName("ĐV_Ngày ký HĐ")]
        public DateTime? SignDayActivities { get; set; }
        [DisplayName("ĐV_Ngày hết hạn HĐ")]
        public DateTime? ExpirationDateActivities { get; set; }
        [DisplayName("Ghi chú")]
        public string Note { get; set; }
        public string ProfilePicture { get; set; }

        [DisplayName("ĐV_Phân loại đối tượng")]
        public object ObjectType { get; set; }
        [DisplayName("ĐV_Phân loại thị trường")]
        public object MarketType { get; set; }
        [DisplayName("ĐV_Phân loại doanh nghiệp")]
        public object CompanyType { get; set; }
        [DisplayName("ĐV_Thị trường hoạt động")]
        public object MarketActive { get; set; }

        public Guid? CustomerTypeId { get; set; }
        public string IdentityCardId { get; set; }
        public OwnerUM OwnerUM { get; set; }
        public AmountUM AmountUM { get; set; }
        public PersonnelUM PersonnelUM { get; set; }
        public DeputyUM DeputyUM { get; set; }

    }
    

    public class CustomerCM
    {
        [DisplayName("Tên Doanh Nghiệp")]
        public string Name { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Địa Chỉ")]
        public string Address { get; set; }
        [DisplayName("Quốc Tịch")]
        public string Country { get; set; }
        [DisplayName("Số điện thoại")]
        public string Tel { get; set; }
        [DisplayName("Fax")]
        public string Fax { get; set; }
        [DisplayName("Mã Số Thuế")]
        public string TaxCode { get; set; }
        [DisplayName("ĐV_Giấy phép ĐKKD số")]
        public string NumberOfBusinessLicense { get; set; }
        public OwnerCM OwnerUM { get; set; }
    }
    public class CustomerUpdateProfilePictureViewModel
    {
        public Guid Id { get; set; }
        public string ProfilePicture { get; set; }
    }

	public class CountMarketVM
	{
		public string MarketType { get; set; }
		public int Total { get; set; }
	}
}
