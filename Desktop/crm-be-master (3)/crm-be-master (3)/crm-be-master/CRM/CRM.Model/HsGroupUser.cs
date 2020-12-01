using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class HsGroupUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid GroupId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual HsUser User { get; set; }
        [ForeignKey("GroupId")]
        public virtual HsGroup Group { get; set; }
    }
}
