using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class CareHistoryViewModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }

    public class CareHistoryCreateModel
    {
        public string Type { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
}
