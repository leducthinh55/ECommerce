using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class ChangeLog
    { 
        public Guid Id { get; set; }
        public string PropertyName { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        public Guid TransactionLogId { get; set; }

        [ForeignKey("TransactionLogId")]
        public virtual TransactionLog TransactionLog { get; set; }

    }
}
