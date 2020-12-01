using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class IdentityCardViewModel
    {
        public string Id { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string IssueAt { get; set; }
    }
    public class IdentityCardUpdateModel
    {
        public DateTime IssueDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string IssueAt { get; set; }
    }
}
