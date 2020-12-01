using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Model
{
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [DisplayName("Số hợp đồng")]
        public string ContractNo { get; set; }
        [DisplayName("Ngày bắt đầu hợp đồng")]
        public DateTime? StartDate { get; set; }
        [DisplayName("Ngày kết thúc hợp đồng")]
        public DateTime? EndDate { get; set; }
        [DisplayName("Toà nhà")]
        public string Building { get; set; }
        [DisplayName("Tầng")]
        public string Floor { get; set; }
        [DisplayName("Phòng cho thuê")]
        public string Room { get; set; }
        [DisplayName("Diện tích cho thuê (m2)")]
        public double Square { get; set; }
        [DisplayName("Giá thuê 1 tháng")]
        public decimal UnitPrice { get; set; }
        [DisplayName("Giá thuê dịch vụ 1 tháng")]
        public decimal? UnitServicePrice { get; set; }
        [DisplayName("Ngày bắt đầu tính tiền thuê")]
        public DateTime StartDateRent { get; set; }
        [DisplayName("Ngày bắt đầu tính tiền dịch vụ")]
        public DateTime? StartDateService { get; set; }
        [DisplayName("Ngày tăng giá")]
        public DateTime UpPriceDate { get; set; }
        [DisplayName("Mức tăng giá thuê ")]
        public double LevelUpUnitPrice { get; set; }
        [DisplayName("Mức tăng giá dịch vụ ")]
        public double? LevelUpUnitServicePrice { get; set; }

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

        //**************
        public int Status { get; set; }

        public virtual ICollection<ContractAppendix> ContractAppendices { get; set; }

        public Guid? CustomerWorkflowId { get; set; }
        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }

    public class ContractQTSC_1
    {
        public int ContractId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string BusinessCertificate { get; set; }
        public string Representative { get; set; }
        public string Position { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RetalEndDate { get; set; }
        public double RentalPrice { get; set; }
        public double Area { get; set; }
        public double RentalTotal { get; set; }
        public double RentalVat { get; set; }
        public int DepositInNumber { get; set; }
        public string DepositInWord { get; set; }
        public string RentalTotalAfterVatInWord { get; set; }
        public double RentalTotalAfterVatInNumber { get; set; }
        public double ServicePrice { get; set; }
        public double ServiceTotal { get; set; }
        public double ServiceTotalAfterVatInNumber { get; set; }
        public string ServiceTotalAfterVatInWord { get; set; }
        public double ServiceVat { get; set; }
        public DateTime TimeToCharegeService { get; set; }
        public DateTime TimeToChargeRental { get; set; }
    }
}
