using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class AccountVM
    {
        public String Id { get; set; }
        public String UserName { get; set; }
        public String FullName { get; set; }
        public IList<String> RoleNames { get; set; }
    }

    public class RoleVM
    {
        public String Id { get; set; }
        public String Name { get; set; }
    }

    public class RoleCM
    {
        public String Name { get; set; }
    }
    public class RoleUM : RoleVM
    {
    }

    public class AccountRoleUM
    {
        public String Id { get; set; }
        public String[] RoleNames { get; set; }
    }
}
