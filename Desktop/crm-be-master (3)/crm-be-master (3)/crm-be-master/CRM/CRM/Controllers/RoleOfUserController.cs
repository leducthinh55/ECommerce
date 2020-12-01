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
    public class RoleOfUserController : Controller
    {
        private readonly IRoleOfUserService _roleOfUserService;

        public RoleOfUserController(IRoleOfUserService roleOfUserService)
        {
            _roleOfUserService = roleOfUserService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var roleOfUsers = _roleOfUserService.GetRoleOfUsers();
            var viewModels = new List<RoleOfUserViewModel>();
            foreach (var roleOfUser in roleOfUsers)
            {
                var viewModel = new RoleOfUserViewModel();
                viewModel = roleOfUser.Adapt(viewModel);
                viewModels.Add(viewModel);
            }
            return Ok(viewModels);
        }

        [HttpGet("GetByUserId/{id}")]
        public ActionResult GetByUserId(string id)
        {
            var roleOfUsers = _roleOfUserService.GetRoleOfUsers(_=>_.UserId.Equals(id));
            var viewModels = new List<RoleOfUserViewModel>();
            foreach (var roleOfUser in roleOfUsers)
            {
                var viewModel = new RoleOfUserViewModel();
                viewModel = roleOfUser.Adapt(viewModel);
                viewModels.Add(viewModel);
            }
            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            try
            {
                var roleOfUser = _roleOfUserService.GetRoleOfUser(id);
                if (roleOfUser == null)
                {
                    return NotFound();
                }
                var viewModel = new RoleOfUserViewModel();
                viewModel = roleOfUser.Adapt(viewModel);
                return Ok(viewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] RoleOfUserCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = vm.UserId;
                foreach (var roleId in vm.RoleIds)
                {
                    _roleOfUserService.CreateRoleOfUser(new HsRoleOfUser { UserId = userId, RoleId = roleId });
                }
                _roleOfUserService.SaveRoleOfUser();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] RoleOfUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var roleOfUser = new HsRoleOfUser();
            roleOfUser = vm.Adapt(roleOfUser);
            try
            {
                _roleOfUserService.EditRoleOfUser(roleOfUser);
                _roleOfUserService.SaveRoleOfUser();
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
                _roleOfUserService.RemoveRoleOfUser(id);
                _roleOfUserService.SaveRoleOfUser(); ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}