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
    public class TelecomserviceParameterController : ControllerBase
    {
        private readonly ITelecomserviceParameterService _TelecomserviceParameterService;

        public TelecomserviceParameterController(ITelecomserviceParameterService telecomserviceParameterService)
        {
            _TelecomserviceParameterService = telecomserviceParameterService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = _TelecomserviceParameterService.GetTelecomserviceParameters().Select(s => s.Adapt<TelecomserviceParameterVM>());
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var result = _TelecomserviceParameterService.GetTelecomserviceParameter(id).Adapt<TelecomserviceParameterVM>();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody]TelecomserviceParameterCM model)
        {
            try
            {
                var TelecomserviceParameter = model.Adapt<TelecomserviceParameter>();
                _TelecomserviceParameterService.CreateTelecomserviceParameter(TelecomserviceParameter);
                _TelecomserviceParameterService.SaveTelecomserviceParameter();
                return StatusCode(201, TelecomserviceParameter.Adapt<TelecomserviceParameterVM>());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        // PUT api/values/5
        [HttpPut]
        public ActionResult Put([FromBody]TelecomserviceParameterUM model)
        {
            try
            {
                var TelecomserviceParameter = _TelecomserviceParameterService.GetTelecomserviceParameter(model.Id);
                var Telecomservice = TelecomserviceParameter.Telecomservice;
                if (Telecomservice.CoContractTelServices.Count() != 0) return BadRequest(new { Message = "Dich vu dang duoc su dung trong : " + Telecomservice.CoContractTelServices.Select(c => c.Adapt<CoContractTelServiceVM>()) });
                if (Telecomservice.TelecomserviceContractAppendices.Count() != 0) return BadRequest(new { Message = "Dich vu dang duoc su dung trong : " + Telecomservice.TelecomserviceContractAppendices.Select(c => c.Adapt<TelecomserviceContractAppendixVM>()) });
                TelecomserviceParameter = model.Adapt(TelecomserviceParameter);
                _TelecomserviceParameterService.EditTelecomserviceParameter(TelecomserviceParameter);
                _TelecomserviceParameterService.SaveTelecomserviceParameter();
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
                var TelecomserviceParameter = _TelecomserviceParameterService.GetTelecomserviceParameter(id);
                var Telecomservice = TelecomserviceParameter.Telecomservice;
                if (Telecomservice.CoContractTelServices.Count() != 0) return BadRequest(new { Message = "Dich vu dang duoc su dung trong : " + Telecomservice.CoContractTelServices.Select(c => c.Adapt<CoContractTelServiceVM>()) });
                if (Telecomservice.TelecomserviceContractAppendices.Count() != 0) return BadRequest(new { Message = "Dich vu dang duoc su dung trong : " + Telecomservice.TelecomserviceContractAppendices.Select(c => c.Adapt<TelecomserviceContractAppendixVM>()) });
                if (TelecomserviceParameter == null) return NotFound();
                _TelecomserviceParameterService.RemoveTelecomserviceParameter(TelecomserviceParameter);
                _TelecomserviceParameterService.SaveTelecomserviceParameter();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
