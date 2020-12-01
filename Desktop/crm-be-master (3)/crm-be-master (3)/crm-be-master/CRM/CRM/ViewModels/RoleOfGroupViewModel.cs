using System;
using System.Collections.Generic;

namespace CRM.ViewModels
{
    public class RoleOfGroupViewModel
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid GroupId { get; set; }
    }
    public class RoleOfGroupCreateViewModel
    {
        public IEnumerable<Guid> RoleIds { get; set; }
        public Guid GroupId { get; set; }
    }
}