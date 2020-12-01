using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class WorkFlowHistoryFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid? WorkFlowHistoryId { get; set; }
        public DateTime Date { get; set; }
        public bool IsTemplate { get; set; }
        [ForeignKey("WorkFlowHistoryId")]
        public virtual WorkFlowHistory WorkFlowHistory { get; set; }
    }
}
