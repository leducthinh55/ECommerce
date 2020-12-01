using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class AccessDashboardVM
    {
        public bool BusinessAccess { get; set; }
        public bool MarketAccess { get; set; }
        public bool TelecomAccess { get; set; }
    }

    public class ServiceRevenue
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class TotalInvestorsCustomerVM
    {
        public string Year { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDomestic { get; set; }
        public decimal TotalInternational { get; set; }
    }

    public class TotalSoftwareCustomerVM
    {
        public string Year { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDomestic { get; set; }
        public decimal TotalInternational { get; set; }
    }

    public class TotalInsideOutsideSoftwareVM
    {
        public string Year { get; set; }
        public decimal Total { get; set; }
        public decimal TotalInside { get; set; }
        public decimal TotalOutside { get; set; }
    }

    public class TotalPeopleVM
    {
        public string Year { get; set; }
        public decimal Total { get; set; }
        public decimal TotalEmployee { get; set; }
        public decimal TotalAlumnus { get; set; }
    }

    public class TopTotalVndRevenueVM
    {
        public TopTotalVndRevenueVM()
        {
            TotalVndRevenueVMs = new List<TotalVndRevenueVM>();
        }
        public List<TotalVndRevenueVM> TotalVndRevenueVMs;
        public int NumberOfCustomer { get; set; }
    }

    public class TotalVndRevenueVM
    {
        public string Name { get; set; }
        public decimal TotalVndRevenue { get; set; }
    }

    public class CustomerContractDashboardVM
    {
        public string Name { get; set; }
        public string ContractNo { get; set; }
        public string ContractTelecomNo { get; set; }
        public DateTime StartYear { get; set; }
    }

    public class ContractDashboardVM
    {
        public string Name { get; set; }
        public string ContractNo { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class BuildingInventoryVM
    {
        public string Name { get; set; }
        public double UsedSquare { get; set; }
        public double UnusedSquare { get; set; }
    }

    public class CompanyStatistics
    {
        public CompanyStatistics(string country, decimal total)
        {
            Country = country;
            Total = total;
        }
        public string Country { get; set; }
        public decimal Total { get; set; }
    }

    #region P.VT
    public class CommonTelecomVM : TelecomserviceRevenueVM
    {
        public CommonTelecomVM()
        {
            Services = new List<TelecomserviceRevenueVM>();
        }
        public List<TelecomserviceRevenueVM> Services { get; set; }
    }
    public class TelecomserviceRevenueVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal ImplementRevenue { get; set; }
        public decimal RevenueRegister { get; set; }
    }

    public class CompletionRateTelecomVM
    {
        public string TelecomServiceName { get; set; }
        public decimal RevenueRegister { get; set; }
        public decimal ImplementedRevenue { get; set; }
        public string CompletionRate { get; set; }
    }
    public class TotalContractTelecomRevenueVM
    {
        public string Year { get; set; }
        public decimal Total { get; set; }
    }
    #endregion


}
