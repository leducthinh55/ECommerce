using System;

namespace CRM.ViewModels
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class RoleCreateViewModel
    {
        public string Name { get; set; }
    }
}