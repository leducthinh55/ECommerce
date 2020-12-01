using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class MailSendModel
    {
        public string ToMail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string[] Cc { get; set; }
        public IFormFile[] fileAttachments { get; set; }
    }

    public class MailModel
    {
        public string ToMail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public List<string> Cc { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
