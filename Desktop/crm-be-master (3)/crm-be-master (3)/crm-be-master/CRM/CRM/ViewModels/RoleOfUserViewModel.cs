using System;
using System.Collections.Generic;

namespace CRM.ViewModels
{
    public class RoleOfUserViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid RoleId { get; set; }

    }
    public class RoleOfUserCreateViewModel
    {
        public string UserId { get; set; }
        public IEnumerable<Guid> RoleIds { get; set; }
    }
}