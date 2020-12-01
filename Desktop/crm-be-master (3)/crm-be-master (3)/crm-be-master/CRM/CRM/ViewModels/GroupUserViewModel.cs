using System;
using System.Collections.Generic;

namespace CRM.ViewModels
{
    public class GroupUserViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid GroupId { get; set; }
    }
    public class GroupUserCreateViewModel
    {
        public string UserId { get; set; }
        public Guid GroupId { get; set; }
    }
}