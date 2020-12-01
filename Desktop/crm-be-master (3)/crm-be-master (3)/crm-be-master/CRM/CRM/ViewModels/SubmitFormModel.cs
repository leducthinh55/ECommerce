using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class SubmitFormModel
    {
        public Guid ProcessId { get; set; }
        public object FormData { get; set; }
    }
}
