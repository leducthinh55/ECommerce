using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class ContractTelecom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String ContractNo { get; set; }
        public int TypeInvestment { get; set; }
        public DateTime DateSigned { get; set; }
        public int Status { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string LocationBuild { get; set; }
        public string Note { get; set; }
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<ContractTelecomAppendix> ContractTelecomAppendices { get; set; }

        //public string Name { get; set; }
        //public string Phone { get; set; }
        //public string Email { get; set; }
        //public string Position { get; set; }

        public Guid? CustomerWorkflowId { get; set; }

    }
}
