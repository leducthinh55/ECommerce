using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Model
{
    public class HsPermissionOfRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }

        [ForeignKey("RoleId")]
        public virtual HsRole Role { get; set; }
        [ForeignKey("PermissionId")]
        public virtual HsPermission Permission { get; set; }
    }
}
