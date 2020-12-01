using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class HsWorkFlow : ProtectPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string Code { get; set; }
        public virtual ICollection<HsWorkFlowInstance> Instances { get; set; }
        public virtual ICollection<GlobalVariable> GlobalVariables { get; set; }
        public int Type { get; set; }

    }
}
