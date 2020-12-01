using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class HsWorkFlowViewModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public string Code { get; set; }
        public String Description { get; set; }
        public int Type { get; set; }
        public  bool IsWrite { get; set; }
    }

    public class HsWorkFlowCreateModel
    {
        public String Name { get; set; }
        public string Code { get; set; }
        public String Description { get; set; }
    }


    public class HsWorkFlowUpdateModel
    {
        public Guid  Id { get; set; }
        public String Name { get; set; }
        public string Code { get; set; }
        public String Description { get; set; }
    }
}
