using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class PredefinedValueVM
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }

    public class PredefinedValueUM
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }

    public class PredefinedValueCM
    {
        public string Value { get; set; }
        public Guid ProductAttributeId { get; set; }
    }
}
