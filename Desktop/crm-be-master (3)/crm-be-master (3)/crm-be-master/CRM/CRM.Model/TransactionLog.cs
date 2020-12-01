using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    public class TransactionLog
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; }
        public Guid EntityId { get; set; }
        public DateTime DateChanged { get; set; }
        public string FunctionType { get; set; }
        public string ByUser { get; set; }
        public virtual ICollection<ChangeLog> ChangeLogs { get; set; }

    }
}
