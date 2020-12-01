using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;

namespace CRM.ViewModels
{
    public class TransactionLogViewModel
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; }
        public Guid EntityId { get; set; }
        public DateTime DateChanged { get; set; }
        public string FunctionType { get; set; }
        public string ByUser { get; set; }
        public virtual ICollection<ChangeLogViewModel> ChangeLogs { get; set; }
    }

}
