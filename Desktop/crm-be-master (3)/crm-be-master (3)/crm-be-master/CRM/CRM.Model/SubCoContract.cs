using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class SubCoContract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Code { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CooperationContractId { get; set; }
        public int Type { get; set; }
        public decimal Total { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        [ForeignKey("CooperationContractId")]
        public virtual CooperationContract CooperationContract { get; set; }
        public virtual ICollection<SubCoContractServiceItem> SubServices { get; set; }
    }
}
