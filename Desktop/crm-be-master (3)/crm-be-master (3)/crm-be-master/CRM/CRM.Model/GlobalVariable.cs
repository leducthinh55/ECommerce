using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class GlobalVariable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        [ForeignKey("WorkflowId")]
        public virtual HsWorkFlow WorkFlow { get; set; }
    }
}
