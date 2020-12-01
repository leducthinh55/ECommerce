using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class MenuCM
    {
        public String GroupName { get; set; }
        public String Label { get; set; }
        public String Detail { get; set; }
        public String RouterLink { get; set; }
        public String IconType { get; set; }
        public String IconName { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class MenuVM
    {
        public String Label { get; set; }
        public String Detail { get; set; }
        public String RouterLink { get; set; }
        public String IconType { get; set; }
        public String IconName { get; set; }
        public List<MenuVM> SubMenu { get; set; }
    }

    public class GroupMenuVM
    {
        public String GroupName { get; set; }
        public List<MenuVM> Items { get; set; }
    }
}
