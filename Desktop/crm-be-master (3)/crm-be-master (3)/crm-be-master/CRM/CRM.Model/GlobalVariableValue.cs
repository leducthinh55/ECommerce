using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class GlobalVariableValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid GlobalVariableId { get; set; }
        public Guid CustomerWorkflowId { get; set; }
        public string Value { get; set; }
        public bool IsObject { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("CustomerWorkflowId")]
        public virtual CustomerWorkFlow  CustomerWorkFlow { get; set; }
    }
}
