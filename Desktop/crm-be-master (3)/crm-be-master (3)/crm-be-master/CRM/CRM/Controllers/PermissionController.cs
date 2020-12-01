using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var permissions = _permissionService.GetPermissions(_=>!_.IsDeleted);
            var viewModels = new List<PermissionViewModel>();
            foreach (var permission in permissions)
            {
                var viewModel = new PermissionViewModel();
                viewModel = permission.Adapt(viewModel);
                viewModels.Add(viewModel);
            }
            viewModels.Reverse();
            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            try
            {
                var permission = _permissionService.GetPermission(id);
                if (permission == null)
                {
                    return NotFound();
                }
                var viewModel = new PermissionViewModel();
                viewModel = permission.Adapt(viewModel);
                return Ok(viewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/Users")]
        public ActionResult GetUsersByPermissionId(Guid id)
        {
            try
            {
                var permission = _permissionService.GetPermission(id);
                if (permission == null)
                {
                    return NotFound();
                }

                var result = _permissionService.GetUsersByPermission(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] PermissionCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var permission = new HsPermission();
            permission = vm.Adapt(permission);
            try
            {
                _permissionService.CreatePermission(permission);
                _permissionService.SavePermission();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] PermissionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var permission = new HsPermission();
            permission = vm.Adapt(permission);
            try
            {
                _permissionService.EditPermission(permission);
                _permissionService.SavePermission();
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
                _permissionService.RemovePermission(id);
                _permissionService.SavePermission(); ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}