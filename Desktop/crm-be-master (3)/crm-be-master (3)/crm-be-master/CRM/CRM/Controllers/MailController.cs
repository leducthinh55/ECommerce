using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRM.Service;
using CRM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IEmailService _mailService;

        public MailController(IEmailService mmailService)
        {
            _mailService = mmailService;
        }

        [HttpPost("_")]
        public ActionResult Test([FromForm]MailModel FileAttachments)
        {
            return Ok(FileAttachments.Files.Count + " done");  
        }

       

        [HttpPost]
        public async Task<ActionResult> SendMailAsync([FromForm] MailModel model)
        {
            try
            {
                List<FileAttachmentModel> fileAttachments = new List<FileAttachmentModel>();
                if(model.Files != null)
                {
                    foreach (var item in model.Files)
                    {
                        MemoryStream stream = new MemoryStream();
                        using (var reader = item.OpenReadStream())
                        {
                            await reader.CopyToAsync(stream);
                        }
                        fileAttachments.Add(
                            new FileAttachmentModel
                            {
                                FileName = item.FileName,
                                FileStream = stream
                            });
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}