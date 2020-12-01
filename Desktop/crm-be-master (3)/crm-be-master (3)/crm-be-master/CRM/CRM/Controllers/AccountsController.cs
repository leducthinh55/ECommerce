using CRM.Helpers;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<HsUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPermissionService _permissionService;

        public AccountsController(UserManager<HsUser> userManager, IPermissionService permissionService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(String RoleName)
        {
            List<AccountVM> result = new List<AccountVM>();
            IList<HsUser> users = null;
            if (RoleName != null)
            {
                var role = await _roleManager.FindByNameAsync(RoleName);
                if (role == null) return NotFound();
                users = await _userManager.GetUsersInRoleAsync(RoleName);
            }
            else
            {
                users = _userManager.Users.ToList();
            }
            foreach (var user in users)
            {
                AccountVM item = user.Adapt<AccountVM>();
                item.RoleNames = await _userManager.GetRolesAsync(user);
                result.Add(item);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = new HsUser()
            {
                Email = model.Email,
                IsEnabled = true,
                UserName = model.UserName,
                FullName = model.FirstName + " " + model.LastName,
            };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            return new OkObjectResult(JsonConvert.SerializeObject("Account Created"));
        }
        
        [HttpPost("Roles")]
        public async Task<IActionResult> UpdateRole([FromBody]AccountRoleUM model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();
            foreach (var roleName in model.RoleNames)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if(!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                } 
            }
            return Ok();
        }

        [HttpPut("Permissions")]
        public async  Task<ActionResult> UpdatePermission()
        {
            var users =  _userManager.Users;
            foreach (var user in users)
            {
                var permissions = _permissionService.GetPermissionsByUser(user.Id).Select(p => p.Id).ToList();
                user.Permissions = JsonConvert.SerializeObject(permissions);
                await _userManager.UpdateAsync(user);
            }
            return Ok();
        }
    }
}