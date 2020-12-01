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
    public class CommonTelecomServiceController : ControllerBase
    {
        private readonly ICommonTelecomserviceService _common;

        public CommonTelecomServiceController(ICommonTelecomserviceService common)
        {
            _common = common;
        }

        [HttpGet("Type")]
        public ActionResult GetType()
        {
            var result = new List<String>();
            foreach (var name in Enum.GetValues(typeof(CommonTelecomserviceType)))
            {
                result.Add(name.ToString());
            }
            return Ok(new { Type = result });
        }

        [HttpGet]
        public ActionResult getAll(int? type)
        {
            if (type == null)
            {
                return Ok(_common.GetCommonTelecomservices().Select(c => c.Adapt<CommonTelecomServiceVM>()));
            }

            return Ok(_common.GetCommonTelecomservices(c => c.Type == type).Select(c => c.Adapt<CommonTelecomServiceVM>()));
        }
        
        [HttpGet("{id}")]
        public ActionResult getAll(Guid id)
        {
            var common = _common.GetCommonTelecomservice(id);
            if (common == null) return NotFound();
            var commonVM = common.Adapt<CommonTelecomServiceDetailVM>();
            foreach (var service in common.Telecomservices)
            {
                commonVM.TelecomserviceVMs.Add(service.Adapt<TelecomserviceVM>());
            }
            return Ok(commonVM);
        }

        [HttpPost]
        public ActionResult create([FromBody] CommonTelecomServiceCM model)
        {
            var common = model.Adapt<CommonTelecomservice>();
            try
            {
                _common.CreateCommonTelecomservice(common);
                _common.SaveCommonTelecomservice();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut]
        public ActionResult update([FromBody] CommonTelecomServiceUM model)
        {
            var common = _common.GetCommonTelecomservice(model.Id);
            if (common == null) return NotFound();
            try
            {
                common = model.Adapt(common);
                _common.EditCommonTelecomservice(common);
                _common.SaveCommonTelecomservice();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult delete(Guid id)
        {
            var common = _common.GetCommonTelecomservice(id);
            if (common == null) return NotFound();
            try
            {
                _common.RemoveCommonTelecomservice(common);
                _common.SaveCommonTelecomservice();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}