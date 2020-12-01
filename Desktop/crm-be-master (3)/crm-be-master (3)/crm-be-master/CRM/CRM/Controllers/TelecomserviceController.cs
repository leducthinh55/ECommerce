using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    public class TelecomserviceController : ControllerBase
    {
        private readonly ITelecomserviceService _TelecomserviceService;
        private readonly ITelecomserviceParameterService _telecomserviceParameterService;

        public TelecomserviceController(ITelecomserviceService telecomserviceService, ITelecomserviceParameterService telecomserviceParameterService)
        {
            _TelecomserviceService = telecomserviceService;
            _telecomserviceParameterService = telecomserviceParameterService;
        }

        [HttpGet]
        public ActionResult Get(int? type)
        {
            var result = new List<TelecomserviceVM>();
            if (type == null)
            {
                result = _TelecomserviceService.GetTelecomservices().Select(s => s.Adapt<TelecomserviceVM>()).OrderBy(_ => _.Name).ToList();
            }
            else
            {
                result = _TelecomserviceService.GetTelecomservices(c => c.CommonTelecomservice != null && c.CommonTelecomservice.Type == type)
                    .Select(s => s.Adapt<TelecomserviceVM>())
                    .OrderBy(_ => _.Name)
                    .ToList();
            }
            return Ok(result);
        }

        [HttpGet("{id}/Parameter")]
        public ActionResult GetById(Guid id)
        {
            var result = new List<string>();
            var telecomservice = _TelecomserviceService.GetTelecomservice(id);
            if (telecomservice == null)
            {
                return NotFound();
            }
            var telecomParameters = telecomservice.TelecomserviceParameters.OrderBy(_=> _.Order).ToList();
            foreach (var telecomParameter in telecomParameters)
            {
                result.Add(telecomParameter.Name);
            }
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody]TelecomserviceCM model)
        {
            try
            {
                var Telecomservice = model.Adapt<Telecomservice>();
                _TelecomserviceService.CreateTelecomservice(Telecomservice);
                _TelecomserviceService.SaveTelecomservice();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        // PUT api/values/5
        [HttpPut]
        public ActionResult Put([FromBody]TelecomserviceUM model)
        {
            try
            {
                var Telecomservice = _TelecomserviceService.GetTelecomservice(model.Id);
                if (Telecomservice.CoContractTelServices.Count() != 0) return BadRequest(new { Message = "Dich vu dang duoc su dung trong : " + Telecomservice.CoContractTelServices.Select(c => c.Adapt<CoContractTelServiceVM>())});
                if (Telecomservice.TelecomserviceContractAppendices.Count() != 0) return BadRequest(new { Message = "Dich vu dang duoc su dung trong : " + Telecomservice.TelecomserviceContractAppendices.Select(c => c.Adapt<TelecomserviceContractAppendixVM>())});
                Telecomservice = model.Adapt(Telecomservice);
                _TelecomserviceService.EditTelecomservice(Telecomservice);
                _TelecomserviceService.SaveTelecomservice();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var Telecomservice = _TelecomserviceService.GetTelecomservice(id);
                if (Telecomservice.CoContractTelServices.Count() != 0) return BadRequest(new { Message = "Dich vu dang duoc su dung trong : " + Telecomservice.CoContractTelServices.Select(c => c.Adapt<CoContractTelServiceVM>()) });
                if (Telecomservice.TelecomserviceContractAppendices.Count() != 0) return BadRequest(new { Message = "Dich vu dang duoc su dung trong : " + Telecomservice.TelecomserviceContractAppendices.Select(c => c.Adapt<TelecomserviceContractAppendixVM>()) });
                if (Telecomservice == null) return NotFound();
                _TelecomserviceService.RemoveTelecomservice(Telecomservice);
                _TelecomserviceService.SaveTelecomservice();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
