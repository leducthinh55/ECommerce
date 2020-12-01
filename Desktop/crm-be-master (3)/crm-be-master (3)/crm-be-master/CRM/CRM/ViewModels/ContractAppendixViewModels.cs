using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class ContractAppendixCM
    {
        public String No { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
        public DateTime DateSign { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public double Square { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? UnitServicePrice { get; set; }
        public int Type { get; set; }
        public Guid ContractId { get; set; }
        public String Note { get; set; }
        public String Key { get; set; }

    }

    public class ContractAppendixUM : ContractAppendixCM
    {
        public Guid Id { get; set; }

    }

    public class ContractAppendixVM
    {
        public Guid Id { get; set; }
        public String No { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
        public DateTime DateSign { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public double Square { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? UnitServicePrice { get; set; }
        public int Type { get; set; }
        public Guid ContractId { get; set; }
        public String Key { get; set; }
    }
}
