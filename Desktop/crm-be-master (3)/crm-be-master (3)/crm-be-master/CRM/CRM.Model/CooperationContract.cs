using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class CooperationContract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Code { get; set; }
        public Guid ParnerId { get; set; }
        public DateTime DateSinged { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        [ForeignKey("ParnerId")]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CoContractTelService> ServiceCMs { get; set; }
        public virtual ICollection<SubCoContract>  SubCoContracts { get; set; }
    }
}
