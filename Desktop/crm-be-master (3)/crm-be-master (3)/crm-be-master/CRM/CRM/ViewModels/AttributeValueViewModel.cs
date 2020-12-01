using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class AttributeValueViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductAttributeId { get; set; }

        public String Value { get; set; }
        public Guid? PredefinedId { get; set; }
    }

    public class AttributeValueDM
    {
        public Guid ProductId { get; set; }
        public Guid ProductAttributeId { get; set; }
    }

    public class AttributeValueUM
    {
        public Guid Id { get; set; }

        public String Value { get; set; }
        public Guid? PredefinedId { get; set; }
    }

    public class AttributeValueCreateModel
    {
        public Guid ProductId { get; set; }
        public Guid ProductAttributeId { get; set; }

        public String Value { get; set; }
        public Guid? PredefinedId { get; set; }
    }
}
