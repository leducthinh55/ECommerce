using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class ContractCM
    {
        public Guid TemplateId { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public Guid WorkFlowHistoryId { get; set; }
    }

    public class ContractVM
    {
        public Guid Id { get; set; }
        #region Thông tin kéo từ CUSTOMER qua
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string DeputyName { get; set; }
        public string DeputyPosition { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }

        #endregion
        public string ContractNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
        public double Square { get; set; }
        public double CurrentSquare { get; set; }
        public decimal CurrentUnitPrice { get; set; }
        public decimal CurrentUnitServicePrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitServicePrice { get; set; }
        public DateTime StartDateRent { get; set; }
        public DateTime? StartDateService { get; set; }
        public DateTime UpPriceDate { get; set; }
        public double LevelUpUnitPrice { get; set; }
        public double LevelUpUnitServicePrice { get; set; }
        public int Status { get; set; }
        //2
        public string Floor_2 { get; set; }
        public string Room_2 { get; set; }
        public double? Square_2 { get; set; }
        public decimal? UnitPrice_2 { get; set; }
        public decimal? UnitServicePrice_2 { get; set; }
        public DateTime? StartDateRent_2 { get; set; }
        public DateTime? StartDateService_2 { get; set; }
        public DateTime? UpPriceDate_2 { get; set; }
        public double? LevelUpUnitPrice_2 { get; set; }
        public double? LevelUpUnitServicePrice_2 { get; set; }
        //3
        public string Floor_3 { get; set; }
        public string Room_3 { get; set; }
        public double? Square_3 { get; set; }
        public decimal? UnitPrice_3 { get; set; }
        public decimal? UnitServicePrice_3 { get; set; }
        public DateTime? StartDateRent_3 { get; set; }
        public DateTime? StartDateService_3 { get; set; }
        public DateTime? UpPriceDate_3 { get; set; }
        public double? LevelUpUnitPrice_3 { get; set; }
        public double? LevelUpUnitServicePrice_3 { get; set; }
        //4
        public string Floor_4 { get; set; }
        public string Room_4 { get; set; }
        public double? Square_4 { get; set; }
        public decimal? UnitPrice_4 { get; set; }
        public decimal? UnitServicePrice_4 { get; set; }
        public DateTime? StartDateRent_4 { get; set; }
        public DateTime? StartDateService_4 { get; set; }
        public DateTime? UpPriceDate_4 { get; set; }
        public double? LevelUpUnitPrice_4 { get; set; }
        public double? LevelUpUnitServicePrice_4 { get; set; }

    }

    public class ContractUM
    {
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }
        public string CustomerWorkFlowCode { get; set; }
        public string ContractNo { get; set; }
        public DateTime ContractDate { get; set; }
        public string BEnterprise { get; set; }
        public string BBusinessRegistrationCertificate { get; set; }
        public string BAddress { get; set; }
        public string BEmail { get; set; }
        public string BTel { get; set; }
        public string BFax { get; set; }
        public string BName { get; set; }
        public string BRole { get; set; }
        public string BNumberBanking { get; set; }
        public string BBank { get; set; }
        public string BTaxNo { get; set; }
        public float Square { get; set; }
        public string Room { get; set; }
        public string Floor { get; set; }
        //Giá cả (VNĐ/m2/tháng ( chưa bao gồm thuế GTGT)
        public float Amount { get; set; }
        // Giá dịch vụ (VNĐ/m2/tháng ( chưa bao gồm thuế GTGT)
        public float ServiceAmount { get; set; }
        //Thời gian dự kiến giao văn phòng
        public DateTime DateRelease { get; set; }
        //Thời điểm áp dụng giá
        public DateTime PaymentTimeBegin { get; set; }
        //Thời điểm áp dụng giá dịch vụ
        public DateTime ServicePaymentTimeBegin { get; set; }
        //Hiệu lực hợp đồng(năm)
        public float ContractBegin { get; set; }
        public DateTime ContractDateBegin { get; set; }
        public DateTime ContractDateEnd { get; set; }
        //Thời gian xem xét điều chỉnh giá (năm)
        public float AmountYear { get; set; }
        public DateTime TimeBegin { get; set; }
        //Thời gian xem xét điều chỉnh giá dịch vụ (năm)
        public float ServiceAmountYear { get; set; }
        public DateTime ServiceTimeBegin { get; set; }
    }

    public class ContractSyncModel
    {
        public string CompanyName { get; set; }
        public string Contact { get; set; }
        public string Position { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
        public string Square { get; set; }
        public string UpSquare { get; set; }
        public string DownSquare { get; set; }
        public string UsingSquare { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string ContractNumber { get; set; }
        public string ContractDate { get; set; }
        public string UpPriceDate { get; set; }
        public string EndDate { get; set; }
        public string Amount { get; set; }
        public string ServiceAmount { get; set; }
        public string LevelUpPrice { get; set; }
        public string OtherPrice { get; set; }
        public string Note { get; set; }
        public List<ContractSyncModel> Appendices { get; set; }
    }
}
