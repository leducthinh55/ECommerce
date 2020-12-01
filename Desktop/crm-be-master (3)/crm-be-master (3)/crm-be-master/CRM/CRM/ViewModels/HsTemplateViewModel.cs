using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class HsTemplateVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime Date { get; set; }
    }

    public class HsTemplateFieldsVM
    {
        public Guid Id { get; set; }
        public List<string> Fields { get; set; }
    }
}
