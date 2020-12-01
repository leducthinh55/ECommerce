using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class ProductAttributeVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public String Value { get; set; }
    }

    public class ProductAttributeCM
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ProductAttributeUM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
