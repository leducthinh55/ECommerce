using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class SubCoContractVM
    {
        public Guid Id { get; set; }
        public String Code { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CooperationContractId { get; set; }
        public int Type { get; set; }
        public decimal Total { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }

    public class SubCoContractCM
    {
        public String Code { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CooperationContractId { get; set; }
        public int Type { get; set; }
        public decimal Total { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public List<SubCoContractServiceItemCM> Services { get; set; }
    }
    public class SubCoContractServiceItemCM
    {
        public Guid CoContractTelServiceId { get; set; }
        public decimal Amount { get; set; }
    }
    
    public class SubCoContractServiceItemVM
    {
        public Guid Id { get; set; }
        public Guid CoContractTelServiceId { get; set; }
        public decimal Amount { get; set; }
    }

    public class SubCoContractVMDetail
    {
        public Guid Id { get; set; }
        public String Code { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CooperationContractId { get; set; }
        public int Type { get; set; }
        public decimal Total { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int Status { get; set; }
        public List<SubCoContractServiceItemVM> Services { get; set; }
    }

    public class SubCoContractUM
    {
        public Guid Id { get; set; }
        public String Code { get; set; }
        public Guid CustomerId { get; set; }
        public int Type { get; set; }
        public decimal Total { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int Status { get; set; }
        public List<SubCoContractServiceItemVM> Services { get; set; }
    }
}
