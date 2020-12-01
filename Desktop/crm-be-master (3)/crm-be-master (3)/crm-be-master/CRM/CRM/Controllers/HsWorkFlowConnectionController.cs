using System;
using System.Collections.Generic;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;


namespace CRM.Controllers
{
    [Route("api/[controller]")]
    public class HsWorkFlowConnectionController : ControllerBase
    {
        private readonly IHsWorkFlowConnectionService _hsWorkFlowConnectionService;

        public HsWorkFlowConnectionController(IHsWorkFlowConnectionService hsWorkFlowConnectionService)
        {
            _hsWorkFlowConnectionService = hsWorkFlowConnectionService ;
        }


        [HttpGet]
        public ActionResult Get()
        {
            List<HsWorkFlowConnectionViewModel> result = new List<HsWorkFlowConnectionViewModel>();
            var data = _hsWorkFlowConnectionService.GetHsWorkFlowConnections();
            foreach(var item in data)
            {
                result.Add(item.Adapt<HsWorkFlowConnectionViewModel>());
            }
            return Ok(result);
        }

        [HttpGet("GetWorkFlowConnectionByFromInstanceId/{id}")]
        public ActionResult GetWorkFlowConnectionByFromInstanceId(Guid id)
        {
            List<HsWorkFlowConnectionViewModel> result = new List<HsWorkFlowConnectionViewModel>();
            try
            {
                var data = _hsWorkFlowConnectionService.GetHsWorkFlowConnections(_=>_.FromInstanceId.Equals(id) && _.IsDeleted == false);
                foreach (var item in data)
                {
                    result.Add(item.Adapt<HsWorkFlowConnectionViewModel>());
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetWorkFlowConnectionByToInstanceId/{id}")]
        public ActionResult GetWorkFlowConnectionByToInstanceId(Guid id)
        {
            List<HsWorkFlowConnectionViewModel> result = new List<HsWorkFlowConnectionViewModel>();
            try
            {
                var data = _hsWorkFlowConnectionService.GetHsWorkFlowConnections(_ => _.ToInstanceId.Equals(id) && _.IsDeleted == false);
                foreach (var item in data)
                {
                    result.Add(item.Adapt<HsWorkFlowConnectionViewModel>());
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            try
            {
                var data = _hsWorkFlowConnectionService.GetHsWorkFlowConnection(id);
                if (data == null) return StatusCode(404);
                return Ok(data.Adapt<HsWorkFlowConnectionViewModel>());
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]HsWorkFlowConnectionCreateModel hsWorkFlowConnectionCM)
        {
            try
            {
                HsWorkFlowConnection workFlowConnection = hsWorkFlowConnectionCM.Adapt<HsWorkFlowConnection>();
                _hsWorkFlowConnectionService.CreateHsWorkFlowConnection(workFlowConnection);
                _hsWorkFlowConnectionService.SaveChange();
                return StatusCode(201);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody]HsWorkFlowConnectionUpdateModel hsWorkFlowConnectionUM)
        {
            try
            {
                HsWorkFlowConnection hsWorkFlowConnection = _hsWorkFlowConnectionService.GetHsWorkFlowConnection(hsWorkFlowConnectionUM.Id);
                if (hsWorkFlowConnection == null) return StatusCode(404);
                hsWorkFlowConnection = hsWorkFlowConnectionUM.Adapt(hsWorkFlowConnection);
                _hsWorkFlowConnectionService.UpdateHsWorkFlowConnection(hsWorkFlowConnection);
                _hsWorkFlowConnectionService.SaveChange();
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                HsWorkFlowConnection hsWorkFlowConnection = _hsWorkFlowConnectionService.GetHsWorkFlowConnection(id);
                if (hsWorkFlowConnection == null) return StatusCode(404);
                _hsWorkFlowConnectionService.DeleteHsWorkFlowConnection(hsWorkFlowConnection);
                _hsWorkFlowConnectionService.SaveChange();
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
