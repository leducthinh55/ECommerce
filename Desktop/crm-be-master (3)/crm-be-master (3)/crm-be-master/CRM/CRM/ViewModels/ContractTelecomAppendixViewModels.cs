using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.ViewModels
{
    public class ContractTelecomAppendixVM
    {
        public Guid Id { get; set; }
        public String Code { get; set; }
        public String ContractNo { get; set; }
        public int Status { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateAccept { get; set; }
        public DateTime? DateEnd { get; set; }
        public int Type { get; set; }
        public string Note { get; set; }
        public Guid ContractTelecomId { get; set; }
    }

    public class ContractTelecomAppendixDetailVM
    {
        public Guid Id { get; set; }
        public String Code { get; set; }
        public String ContractNo { get; set; }
        public int Status { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Guid ContractTelecomId { get; set; }
        public int Type { get; set; }
        public String Note { get; set; }
        public IList<TelecomserviceContractAppendixVM> Services { get; set; }
    }

    public class ContractTelecomAppendixCM
    {
        [Required]
        public String Code { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DateAccept { get; set; }
        public String Note { get; set; }
        public int Type { get; set; }
        public Guid ContractTelecomId { get; set; }
        public List<TelecomserviceContractAppendixCM> Services { get; set; }
    }
    public class ContractTelecomAppendixUM : ContractTelecomAppendixCM
    {
        public Guid Id { get; set; }
    }
    public class TelecomserviceContractAppendixCM
    {
     
        public Guid TelecomserviceId { get; set; }
        public object Data { get; set; }
        public decimal UnitAmount { get; set; }
        public DateTime? DateEnd { get; set; }
        public int Quantity { get; set; }
    }
    public class TelecomserviceContractAppendixVM
    {
        public Guid Id { get; set; }
        public Guid TelecomserviceId { get; set; }
        public object Data { get; set; }
        public int Quantity { get; set; }
        public decimal UnitAmount { get; set; }
        public String Note { get; set; }
        public DateTime? DateEnd { get; set; }
    }

    public class ContractTelecomAppxDateAccept
    {
        public DateTime DateAccept { get; set; }
    }
}
