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
    public class GlobalVariableController : ControllerBase
    {
        private readonly IGlobalVariableService _globalVariableService;

        public GlobalVariableController(IGlobalVariableService globalVariableService)
        {
            _globalVariableService = globalVariableService;
        }

        [HttpPost]
        public ActionResult Post(GlobalVariableCM model)
        {
            try
            {
                var globalVariable = model.Adapt<GlobalVariable>();
                _globalVariableService.CreateGlobalVariable(globalVariable);
                _globalVariableService.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }
        [HttpGet("GetByWorkFlowId/{id}")]
        public ActionResult GetByCustomerWFId(Guid id)
        {
            var data = _globalVariableService.GetGlobalVariables(_=>_.WorkflowId.Equals(id));
            List<GlobalVariableVM> result = new List<GlobalVariableVM>();
            foreach (var item in data)
            {
                result.Add(item.Adapt<GlobalVariableVM>());
            }
            return Ok(result);
        }
    }
}