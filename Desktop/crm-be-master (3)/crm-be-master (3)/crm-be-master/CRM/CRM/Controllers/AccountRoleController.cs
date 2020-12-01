using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var username = User.Identity.Name;
            var roles = _roleManager.Roles.Select(_ => _.Adapt<RoleVM>());
            
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleCM model)
        {
             var result = await _roleManager.CreateAsync(model.Adapt<IdentityRole>());
            if(result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RoleUM model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null) return NotFound();
            role.Name = model.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}