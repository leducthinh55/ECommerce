using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class GlobalVariableCM
    {
        public Guid WorkflowId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class GlobalVariableVM
    {
        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
