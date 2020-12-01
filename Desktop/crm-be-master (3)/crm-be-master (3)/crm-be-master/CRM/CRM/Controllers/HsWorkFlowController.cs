

using CRM.Helpers;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    public class HsWorkFlowController : ControllerBase
    {
        private readonly IHsWorkFlowService _workFlowService;
        private readonly IPermissionService _permissionSerive;
        private readonly UserManager<HsUser> _userManager;
        private readonly IFormService _formService;

        public HsWorkFlowController(IHsWorkFlowService workFlowService, IPermissionService permissionSerive, UserManager<HsUser> userManager, IFormService formService)
        {
            _workFlowService = workFlowService;
            _permissionSerive = permissionSerive;
            _userManager = userManager;
            _formService = formService;
        }




        // GET: api/<controller>
        [HttpGet]
        [Authorize]
        public ActionResult Get()
        {
            var _user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var permissions = JsonConvert.DeserializeObject<List<Guid>>(_user.Permissions);

            var data = _workFlowService.GetHsWorkFlows();
                //.Where(_=>permissions.Contains(_.PermissionIdR.Value))
            List<HsWorkFlowViewModel> result = new List<HsWorkFlowViewModel>();
            foreach (var item in data)
            {
                var _item = item.Adapt<HsWorkFlowViewModel>();
                _item.IsWrite = permissions.Contains(item.PermissionIdR.Value);
                result.Add(_item);
            }
            return Ok(result);
        }

        // GET: api/<controller>
        [HttpGet("Admin")]
        [Authorize(Roles = "Admin")]
        public ActionResult GetAll() => Ok(_workFlowService.GetHsWorkFlows()
                                                            .Select(item => item.Adapt<HsWorkFlowViewModel>())
                                                            .ToList());

        [HttpGet("/AuthRead")]
        public ActionResult GetAuthRead(List<Guid> permissionIds)
        {
            permissionIds.Sort();
            List<HsWorkFlowViewModel> result = new List<HsWorkFlowViewModel>();
            var data = _workFlowService.GetHsWorkFlows();
            foreach (var item in data)
            {
                if (permissionIds.BST(item.PermissionIdR))
                {
                    result.Add(item.Adapt<HsWorkFlowViewModel>());
                }
            }
            return Ok(result);
        }

        [HttpGet("/AuthWrite")]
        public ActionResult GetAuthWrite(List<Guid> permissionIds)
        {
            permissionIds.Sort();
            List<HsWorkFlowViewModel> result = new List<HsWorkFlowViewModel>();
            var data = _workFlowService.GetHsWorkFlows();
            foreach (var item in data)
            {
                if (permissionIds.BST(item.PermissionIdW))
                {
                    result.Add(item.Adapt<HsWorkFlowViewModel>());
                }
            }
            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            HsWorkFlowViewModel result = null;
            try
            {
                var workFlow = _workFlowService.GetHsWorkFlow(id);
                result = workFlow.Adapt<HsWorkFlowViewModel>();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(result);
        }

        [HttpGet("{id}/Form")]
        public ActionResult GetForm(Guid id)
        {
            var data = _formService.GetForms(_ => !_.IsDeleted && (_.HsWorkflowId ==null || _.HsWorkflowId == id)).Select(_=>_.Adapt<FormVM>());
            return Ok(data);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]HsWorkFlowCreateModel workFlowCM)
        {
            try
            {
                var workFlow = workFlowCM.Adapt<HsWorkFlow>();
                _workFlowService.CreateHsWorkFlow(workFlow);
                _workFlowService.SaveHsWorkFlow();

                #region Create Permission
                var permissionR = new HsPermission
                {
                    Name = workFlowCM.Code + "-" + "R",
                };
                var permissionW = new HsPermission
                {
                    Name = workFlowCM.Code + "-" + "W",
                };
                _permissionSerive.CreatePermission(permissionR);
                _permissionSerive.CreatePermission(permissionW);
                workFlow.PermissionIdR = permissionR.Id;
                workFlow.PermissionIdW = permissionW.Id;
                _permissionSerive.SavePermission();
                #endregion
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpPut]
        public ActionResult Put([FromBody]HsWorkFlowUpdateModel workFlowUM)
        {
            try
            {
                var workFlow = _workFlowService.GetHsWorkFlow(workFlowUM.Id);
                if (workFlow == null) return StatusCode(400);
                var changPermission = workFlowUM.Code != workFlow.Code;

                workFlow = workFlowUM.Adapt(workFlow);

                #region Change Permission
                if(changPermission)
                {
                    var R = _permissionSerive.GetPermission(workFlow.PermissionIdR.Value);
                    var W = _permissionSerive.GetPermission(workFlow.PermissionIdW.Value);
                    R.Name = workFlow.Code + "-" + "R";
                    W.Name = workFlow.Code + "-" + "W";
                    foreach (var item in workFlow.Instances)
                    {
                        var iR = _permissionSerive.GetPermission(item.PermissionIdR.Value);
                        var iW = _permissionSerive.GetPermission(item.PermissionIdW.Value);
                        var iN = _permissionSerive.GetPermission(item.PermissionIdNoti.Value);
                        iR.Name = workFlow.Code + "-" + item.Name + "-" + "R";
                        iW.Name = workFlow.Code + "-" + item.Name + "-" + "W";
                        iN.Name = workFlow.Code + "-" + item.Name + "-" + "N";
                    }
                }
                #endregion

                _workFlowService.EditHsWorkFlow(workFlow);
                _workFlowService.SaveHsWorkFlow();
            }
            catch(Exception e)
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
                var workFlow = _workFlowService.GetHsWorkFlow(id);
                if (workFlow == null) return StatusCode(400);
                _workFlowService.RemoveHsWorkFlow(workFlow);
                _workFlowService.SaveHsWorkFlow();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
