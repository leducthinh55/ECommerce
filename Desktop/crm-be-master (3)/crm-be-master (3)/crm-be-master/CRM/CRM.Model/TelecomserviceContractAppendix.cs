using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class TelecomserviceContractAppendix
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ContractAppendixId { get; set; }
        public Guid TelecomserviceId { get; set; }
        public string Data { get; set; }
        public decimal UnitAmount { get; set; }
        public int Status { get; set; }
        public DateTime? DateEnd { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("ContractAppendixId")]
        public virtual ContractTelecomAppendix ContractTelecomAppendix { get; set; }
        [ForeignKey("TelecomserviceId")]
        public virtual Telecomservice Telecomservice { get; set; }
    }
}
