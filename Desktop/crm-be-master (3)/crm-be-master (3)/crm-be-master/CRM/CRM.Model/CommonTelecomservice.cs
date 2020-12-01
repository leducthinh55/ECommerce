using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Model
{
    public enum CommonTelecomserviceType
    {
        Telecom,
        Cooperation
    }
    public class CommonTelecomservice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public int Type { get; set; }
        public virtual ICollection<Telecomservice> Telecomservices { get; set; }
    }
}
