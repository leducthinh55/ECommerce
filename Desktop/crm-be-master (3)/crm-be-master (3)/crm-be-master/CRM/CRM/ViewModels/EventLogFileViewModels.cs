using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class EventLogFileVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class EventLogFileCM
    {
        public Guid EventLogId { get; set; }
        public List<IFormFile> Files { get; set; }
    }

    public class EventLogFileResult
    {
        public string FileName { get; set; }
        public bool Status { get; set; }
    }
}
