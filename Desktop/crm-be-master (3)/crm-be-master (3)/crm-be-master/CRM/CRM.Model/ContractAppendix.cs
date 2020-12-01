using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class ContractAppendix
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String No { get; set; }
        [DisplayName("Toà nhà")]
        public string Building { get; set; }
        [DisplayName("Tầng")]
        public string Floor { get; set; }
        [DisplayName("Phòng cho thuê")]
        public string Room { get; set; }
        public DateTime DateSign { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public double? Square { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? UnitServicePrice { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public Guid ContractId { get; set; }
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; }
        public String Note { get; set; }
        public String Key { get; set; }
    }
}
