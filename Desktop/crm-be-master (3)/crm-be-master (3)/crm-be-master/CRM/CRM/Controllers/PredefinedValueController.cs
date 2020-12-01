using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;


namespace CRM.Controllers
{
    [Route("api/[controller]")]
    public class PredefinedValueController : ControllerBase
    {
        private readonly IPredefinedValueService _predefinedValueService;

        public PredefinedValueController(IPredefinedValueService predefinedValueService)
        {
            _predefinedValueService = predefinedValueService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var data = _predefinedValueService.GetPredefinedValues(_=>!_.IsDelete );
            List<PredefinedValueVM> result = new List<PredefinedValueVM>();
            foreach(var item in data )
            {
                result.Add(item.Adapt<PredefinedValueVM>());
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var predefinedValue = _predefinedValueService.GetPredefinedValue(id);
            if (predefinedValue == null) return NotFound();
            return Ok(predefinedValue.Adapt<PredefinedValueVM>());
        }

        [HttpPost]
        public ActionResult Post([FromBody]PredefinedValueCM predefinedValueCM)
        {
            try
            {
                var predefinedValue = predefinedValueCM.Adapt<PredefinedValue>();
                _predefinedValueService.CreatePredefinedValue(predefinedValue);
                _predefinedValueService.SaveChange();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpPut]
        public ActionResult Put([FromBody]PredefinedValueUM predefinedValueUM)
        {
            try
            {
                var predefinedValue = _predefinedValueService.GetPredefinedValue(predefinedValueUM.Id);
                if (predefinedValue == null) return NotFound();
                predefinedValue = predefinedValueUM.Adapt(predefinedValue);
                _predefinedValueService.UpdatePredefinedValue(predefinedValue);
                _predefinedValueService.SaveChange();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
               var predefinded =  _predefinedValueService.GetPredefinedValue(id);
                predefinded.IsDelete = true;
                _predefinedValueService.UpdatePredefinedValue(predefinded);
                _predefinedValueService.SaveChange();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }


    }
}
