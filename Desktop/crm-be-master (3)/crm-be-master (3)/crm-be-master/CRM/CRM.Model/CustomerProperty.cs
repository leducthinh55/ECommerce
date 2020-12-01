using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class CustomerProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PredefinedValue { get; set; }
        public Guid ReadPermission { get; set; }
        public Guid WritePermission { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<CustomerValue> CustomerValues { get; set; }
    }
}
