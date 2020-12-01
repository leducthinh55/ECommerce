using System;
using System.Collections.Generic;

namespace CRM.ViewModels
{
    public class PermissionOfRoleViewModel
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
    }
    public class PermissionOfRoleCreateViewModel
    {
        public Guid RoleId { get; set; }
        public List<Guid> PermissionId { get; set; }
    }


}