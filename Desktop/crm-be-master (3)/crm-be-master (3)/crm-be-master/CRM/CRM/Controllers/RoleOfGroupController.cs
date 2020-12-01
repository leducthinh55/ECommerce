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
    [ApiController]
    public class RoleOfGroupController : Controller // <-- mean?
    {
        private readonly IRoleOfGroupService _roleOfGroupService;

        public RoleOfGroupController(IRoleOfGroupService roleOfGroupService)
        {
            _roleOfGroupService = roleOfGroupService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var roleOfGroups = _roleOfGroupService.GetRoleOfGroups();
            var viewModels = new List<RoleOfGroupViewModel>();
            foreach (var roleOfGroup in roleOfGroups)
            {
                var viewModel = new RoleOfGroupViewModel();
                viewModel = roleOfGroup.Adapt(viewModel);
                viewModels.Add(viewModel);
            }
            return Ok(viewModels);
        }

        [HttpGet("GetByGroupId/{id}")]
        public ActionResult GetByGroupId(Guid id) //<-- mean?? get role by group -> return roles
        {
            var roleOfGroups = _roleOfGroupService.GetRoleOfGroups(_=>_.GroupId.Equals(id));
            var viewModels = new List<RoleOfGroupViewModel>();
            foreach (var roleOfGroup in roleOfGroups)
            {
                var viewModel = new RoleOfGroupViewModel(); // <-- wrong 
                viewModel = roleOfGroup.Adapt(viewModel);
                viewModels.Add(viewModel);
            }
            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            try
            {
                var roleOfGroup = _roleOfGroupService.GetRoleOfGroup(id);
                if (roleOfGroup == null)
                {
                    return NotFound();
                }
                var viewModel = new RoleOfGroupViewModel();
                viewModel = roleOfGroup.Adapt(viewModel);
                return Ok(viewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] RoleOfGroupCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var groupId = vm.GroupId;
                foreach (var roleId in vm.RoleIds)
                {
                    _roleOfGroupService.CreateRoleOfGroup(new HsRoleOfGroup{GroupId = groupId, RoleId = roleId});
                }
                
                _roleOfGroupService.SaveRoleOfGroup();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] RoleOfGroupViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var roleOfGroup = new HsRoleOfGroup();
            roleOfGroup = vm.Adapt(roleOfGroup);
            try
            {
                _roleOfGroupService.EditRoleOfGroup(roleOfGroup);
                _roleOfGroupService.SaveRoleOfGroup();
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
                _roleOfGroupService.RemoveRoleOfGroup(id);
                _roleOfGroupService.SaveRoleOfGroup(); ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}