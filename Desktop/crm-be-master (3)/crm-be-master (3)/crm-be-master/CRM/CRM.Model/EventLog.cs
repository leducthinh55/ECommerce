using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class EventLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string ActionType { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? ExecutedDate { get; set; }
        public string Description { get; set; }
        public Guid WorkFlowHistoryId { get; set; }
        public string CreatedBy { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("WorkFlowHistoryId")]
        public virtual WorkFlowHistory WorkFlowHistory { get; set; }

        public virtual ICollection<EventLogFile> Files { get; set; }
    }
}
