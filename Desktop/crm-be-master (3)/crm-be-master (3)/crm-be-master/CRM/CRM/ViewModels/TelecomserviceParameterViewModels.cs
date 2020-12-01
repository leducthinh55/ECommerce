using System;
namespace CRM.ViewModels
{
    public class TelecomserviceParameterVM
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Guid TelecomServiceId { get; set; }
    }
    public class TelecomserviceParameterCM
    {
        public String Name { get; set; }
        public Guid TelecomServiceId { get; set; }
    }
    public class TelecomserviceParameterUM : TelecomserviceParameterVM
    {

    }
}
