using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM_HsTemplate.Models
{
    public class HsTemplate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid? InstanceId { get; set; }
        public DateTime Date { get; set; }
    }
}