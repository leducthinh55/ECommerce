using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class SubCoContractServiceItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid SubContractId { get; set; }
        public Guid CoContractTelServiceId { get; set; }
        public Guid ServiceId { get; set; }
        [ForeignKey("SubContractId")]
        public virtual SubCoContract SubCoContract { get; set; }
        public decimal Amount { get; set; }
        public String Data { get; set; }

    }
}
