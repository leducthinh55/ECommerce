using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class EventLogFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid EventLogId { get; set; }

        public bool IsDeleted { get; set; }


        [ForeignKey("EventLogId")]
        public virtual EventLog EventLog { get; set; }

    }
}
