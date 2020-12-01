using System;

namespace CRM.ViewModels
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class GroupCreateViewModel
    {
        public string Name { get; set; }
    }
}