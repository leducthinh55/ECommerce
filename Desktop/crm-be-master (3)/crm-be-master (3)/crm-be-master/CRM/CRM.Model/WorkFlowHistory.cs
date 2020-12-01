using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class WorkFlowHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid CustomerWorkFlowId { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid InstanceId { get; set; }
        public string InstanceName { get; set; }
        public Guid? PreviousStep { get; set; }
        public int Status { get; set; } // 1 - in process; 2 - done 
        public string FormData { get; set; }
        public string Comment { get; set; }

        [ForeignKey("CustomerWorkFlowId")]
        public virtual CustomerWorkFlow CustomerWorkFlow { get; set; } 
        public virtual ICollection<WorkFlowHistoryFile> Files { get; set; }
        public virtual ICollection<EventLog> EventLogs { get; set; }
    }
}
