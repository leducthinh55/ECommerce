using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class FormGroup :ProtectPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public String Data { get; set; }

        public Guid FormId { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("FormId")]
        public virtual Form Form { get; set; }
    }

}
