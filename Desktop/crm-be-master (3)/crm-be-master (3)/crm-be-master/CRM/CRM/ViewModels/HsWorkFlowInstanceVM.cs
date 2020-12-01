using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class HsWorkFlowInstanceViewModel
    {
        public String Id { get; set; }
        public string Code { get; set; }

        public String WorkFlowId { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }
        public String SubType { get; set; }
        public String Description { get; set; }
        public Guid? FormId { get; set; }
    }

    public class HsWorkFlowInstanceCreateModel
    {
        public Guid WorkFlowId { get; set; }
        public String Name { get; set; }
        public string Code { get; set; }
        public String Type { get; set; }
        public String SubType { get; set; }
    }

    public class HsWorkFlowInstanceUpdateModel
    {
        public Guid Id { get; set; }
        public String WorkFlowId { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }
        public String SubType { get; set; }
        public String Description { get; set; }
    }


}
