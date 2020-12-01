using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.Service;
using CRM.Service.Utils;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventLogFileController : ControllerBase
    {
        private readonly IEventLogFileService _eventLogFileService;

        public EventLogFileController(IEventLogFileService eventLogFileService)
        {
            _eventLogFileService = eventLogFileService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm]EventLogFileCM model)
        {
            List<EventLogFileResult> result = new List<EventLogFileResult>();
            foreach (var file in model.Files)
            {
                string path = "";
                try
                {
                    EventLogFile fileDb = new EventLogFile
                    {
                        Name = file.FileName,
                        DateCreated = DateTime.Now,
                        EventLogId = model.EventLogId,
                    };
                    path = await _eventLogFileService.UploadFile(file,FilePath.EventLogFile);
                    if (path == null)
                    {
                        result.Add(new EventLogFileResult
                        {
                            FileName = file.FileName,
                            Status = false
                        });
                        continue;
                    }
                    fileDb.Path = path;
                    _eventLogFileService.CreateEventLogFile(fileDb);
                    _eventLogFileService.SaveChanges();
                    result.Add(new EventLogFileResult
                    {
                        FileName = file.FileName,
                        Status = true
                    });
                }
                catch (Exception e)
                {
                    _eventLogFileService.DeleteFile(path);
                    result.Add(new EventLogFileResult
                    {
                        FileName = file.FileName,
                        Status = false
                    });
                }
            }
            return StatusCode(201, result);
            
        }

        [HttpGet("GetByEventLogId/{id}")]
        public ActionResult GetGetByEventLogId(Guid id)
        {
            var data = _eventLogFileService.GetEventLogFiles(_ => _.EventLogId == id);
            List<EventLogFileVM> result = new List<EventLogFileVM>();
            foreach (var item in data)
            {
                result.Add(item.Adapt<EventLogFileVM>());
            }
            return Ok(result);
        }

        [HttpGet("File/{id}")]
        public async Task<ActionResult> GetFile(Guid id)
        {
            try
            {
                var fileDb = _eventLogFileService.GetEventLogFile(id);
                if (fileDb == null) return NotFound();
                FileSupport fileSupport = await _eventLogFileService.DownloadFile(fileDb.Path);
                return File(fileSupport.Stream, fileSupport.ContentType, fileDb.Name);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var fileDb = _eventLogFileService.GetEventLogFile(id);
                if (fileDb == null) return NotFound();
                _eventLogFileService.DeleteEventLogFile(fileDb);
                _eventLogFileService.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}