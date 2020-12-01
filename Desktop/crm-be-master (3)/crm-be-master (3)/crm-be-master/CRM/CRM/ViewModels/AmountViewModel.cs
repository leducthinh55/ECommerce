using System;
using System.ComponentModel;

namespace CRM.ViewModels
{
    public class AmountVM
    {
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
    }

    public class AmountCML
    {
        public decimal TotalVndRevenue { get; set; }
        public decimal TotalUsdRevenue { get; set; }
        public decimal TotalVndRevenueOutside { get; set; }
        public decimal TotalVndRevenueInside { get; set; }
        public decimal TotalUsdRevenueOutside { get; set; }
        public decimal TotalUsdRevenueInside { get; set; }

        #region NỘI ĐỊA
        public decimal TotalDomesticVndRevenue { get; set; }
        public decimal TotalDomesticUsdRevenue { get; set; }
        public decimal TotalDomesticVndRevenueOutside { get; set; }
        public decimal TotalDomesticVndRevenueInside { get; set; }
        public decimal TotalDomesticUsdRevenueOutside { get; set; }
        public decimal TotalDomesticUsdRevenueInside { get; set; }
        #endregion

        #region XUẤT KHẨU
        public decimal TotalExportVndRevenue { get; set; }
        public decimal TotalExportUsdRevenue { get; set; }
        public decimal TotalExportVndRevenueOutside { get; set; }
        public decimal TotalExportVndRevenueInside { get; set; }
        public decimal TotalExportUsdRevenueOutside { get; set; }
        public decimal TotalExportUsdRevenueInside { get; set; }
        #endregion

        public decimal RatioOfExportRevenue { get; set; }
    }

    public class AmountUM : AmountVM
    {

    }
}
