using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class HsRoleOfGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid GroupId { get; set; }

        [ForeignKey("RoleId")]
        public virtual HsRole Role { get; set; }
        [ForeignKey("GroupId")]
        public virtual HsGroup Group { get; set; }
    }
}
