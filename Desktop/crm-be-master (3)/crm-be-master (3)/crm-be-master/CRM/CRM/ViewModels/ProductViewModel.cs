using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class ProductVM
    {
        public Guid Id { get; set; }

        public String Name { get; set; }
    }

    public class ProductCM
    {
        public String Name { get; set; }
    }

    public class ProductUM
    {
        public Guid Id { get; set; }

        public String Name { get; set; }
    }


}
