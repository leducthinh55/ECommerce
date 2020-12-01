using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventLogController : ControllerBase
    {
        private readonly IEventLogService _eventLogService;

        public EventLogController(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] EventLogCM model)
        {
            try
            {
                var eventLog = model.Adapt<EventLog>();
                eventLog.CreatedBy = User.Identity.Name;
                _eventLogService.CreateEventLog(eventLog);
                _eventLogService.SaveChanges();
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByWorkFlowHistoryId/{id}")]
        public ActionResult GetByCustomerWorkflowId(Guid id)
        {
            var data = _eventLogService.GetEventLogs(_ => _.WorkFlowHistoryId == id && _.IsDeleted == false).OrderByDescending(_=>_.DateCreated);
            List<EventLogVM> result = new List<EventLogVM>();
            foreach (var item in data)
            {
                result.Add(item.Adapt<EventLogVM>());
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var result = _eventLogService.GetEventLog(id);
            return Ok(result.Adapt<EventLogVM>());
        }

        [HttpPut]
        public ActionResult Put([FromBody] EventLogUM model)
        {
            try
            {
                var eventLog = _eventLogService.GetEventLog(model.Id);
                if (eventLog == null) return NotFound();
                eventLog = model.Adapt(eventLog);
                eventLog.CreatedBy = User.Identity.Name;
                _eventLogService.UpdateEventLog(eventLog);
                _eventLogService.SaveChanges();
                return Ok();
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
                var eventLog = _eventLogService.GetEventLog(id);
                if (eventLog == null) return NotFound();
                _eventLogService.DeleteEventLog(eventLog);
                _eventLogService.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}