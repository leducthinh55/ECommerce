using System;
using System.Collections.Generic;
using System.IO;
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
    public class GlobalVariableValueController : ControllerBase
    {
        private readonly IGlobalVariableValueService _globalVariableValueService;
        private readonly IGlobalVariableService _globalVariableService;

        public GlobalVariableValueController(IGlobalVariableValueService globalVariableValueService, IGlobalVariableService globalVariableService)
        {
            _globalVariableValueService = globalVariableValueService;
            _globalVariableService = globalVariableService;
        }

        

        [HttpGet("GetByCustomerWorkflowId/{id}")]
        public ActionResult Get(Guid id)
        {
            try
            {
                var data = _globalVariableValueService.GetGlobalVariableValues(_ => _.CustomerWorkflowId.Equals(id));
                List<GlobalVariableValueVM> result = new List<GlobalVariableValueVM>();
                foreach (var item in data)
                {
                    var GV = _globalVariableService.GetGlobalVariable(item.GlobalVariableId);
                    
                    result.Add(new GlobalVariableValueVM
                    {
                        Id = item.Id,
                        Name = GV.Name,
                        Type = GV.Type,
                        Value = GV.Type == "file" ? "GlobalVariableValue/"+ item.Id + "/file" :  item.Value
                    });
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]GlobalVariableValueCM model)
        {
            try
            {
                var value = model.Adapt<GlobalVariableValue>();
                _globalVariableValueService.CreateGlobalVariableValue(value);
                _globalVariableValueService.SaveChanges();
                return StatusCode(201,new { id= value.Id});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("files")]
        public async Task<ActionResult> PostFile(Guid globalVariableId, Guid customerWorkflowId, IFormFile file)
        {
            try
            {
                GlobalVariableValue value = new GlobalVariableValue
                {
                    CustomerWorkflowId = customerWorkflowId,
                    GlobalVariableId = globalVariableId,
                    DateCreated = DateTime.Now
                };

                string fileName = await _globalVariableValueService.UploadFile(file);
                if (fileName == null) return StatusCode(500, "Can not create file");
                value.Value = fileName;

                _globalVariableValueService.CreateGlobalVariableValue(value);
                _globalVariableValueService.SaveChanges();

                //Cắt số trước tên file
                fileName = Path.GetFileName(fileName);
                fileName = fileName.Substring(fileName.IndexOf('_') + 1);
                return StatusCode(201, new {
                    value.Id,
                    Date = value.DateCreated,
                    Name = fileName
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/file")]
        public async Task<ActionResult> GetFile(Guid id)
        {
            try
            {
                var value = _globalVariableValueService.GetGlobalVariableValue(id);
                if (value == null) return NotFound();
                var fileSupport = await _globalVariableValueService.DowloadFile(value.Value);
                return File(fileSupport.Stream, fileSupport.ContentType,fileSupport.FileName);
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
                var value = _globalVariableValueService.GetGlobalVariableValue(id);
                if (value == null) return NotFound();
                var GV = _globalVariableService.GetGlobalVariable(value.GlobalVariableId);
                if(GV.Type == "file")
                {
                    _globalVariableValueService.DeleteFile(value.Value);
                }
                _globalVariableValueService.DeleteGlobalVariableValue(value);
                _globalVariableValueService.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}