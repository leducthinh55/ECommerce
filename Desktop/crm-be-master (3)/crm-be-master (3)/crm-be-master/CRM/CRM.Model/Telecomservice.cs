using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class Telecomservice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public decimal RevenueRegister { get; set; }
        public bool WillDelete { get; set; }

        public Guid? CommonTelecomserviceId { get; set; }
        public virtual CommonTelecomservice CommonTelecomservice { get; set; }
        public virtual ICollection<TelecomserviceParameter> TelecomserviceParameters { get; set; }
        public virtual ICollection<TelecomserviceContractAppendix> TelecomserviceContractAppendices { get; set; }
        public virtual ICollection<CoContractTelService> CoContractTelServices { get; set; }
    }
}
