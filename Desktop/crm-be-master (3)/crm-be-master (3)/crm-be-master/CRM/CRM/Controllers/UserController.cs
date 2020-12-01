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
    public class UserController : ControllerBase
    {
        private readonly UserManager<HsUser> _userManager;
        private readonly IGroupUserService _groupUserService;

        public UserController(UserManager<HsUser> userManager, IGroupUserService groupUserService)
        {
            _userManager = userManager;
            _groupUserService = groupUserService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            List<UserViewModel> result = new List<UserViewModel>();
            var data = _userManager.Users.ToList();
            foreach (var item in data)
            {
                result.Add(item.Adapt<UserViewModel>());
            }
            return Ok(result);
        }

        [HttpGet("GetByUsername")]
        public async Task<ActionResult> Get(String username)
        {
            try
            {
                var result = await _userManager.FindByNameAsync(username) ;
                return Ok(result.Adapt<UserViewModel>());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/Groups")]
        public ActionResult GetGroupsByUserId(string id)
        {
            var groupUsers = _groupUserService.GetGroupUsers(_ => _.UserId.Equals(id));
            var result = new List<GroupViewModel>();
            foreach (var groupUser in groupUsers)
            {
                var viewModel = groupUser.Adapt<GroupViewModel>();
                result.Add(viewModel);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]UserCreateModel userCM)
        {
            try
            {
                var currentUser = await _userManager.CreateAsync(userCM.Adapt<HsUser>(), userCM.Password);
                if(currentUser.Succeeded)
                {
                    return StatusCode(201);
                }
                else
                {
                    return BadRequest(currentUser.Errors);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody]string value)
        {
            return Ok("Not finish code yet !");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var data = await _userManager.FindByIdAsync(id);
                var result = await _userManager.DeleteAsync(data);
                if(result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
