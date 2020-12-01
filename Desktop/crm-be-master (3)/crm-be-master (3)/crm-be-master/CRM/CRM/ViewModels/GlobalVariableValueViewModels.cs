using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class GlobalVariableValueCM
    {
        public Guid GlobalVariableId { get; set; }
        public Guid CustomerWorkflowId { get; set; }
        public string Value { get; set; }
    }

    public class GlobalVariableValueVM
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

    }
}
