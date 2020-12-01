using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class DashBoardChart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime Time { get; set; }

        public decimal VondautuSoftwares { get; set; }

        public decimal VondautuInvestors { get; set; }
        
        // Total SoftWares
        public long TotalSoftWare{ get; set; }

        public long TotalDomestic { get; set; }

        public long TotalInternational { get; set; }
        // End Total SoftWare

        // Total Peoples
        public long TotalPeoples { get; set; }

        public long TotalEmployee { get; set; }

        public long TotalAlumnus { get; set; }
        // End Total Peoples
    }
}
