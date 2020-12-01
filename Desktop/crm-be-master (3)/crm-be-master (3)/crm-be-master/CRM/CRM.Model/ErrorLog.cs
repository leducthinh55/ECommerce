using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Model
{
    public class ErrorLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Location { get; set; }
        public string Error { get; set; }
        public DateTime When { get; set; }
    }

    public class CustomerError
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Country { get; set; }
    }
}
