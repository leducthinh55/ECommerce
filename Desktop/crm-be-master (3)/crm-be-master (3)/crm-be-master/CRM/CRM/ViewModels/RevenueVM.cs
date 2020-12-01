using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class RevenueVM
    {
        public RevenueVM()
        {
            MonthDatas = new MonthData[12];
        }
        public string CustomerCode { get; set; }
        public string CompanyName { get; set; }
        public string Room { get; set; }
        public string Floor { get; set; }
        public string Building { get; set; }
        public string ContractNo { get; set; }
        public DateTime? StartDateRent { get; set; }
        public double CurrentSquare { get; set; }
        public double Square { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitServicePrice { get; set; }
        public MonthData[] MonthDatas { get; set; }
        public decimal RevenueSeason1 { get; set; }
        public decimal RevenueSeason2 { get; set; }
        public decimal RevenueSeason3 { get; set; }
        public decimal RevenueSeason4 { get; set; }
        public decimal RevenueYear { get; set; }

    }

    public class MonthData
    {
        public decimal TotalRevenue { get; set; }
        public decimal Increase { get; set; }
    }

    public class RevenueTelecomVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal RevenueRegister { get; set; }
        public decimal RevenueReal { get; set; }
        public float RateDone { get; set; }
    }

    public class RevenueVTVM
    {
        public RevenueVTVM()
        {
        }
        public string CustomerCode { get; set; }
        public string CompanyName { get; set; }
        public Guid ContractId { get; set; }
        public string ContractNo { get; set; }
        public DateTime DateSigned { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string LocationBuild { get; set; }

        public decimal[] MonthDatas { get; set; }
        public decimal RevenueSeason1 { get; set; }
        public decimal RevenueSeason2 { get; set; }
        public decimal RevenueSeason3 { get; set; }
        public decimal RevenueSeason4 { get; set; }
        public decimal RevenueYear { get; set; }

    }


    public class RevenueVTVMDetail
    {
        public RevenueVTVMDetail()
        {
            Services = new List<ServiceRevenueVM>();
        }
        public string CustomerCode { get; set; }
        public string CompanyName { get; set; }
        public string ContractNo { get; set; }
        public DateTime DateSigned { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string LocationBuild { get; set; }

        public List<ServiceRevenueVM> Services { get; set; }

    }

    public class ServiceRevenueVM
    {
        public String AppendixCode { get; set; }
        public DateTime? DateAccept { get; set; }
        public DateTime? DateEnd { get; set; }
        public Guid ServiceId { get; set; }
        public String ServiceName { get; set; }
        public object Data { get; set; }
        public decimal UnitAmount { get; set; }
        public int Quantity { get; set; }

        public decimal[] MonthDatas { get; set; }
        public decimal RevenueSeason1 { get; set; }
        public decimal RevenueSeason2 { get; set; }
        public decimal RevenueSeason3 { get; set; }
        public decimal RevenueSeason4 { get; set; }
        public decimal RevenueYear { get; set; }
    }
}
