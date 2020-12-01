using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class Form : ProtectPermission
    {
        
        public Form()
        {
            Method = "Get";
            DateCreated = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Method { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsDeleted { get; set; }

        public string Formulas { get; set; }
        public string NumbertoWordFields { get; set; }
        public Guid? HsWorkflowId { get; set; }

        public virtual ICollection<FormGroup> FormGroups { get; set; }
    }
}
