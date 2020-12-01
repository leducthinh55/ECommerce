using System;
using System.Collections.Generic;

namespace CRM.ViewModels
{
    public class TelecomserviceVM
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Guid CommonTelecomserviceId { get; set; }
    }
    public class TelecomserviceCM
    {
        public String Name { get; set; }
        public Guid CommonTelecomserviceId { get; set; }
    }
    public class TelecomserviceUM : TelecomserviceVM
    {

    }
}
