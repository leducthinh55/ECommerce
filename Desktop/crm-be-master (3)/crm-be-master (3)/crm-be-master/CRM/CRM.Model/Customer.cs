using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class Customer
    {
        public Customer()
        {
            Country = "";
            CountryType = "";
            BusinessType = "";
            DateOfIssuingInvestmentCertificate = DateTime.Now;
            DateOfIssuingBusinessLicense = DateTime.Now;
            ActiveDay = DateTime.Now;
            ExpirationDateActivities = DateTime.Now;
            //SignDayActivities = DateTime.Now;
            Country = "";
            CountryType = "";
            BusinessType = "";
            MarketType = "";
            //if (Owner == null) Owner = new Owner();
            //if (Personnel == null) Personnel = new Personnel();
            //if (Amount == null) Amount = new Amount();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Id")]
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Mã số")]
        public int No { get; set; } 
        [DisplayName("Mã Khách Hàng")]
        public string Code { get; set; } 
        [DisplayName("ĐV_Tên")]
        public string Name { get; set; }
        [DisplayName("ĐV_Tên viết tắt")]
        public string ShortName { get; set; }
        [DisplayName("ĐV_Quốc tịch")]
        public string Country { get; set; }
        /// <summary>
        /// Quốc tịch Việt Nam thì lựa chọn trong nước
        /// </summary>
        [DisplayName("ĐV_Phân loại quốc tịch")]
        public string CountryType { get; set; }

        /// <summary>
        /// Nhập đúng theo GP ĐKKD, dùng để tính tổng vốn điều lệ. 
        /// </summary>
        [DisplayName("ĐV_Vốn điều lệ (vnđ)")]
        public decimal VondieuleVND { get; set; }
        /// <summary>
        /// Nhập đúng theo GP ĐKKD, dùng để tính tổng vốn điều lệ
        /// </summary>
        [DisplayName("ĐV_Vốn điều lệ (usd)")]
        public decimal VondieuleUSD { get; set; }
        /// <summary>
        /// Nhập đúng theo GP đăng ký đầu tư dự án, dùng để tính vốn đăng ký đầu tư
        /// </summary>
        [DisplayName("Vốn đầu tư_đăng ký (vnđ)")]
        public decimal VondautuRegister { get; set; }
        /// <summary>
        /// Dùng để tính vốn thực hiện và so sánh với vốn đầu tư đăng ký
        /// </summary>
        [DisplayName("Vốn đầu tư_thực hiện (vnđ)")]
        public decimal VondautuProcess { get; set; }
        /// <summary>
        /// Phân loại thị trường hoạt động của đơn vị theo châu lục
        /// </summary>
        [DisplayName("ĐV_Phân loại thị trường")]
        public string MarketType { get; set; }

        /// <summary>
        /// Thị trường hoạt động của đơn vị
        /// </summary>
        [DisplayName("ĐV_Thị trường hoạt động")]
        public string MarketActive { get; set; }
        [DisplayName("ĐV_Năm bắt đầu HĐ tại QTSC")]
        public DateTime? StartYear { get; set; }
        [DisplayName("ĐV_Năm kết thúc HĐ tại QTSC")]
        public string EndYear { get; set; }
        [DisplayName("ĐV_Tên giao dịch")]
        public string TransactionName { get; set; }
        /// <summary>
        /// - Thêm chú thích: Giống phân loại đối tượng nhưng căn cứ theo luật doanh nghiệp
        /// - List: Công ty cổ phần; Công ty TNHH; công ty hợp danh; doanh nghiệp tư nhân
        /// </summary>
        [DisplayName("ĐV_Loại hình doanh nghiệp")]
        public string BusinessType { get; set; }
        /// <summary>
        /// Tiêu chí phân loại theo thực tế công việc 
        /// </summary>
        [DisplayName("ĐV_Phân loại doanh nghiệp")]
        public string CompanyType { get; set; }
        [DisplayName("ĐV_Phân loại đối tượng")]
        public string ObjectType { get; set; }
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

        /// <summary>
        /// Giống chủ sở hữu, dùng riêng cho P.CLTT
        /// </summary>
        [DisplayName("ĐV_Cơ quan chủ quản")]
        public string Agency { get; set; }
        [DisplayName("ĐV_ĐC_Tòa nhà")]
        public string AddressBuilding { get; set; }
        [DisplayName("ĐV_ĐC_Tầng")]
        public string AddressFloor { get; set; }
        [DisplayName("ĐV_ĐC_Phòng")]
        public string AddressRoom { get; set; }
        [DisplayName("ĐV_TEL")]
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
        //[DisplayName("ĐV_Ngày ký HĐ")]
        //public DateTime? SignDayActivities { get; set; }
        [DisplayName("ĐV_Ngày hết hạn HĐ")]
        public DateTime? ExpirationDateActivities { get; set; }
        [DisplayName("Ghi chú")]
        public string Note { get; set; }
        public string ProfilePicture { get; set; }

        //TODO: Delete when Test150Data success!
        public bool IsReal { get; set; }

        #region Relationship
        public Guid? CustomerTypeId { get; set; }
        public string IdentityCardId { get; set; }
        [ForeignKey("IdentityCardId")]
        public virtual IdentityCard IdentityCard { get; set; }
        public virtual ICollection<BankAccount> BankAccount { get; set; }
        public virtual ICollection<CustomerWorkFlow> CustomerWorkFlows { get; set; }
        [ForeignKey("CustomerTypeId")]
        public virtual CustomerType CustomerType { get; set; }

        public virtual ICollection<Contact> Contact { get; set; }
        public virtual ICollection<ContractTelecom> ContractTelecoms { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }

        public virtual ICollection<CooperationContract> CooperationContracts { get; set; }

        //1-1
        //public Guid OwnerId { get; set; }
        //[ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }

        //public Guid PersonnelId { get; set; }
        //[ForeignKey("PersonnelId")]
        public virtual Personnel Personnel { get; set; }

        //public Guid AmountId { get; set; }
        //[ForeignKey("AmountId")]
        public virtual Amount Amount { get; set; }

        //public Guid DeputyId { get; set; }
        //[ForeignKey("DeputyId")]
        public virtual Deputy Deputy { get; set; }
        #endregion
    }
}
