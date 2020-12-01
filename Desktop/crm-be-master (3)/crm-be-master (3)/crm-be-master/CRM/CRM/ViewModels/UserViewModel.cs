using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public String  UserName { get; set; }

        public String FullName { get; set; }
    }

    public class UserCreateModel
    {
        public String UserName { get; set; }

        public String FullName { get; set; }

        public String Password { get; set; }
    }
}
