using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class CustomerWorkFlowCreateModel
    {
        public Guid? CustomerId { get; set; }
        public Guid WorkFlowId { get; set; }
    }

    public class CustomerWorkFlowViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid CustomerId { get; set; }
        public Guid WorkFlowId { get; set; }
        public DateTime StartDate { get; set; }
    }
    public class FileCommonVM
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public DateTime DateCreated { get; set; }
        public int Type { get; set; }
    }

    public enum FileType
    {
        Global, WorkFlowHistory
    }
}
