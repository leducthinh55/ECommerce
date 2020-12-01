using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class HsWorkFlowInstance : ProtectPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid WorkFlowId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public bool IsDeleted { get; set; }

        public Guid? FormId { get; set; }
        public Guid? PermissionId { get; set; }

        //Notification
        public Guid? PermissionIdNoti { get; set; }

        [ForeignKey("WorkFlowId")]
        public virtual HsWorkFlow WorkFlow { get; set; }
        public virtual ICollection<HsWorkFlowConnection> FromInstances { get; set; }
        public virtual ICollection<HsWorkFlowConnection> ToInstances { get; set; }
        public virtual ICollection<HsTemplate> Templates { get; set; }
    }
}
