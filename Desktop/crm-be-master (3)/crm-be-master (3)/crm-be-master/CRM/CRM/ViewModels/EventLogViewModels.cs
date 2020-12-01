using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class EventLogCM
    {
        public string Type { get; set; }
        public string ActionType { get; set; }
        public DateTime? ExecutedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid WorkFlowHistoryId { get; set; }

    }

    public class EventLogVM
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string ActionType { get; set; }
        public DateTime? ExecutedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid WorkFlowHistoryId { get; set; }

    }

    public class EventLogUM
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ActionType { get; set; }
        public DateTime? ExecutedDate { get; set; }
    }
}
