using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class BuildingVM : BuildingUM
    {
    }

    public class BuildingDetailVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Square { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserUpdated { get; set; }
        public DateTime DateUpdated { get; set; }
    }

    public class BuildingCM
    {
        public string Name { get; set; }
        public double Square { get; set; }
    }

    public class BuildingUM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Square { get; set; }
    }

}
