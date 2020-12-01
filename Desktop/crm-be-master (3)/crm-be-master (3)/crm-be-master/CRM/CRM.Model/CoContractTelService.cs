using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class CoContractTelService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ContractId { get; set; }
        public Guid ServiceId { get; set; }
        public int Percentage { get; set; }
        public string AppendixLink { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Note { get; set; }
        public bool IsClosed { get; set; }

        [ForeignKey("ContractId")]
        public virtual CooperationContract CooperationContract { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Telecomservice Telecomservice { get; set; }
    }
}
