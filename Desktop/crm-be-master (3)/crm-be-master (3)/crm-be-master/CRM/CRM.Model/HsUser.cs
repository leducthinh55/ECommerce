
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    public class HsUser : IdentityUser
    {
        public String FullName { get; set; }

        public bool? IsEnabled { get; set; }
        public String Permissions { get; set; }

        public virtual ICollection<HsGroupUser> Groups { get; set; }
        public virtual ICollection<HsRoleOfUser> Roles { get; set; }
        public virtual ICollection<HubUserConnection> HubUserConnections { get; set; }
        public virtual ICollection<HsNotification> Notifications { get; set; }
    }
}
