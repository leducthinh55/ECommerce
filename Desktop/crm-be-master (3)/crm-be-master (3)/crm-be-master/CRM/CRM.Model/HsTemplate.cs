using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class HsTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid? InstanceId { get; set; }
        public DateTime Date { get; set; }
        public Guid? FormId { get; set; }
        [ForeignKey("InstanceId")]
        public virtual HsWorkFlowInstance WorkFlowInstance { get; set; }
    }
}
