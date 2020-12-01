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
    public class PermissionOfRoleController : Controller
    {
        private readonly IPermissionOfRoleService _permissionOfRoleService;

        public PermissionOfRoleController(IPermissionOfRoleService permissionOfRoleService)
        {
            _permissionOfRoleService = permissionOfRoleService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var permissionOfRoles = _permissionOfRoleService.GetPermissionOfRoles();
            var viewModels = new List<PermissionOfRoleViewModel>();
            foreach (var permissionOfRole in permissionOfRoles)
            {
                var viewModel = new PermissionOfRoleViewModel();
                viewModel = permissionOfRole.Adapt(viewModel);
                viewModels.Add(viewModel);
            }
            return Ok(viewModels);
        }

        [HttpGet("GetByRoleId/{id}")]
        public ActionResult GetByRoleId(Guid id)
        {
            try
            {
                var result = new List<PermissionOfRoleViewModel>();
                var permissionOfRoles = _permissionOfRoleService.GetPermissionOfRoles(_=>_.RoleId.Equals(id));
                foreach (var permissionOfRole in permissionOfRoles)
                {
                    result.Add(permissionOfRole.Adapt<PermissionOfRoleViewModel>());
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            try
            {
                var permissionOfRole = _permissionOfRoleService.GetPermissionOfRole(id);
                if (permissionOfRole == null)
                {
                    return NotFound();
                }
                var viewModel = new PermissionOfRoleViewModel();
                viewModel = permissionOfRole.Adapt(viewModel);
                return Ok(viewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] PermissionOfRoleCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                foreach (var item in vm.PermissionId)
                {
                    _permissionOfRoleService.CreatePermissionOfRole(new HsPermissionOfRole { RoleId = vm.RoleId, PermissionId = item });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            _permissionOfRoleService.SavePermissionOfRole();
            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] PermissionOfRoleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var permissionOfRole = new HsPermissionOfRole();
            permissionOfRole = vm.Adapt(permissionOfRole);
            try
            {
                _permissionOfRoleService.EditPermissionOfRole(permissionOfRole);
                _permissionOfRoleService.SavePermissionOfRole();
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
                _permissionOfRoleService.RemovePermissionOfRole(id);
                _permissionOfRoleService.SavePermissionOfRole(); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}