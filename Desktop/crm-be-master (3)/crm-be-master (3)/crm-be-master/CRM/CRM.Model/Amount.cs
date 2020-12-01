using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Model
{
    public class Amount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DisplayName("Tổng doanh thu(vnđ))")]
        public decimal TotalVndRevenue { get; set; }
        [DisplayName("Tổng doanh thu(usd)")]
        public decimal TotalUsdRevenue { get; set; }
        [DisplayName("Bên ngoài Công viên phần mềm Quang Trung(vnđ)")]
        public decimal TotalVndRevenueOutside { get; set; }
        [DisplayName("Nội khu Công viên phần mềm Quang Trung(vnđ)")]
        public decimal TotalVndRevenueInside { get; set; }
        [DisplayName("Bên ngoài Công viên phần mềm Quang Trung(usd)")]
        public decimal TotalUsdRevenueOutside { get; set; }
        [DisplayName("Nội khu Công viên phần mềm Quang Trung(usd)")]
        public decimal TotalUsdRevenueInside { get; set; }

        #region NỘI ĐỊA
        [DisplayName("Tổng doanh thu nội địa(vnđ)")]
        public decimal TotalDomesticVndRevenue { get; set; }
        [DisplayName("Tổng doanh thu nội địa(usd)")]
        public decimal TotalDomesticUsdRevenue { get; set; }
        [DisplayName("Bên ngoài Công viên phần mềm Quang Trung(vnđ)")]
        public decimal TotalDomesticVndRevenueOutside { get; set; }
        [DisplayName("Nội khu Công viên phần mềm Quang Trung(vnđ)")]
        public decimal TotalDomesticVndRevenueInside { get; set; }
        [DisplayName("Bên ngoài Công viên phần mềm Quang Trung(usd)")]
        public decimal TotalDomesticUsdRevenueOutside { get; set; }
        [DisplayName("Nội khu Công viên phần mềm Quang Trung(usd)")]
        public decimal TotalDomesticUsdRevenueInside { get; set; }
        #endregion

        #region XUẤT KHẨU
        [DisplayName("Tổng doanh thu xuất khẩu(vnđ)")]
        public decimal TotalExportVndRevenue { get; set; }
        [DisplayName("Tổng doanh thu xuất khẩu(usd)")]
        public decimal TotalExportUsdRevenue { get; set; }
        [DisplayName("Bên ngoài Công viên phần mềm Quang Trung(vnđ)")]
        public decimal TotalExportVndRevenueOutside { get; set; }
        [DisplayName("Nội khu Công viên phần mềm Quang Trung(vnđ)")]
        public decimal TotalExportVndRevenueInside { get; set; }
        [DisplayName("Bên ngoài Công viên phần mềm Quang Trung(usd)")]
        public decimal TotalExportUsdRevenueOutside { get; set; }
        [DisplayName("Nội khu Công viên phần mềm Quang Trung(usd)")]
        public decimal TotalExportUsdRevenueInside { get; set; }
        #endregion

        [DisplayName("Tỷ lệ doanh thu xuất khẩu trên tổng doanh thu")]
        public decimal RatioOfExportRevenue { get; set; }

        //1-1
        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
