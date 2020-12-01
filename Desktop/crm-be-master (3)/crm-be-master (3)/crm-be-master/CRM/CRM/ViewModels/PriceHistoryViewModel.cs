using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class PriceHistoryCM
    {
        public double Price { get; set; }
        public DateTime FromDate { get; set; }
        public Guid ProductId { get; set; }
    }

    public class PriceHistoryVM 
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public DateTime FromDate { get; set; }
        public Guid ProductId { get; set; }
    }

    public class PriceHistoryUM
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public DateTime FromDate { get; set; }
    }
}
