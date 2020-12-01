using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class HsWorkFlowConnectionCreateModel
    {
        public String Type { get; set; }
        public string Command { get; set; }
        public String FromInstanceId { get; set; }
        public String ToInstanceId { get; set; }
    }

    public class HsWorkFlowConnectionViewModel
    {
        public Guid Id { get; set; }
        public String Type { get; set; }
        public string Command { get; set; }
        public String FromInstanceId { get; set; }
        public String ToInstanceId { get; set; }
    }

    public class HsWorkFlowConnectionUpdateModel
    {
        public Guid  Id { get; set; }
        public String Type { get; set; }
        public string Command { get; set; }
        public String FromInstanceId { get; set; }
        public String ToInstanceId { get; set; }
    }


}
