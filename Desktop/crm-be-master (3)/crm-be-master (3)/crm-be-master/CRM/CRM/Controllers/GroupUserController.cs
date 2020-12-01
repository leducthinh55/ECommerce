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
    public class GroupUserController : Controller
    {
        private readonly IGroupUserService _groupUserService;

        public GroupUserController(IGroupUserService groupUserService)
        {
            _groupUserService = groupUserService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var groupUsers = _groupUserService.GetGroupUsers();
            var viewModels = new List<GroupUserViewModel>();
            foreach (var groupUser in groupUsers)
            {
                var viewModel = new GroupUserViewModel();
                viewModel = groupUser.Adapt(viewModel);
                viewModels.Add(viewModel);
            }
            return Ok(viewModels);
        }

        [HttpGet("GetByGroupId/{id}")]
        public ActionResult GetByGroupId(Guid id)
        {
            var groupUsers = _groupUserService.GetGroupUsers(_ => _.GroupId.Equals(id));
            var viewModels = new List<GroupUserViewModel>();
            foreach (var groupUser in groupUsers)
            {
                var viewModel = new GroupUserViewModel();
                viewModel = groupUser.Adapt(viewModel);
                viewModels.Add(viewModel);
            }
            return Ok(viewModels);
        }

        [HttpGet("GetByUserId/{id}")]
        public ActionResult GetByUserId(string id)
        {
            var groupUsers = _groupUserService.GetGroupUsers(_ => _.UserId.Equals(id));
            var viewModels = new List<GroupUserViewModel>();
            foreach (var groupUser in groupUsers)
            {
                var viewModel = new GroupUserViewModel();
                viewModel = groupUser.Adapt(viewModel);
                viewModels.Add(viewModel);
            }
            return Ok(viewModels);
        }


        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            try
            {
                var groupUser = _groupUserService.GetGroupUser(id);
                if (groupUser == null)
                {
                    return NotFound();
                }
                var viewModel = new GroupUserViewModel();
                viewModel = groupUser.Adapt(viewModel);
                return Ok(viewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] IEnumerable<GroupUserCreateViewModel> viewModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                foreach (var viewModel in viewModels)
                {
                    var groupId = viewModel.GroupId;
                    var userId = viewModel.UserId;
                    _groupUserService.CreateGroupUser(new HsGroupUser { GroupId = groupId, UserId = userId });
                }
                _groupUserService.SaveGroupUser();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] GroupUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var groupUser = new HsGroupUser();
            groupUser = vm.Adapt(groupUser);
            try
            {
                _groupUserService.EditGroupUser(groupUser);
                _groupUserService.SaveGroupUser();
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
                _groupUserService.RemoveGroupUser(id);
                _groupUserService.SaveGroupUser(); ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}