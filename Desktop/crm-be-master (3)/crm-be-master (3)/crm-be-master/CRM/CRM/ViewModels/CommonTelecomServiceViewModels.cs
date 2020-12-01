using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class CommonTelecomServiceVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }

    public class CommonTelecomServiceDetailVM : CommonTelecomServiceVM
    {
        public CommonTelecomServiceDetailVM()
        {
            TelecomserviceVMs = new List<TelecomserviceVM>();
        }
        public List<TelecomserviceVM> TelecomserviceVMs { get; set; }
    }

    public class CommonTelecomServiceCM
    {
        public string Name { get; set; }
        public int Type { get; set; }
    }

    public class CommonTelecomServiceUM : CommonTelecomServiceVM
    {

    }

}
