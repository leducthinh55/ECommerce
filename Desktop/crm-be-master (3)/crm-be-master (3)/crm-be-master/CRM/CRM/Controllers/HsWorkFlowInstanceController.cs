using System;
using System.Collections.Generic;
using CRM.Helpers;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;


namespace CRM.Controllers
{
    [Route("api/[controller]")]
    public class HsWorkFlowInstanceController : ControllerBase
    {
        private readonly IHsWorkFlowInstanceService _hsWorkFlowInstanceService;
        private readonly IPermissionService _permissionService;
        private readonly IHsWorkFlowService _hsWorkFlowService;
        private readonly IFormService _formService;

        public HsWorkFlowInstanceController(IHsWorkFlowInstanceService hsWorkFlowInstanceService, IPermissionService permissionSerive, IHsWorkFlowService hsWorkFlowService, IFormService formService)
        {
            _hsWorkFlowInstanceService = hsWorkFlowInstanceService;
            _permissionService = permissionSerive;
            _hsWorkFlowService = hsWorkFlowService;
            _formService = formService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            List<HsWorkFlowInstanceViewModel> result = new List<HsWorkFlowInstanceViewModel>();
            var data = _hsWorkFlowInstanceService.GetHsWorkFlowInstances();
            foreach (var item in data)
            {
                result.Add(item.Adapt<HsWorkFlowInstanceViewModel>());
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            try
            {
                var data = _hsWorkFlowInstanceService.GetHsWorkFlowInstance(id);
                if (data == null) return StatusCode(404);
                return Ok(data.Adapt<HsWorkFlowInstanceViewModel>());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("GetByWorkFlowId/{id}")]
        public ActionResult GetByWorkFlowId(Guid id)
        {
            try
            {
                List<HsWorkFlowInstanceViewModel> result = new List<HsWorkFlowInstanceViewModel>();
                var data = _hsWorkFlowInstanceService.GetHsWorkFlowInstances(_ => _.WorkFlowId.Equals(id) && _.IsDeleted == false);
                foreach (var item in data)
                {
                    result.Add(item.Adapt<HsWorkFlowInstanceViewModel>());
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetByWorkFlowId/{id}/AuthRead")]
        public ActionResult GetByWorkFlowIdAuthRead(Guid id, List<Guid> permissionIds)
        {
            try
            {
                permissionIds.Sort();
                List<HsWorkFlowInstanceViewModel> result = new List<HsWorkFlowInstanceViewModel>();
                var data = _hsWorkFlowInstanceService.GetHsWorkFlowInstances(_ => _.WorkFlowId.Equals(id) && _.IsDeleted == false);
                foreach (var item in data)
                {
                    if (permissionIds.BST(item.PermissionIdR))
                        result.Add(item.Adapt<HsWorkFlowInstanceViewModel>());
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByWorkFlowId/{id}/AuthWrite")]
        public ActionResult GetByWorkFlowIdAuthWrite(Guid id, List<Guid> permissionIds)
        {
            try
            {
                permissionIds.Sort();
                List<HsWorkFlowInstanceViewModel> result = new List<HsWorkFlowInstanceViewModel>();
                var data = _hsWorkFlowInstanceService.GetHsWorkFlowInstances(_ => _.WorkFlowId.Equals(id) && _.IsDeleted == false);
                foreach (var item in data)
                {
                    if (permissionIds.BST(item.PermissionIdW))
                        result.Add(item.Adapt<HsWorkFlowInstanceViewModel>());
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]HsWorkFlowInstanceCreateModel model)
        {
            try
            {
                var hsWorkFlowInstance = model.Adapt<HsWorkFlowInstance>();
                _hsWorkFlowInstanceService.CreateHsWorkFlowInstance(hsWorkFlowInstance);
                _hsWorkFlowInstanceService.SaveChange();
                var hsWorkFlow = _hsWorkFlowService.GetHsWorkFlow(hsWorkFlowInstance.WorkFlowId);

                //Create Permission
                var permissionR = new HsPermission
                {
                    Name = hsWorkFlow.Code + "-" + model.Name + "-" + "R",
                };
                var permissionW = new HsPermission
                {
                    Name = hsWorkFlow.Code + "-" + model.Name + "-" + "W",
                };
                var permissionN = new HsPermission
                {
                    Name = hsWorkFlow.Code + "-" + model.Name + "-" + "N",
                };
                _permissionService.CreatePermission(permissionR);
                _permissionService.CreatePermission(permissionW);
                _permissionService.CreatePermission(permissionN);
                hsWorkFlowInstance.PermissionIdR = permissionR.Id;
                hsWorkFlowInstance.PermissionIdW = permissionW.Id;
                hsWorkFlowInstance.PermissionIdNoti = permissionN.Id;
                _permissionService.SavePermission();
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}/form/{formId}")]
        public ActionResult AddForm(Guid id, Guid formId)
        {
            try
            {
                var form = _formService.GetForm(formId);
                var instance = _hsWorkFlowInstanceService.GetHsWorkFlowInstance(id);
                if (form == null || instance == null)
                {
                    return NotFound();
                }
                instance.FormId = formId;
                _hsWorkFlowInstanceService.UpdateHsWorkFlowInstance(instance);
                _hsWorkFlowInstanceService.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody]HsWorkFlowInstanceUpdateModel hsWorkFlowInstanceUM)
        {
            try
            {
                var hsWorkFlowInstance = _hsWorkFlowInstanceService.GetHsWorkFlowInstance(hsWorkFlowInstanceUM.Id);
                if (hsWorkFlowInstance == null) return StatusCode(404);
                hsWorkFlowInstance = hsWorkFlowInstanceUM.Adapt(hsWorkFlowInstance);
                _hsWorkFlowInstanceService.UpdateHsWorkFlowInstance(hsWorkFlowInstance);

                //update permission
                _permissionService.EditPermission(new
                            HsPermission
                {
                    Id = hsWorkFlowInstance.PermissionIdR.Value,
                    Name = hsWorkFlowInstance.WorkFlow.Code + "-" + hsWorkFlowInstance.Name + "-" + "R",
                });
                _permissionService.EditPermission(new
                            HsPermission
                {
                    Id = hsWorkFlowInstance.PermissionIdW.Value,
                    Name = hsWorkFlowInstance.WorkFlow.Code + "-" + hsWorkFlowInstance.Name + "-" + "W",
                });
                _permissionService.EditPermission(new
                            HsPermission
                {
                    Id = hsWorkFlowInstance.PermissionIdNoti.Value,
                    Name = hsWorkFlowInstance.WorkFlow.Code + "-" + hsWorkFlowInstance.Name + "-" + "N",
                });

                _hsWorkFlowInstanceService.SaveChange();
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
                var hsWorkFlowInstance = _hsWorkFlowInstanceService.GetHsWorkFlowInstance(id);
                if (hsWorkFlowInstance == null) return StatusCode(404);
                _hsWorkFlowInstanceService.DeleteHsWorkFlowInstance(hsWorkFlowInstance);
                _hsWorkFlowInstanceService.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
