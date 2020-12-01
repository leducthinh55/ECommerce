using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class ContractTelecomAppendix
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Code { get; set; }
        public int Status { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateAccept { get; set; }
        public DateTime? DateEnd { get; set; }

        public String Note { get; set; }
        public int Type { get; set; }


        public Guid ContractTelecomId { get; set; }
        [ForeignKey("ContractTelecomId")]
        public virtual ContractTelecom ContractTelecom { get; set; }
        public virtual ICollection<TelecomserviceContractAppendix> TelecomserviceContractAppendices { get; set; }
    }
}
