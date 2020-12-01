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
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IRoleOfGroupService _roleOfGroupService;
        private readonly IGroupUserService _groupUserService;

        public GroupController(IGroupService groupService, IRoleOfGroupService roleOfGroupService, IGroupUserService groupUserService)
        {
            _groupService = groupService ;
            _roleOfGroupService = roleOfGroupService;
            _groupUserService = groupUserService ;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var groups = _groupService.GetGroups();
            var viewModels = new List<GroupViewModel>();
            foreach (var group in groups)
            {
                var viewModel = new GroupViewModel();
                viewModel = group.Adapt(viewModel);
                viewModels.Add(viewModel);
            }
            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            try
            {
                var group = _groupService.GetGroup(id);
                if (group == null)
                {
                    return NotFound();
                }
                var viewModel = new GroupViewModel();
                viewModel = group.Adapt(viewModel);
                return Ok(viewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/Roles")]
        public ActionResult GetRolesByGroupId(Guid id)
        {
            try
            {
                var result = new List<RoleViewModel>();
                var roleOfGroups = _roleOfGroupService.GetRoleOfGroups(_ => _.GroupId.Equals(id));
                foreach (var roleOfGroup in roleOfGroups)
                {
                    var viewModel = roleOfGroup.Adapt<RoleViewModel>();
                    result.Add(viewModel);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/Users")]
        public ActionResult GetUsersByGroupId(Guid id)
        {
            try
            {
                var result = new List<UserViewModel>();
                var groupUsers = _groupUserService.GetGroupUsers(_ => _.GroupId.Equals(id));
                foreach (var groupUser in groupUsers)
                {
                    var viewModel = groupUser.Adapt<UserViewModel>();
                    result.Add(viewModel);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] GroupCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var group = new HsGroup();
            group = vm.Adapt(group);
            try
            {
                _groupService.CreateGroup(group);
                _groupService.SaveGroup();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] GroupViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var group = new HsGroup();
            group = vm.Adapt(group);
            try
            {
                _groupService.EditGroup(group);
                _groupService.SaveGroup();
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
                _groupService.RemoveGroup(id);
                _groupService.SaveGroup(); ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}