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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            //var roles = _roleService.GetRoles();
            //var viewModels = new List<RoleViewModel>();
            //foreach (var role in roles)
            //{
            //    var viewModel = new RoleViewModel();
            //    viewModel = role.Adapt(viewModel);
            //    viewModels.Add(viewModel);
            //}
            //return Ok(viewModels);
            return Ok(_roleService.GetRoles().Select(n => new RoleViewModel
            {
                Id = n.Id, 
                Name = n.Name
            }));
        }

        [HttpGet("{id}", Name = "GetRole")]
        public ActionResult GetById(Guid id)
        {
            //try
            //{
            //    var role = _roleService.GetRole(id);
            //    if (role == null)
            //    {
            //        return NotFound();
            //    }
            //    var viewModel = new RoleViewModel();
            //    viewModel = role.Adapt(viewModel);
            //    return Ok(viewModel);
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e.Message);
            //}
            var role = _roleService.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            });
        }
        
        [HttpPost]
        public ActionResult Create([FromBody] RoleCreateViewModel model)
        {
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var role = new HsRole();
        //    role = vm.Adapt(role);
        //    try
        //    {
        //        _roleService.CreateRole(role);
        //        _roleService.SaveRole();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }

            
        //    var roleViewModel = new RoleViewModel();
        //    roleViewModel = role.Adapt(roleViewModel);

        //    return CreatedAtRoute("GetById", new {id = role.Id}, roleViewModel);
            var role = new HsRole();
            role = model.Adapt(role);
            _roleService.CreateRole(role);
            _roleService.SaveRole();
            return CreatedAtRoute("GetRole", new {id = role.Id}, model);
        }

        [HttpPut]
        public ActionResult Update([FromBody] RoleViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //var role = new HsRole();
            //role = vm.Adapt(role);
            //try
            //{
            //    _roleService.EditRole(role);
            //    _roleService.SaveRole();
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e.Message);
            //}

            //vm = role.Adapt(vm);
            //return Ok(vm);
            var role = _roleService.GetRole(model.Id);
            if (role == null)
            {
                return NotFound();
            }

            role = model.Adapt(role);
            _roleService.EditRole(role);
            _roleService.SaveRole();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var role = _roleService.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }
            try
            {
                _roleService.RemoveRole(id);
                _roleService.SaveRole(); ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }
    }
}