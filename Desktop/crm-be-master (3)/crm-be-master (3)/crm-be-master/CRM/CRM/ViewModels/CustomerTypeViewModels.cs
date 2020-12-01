using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class CustomerTypeCM
    {
        public string Name { get; set; }
    }
    public class CustomerTypeVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    //public class CustomerTypeDashboard
    //{
    //    public Guid Id { get; set; }
    //    public string Name { get; set; }
    //    public int Total { get; set; }
    //}
    public class MarketTypeDashboard
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public int Total { get; set; }
    }
    public class CustomerTypeUM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
