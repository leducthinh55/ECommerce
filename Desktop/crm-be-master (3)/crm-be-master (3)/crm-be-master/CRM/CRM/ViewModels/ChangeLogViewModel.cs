using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class ChangeLogViewModel
    {
        public Guid Id { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
    }
}
