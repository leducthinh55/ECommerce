using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Label { get; set; }
        public String Detail { get; set; }
        public String RouterLink { get; set; }
        public String IconType { get; set; }
        public String IconName { get; set; }
        public Guid? ParentId { get; set; }
        public String GroupName { get; set; }
        [ForeignKey("ParentId")]
        public virtual Menu ParentMenu { get; set; }
    }
}
