using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class CategoryVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryCM
    {
        public string Name { get; set; }
    }
    public class CategoryUM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
