using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class TelecomserviceParameter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public int Order { get; set; }
        public Guid TelecomServiceId { get; set; }
        [ForeignKey("TelecomServiceId")]
        public virtual Telecomservice Telecomservice { get; set; }
    }
}
