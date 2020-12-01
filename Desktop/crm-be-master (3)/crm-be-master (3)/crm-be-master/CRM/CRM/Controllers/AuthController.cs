using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CRM.Auth;
using CRM.Helpers;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<HsUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IPermissionService _permissionService;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<HsUser> userManager
                            , IJwtFactory jwtFactory
                            , IOptions<JwtIssuerOptions> jwtOptions
                            , IPermissionService permissionService,
                             IConfiguration configuration)
        {
            _permissionService = permissionService;
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _configuration = configuration;
        }


        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);

            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            var user = _userManager.FindByNameAsync(credentials.UserName + "@qtsc.com.vn").Result;

            if (user == null)
            {
                user = _userManager.FindByNameAsync(credentials.UserName).Result;
            }
            var roles = _userManager.GetRolesAsync(user).Result;
            var jwt = await Tokens.GenerateJwt(identity
                                             , _jwtFactory, user.UserName
                                             , _jwtOptions
                                             , new JsonSerializerSettings { Formatting = Formatting.Indented }
                                             , (roles != null && roles.Any()) ? roles : new List<String>());

            var permissions = _permissionService.GetPermissionsByUser(user.Id).Select(p => p.Id).ToList();
            user.Permissions = JsonConvert.SerializeObject(permissions);
            await _userManager.UpdateAsync(user);
            return new OkObjectResult(jwt);
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);

        //    if (identity == null)
        //    {
        //        return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
        //    }

        //    //if (identity.FindAll("AccountNotExistInCRM") != null)
        //    //{
        //    //    return BadRequest(Errors.AddErrorToModelState("login_failure", "Account does not exist in CRM", ModelState));
        //    //}
        //    //var user = _userManager.FindByNameAsync(credentials.UserName + "@qtsc.com.vn").Result;
        //    var user = _userManager.FindByNameAsync(credentials.UserName).Result;
        //    //if (user == null)
        //    //{
        //    //    user = _userManager.FindByNameAsync(credentials.UserName).Result;
        //    //}
        //    var roles = _userManager.GetRolesAsync(user).Result;

        //    //var jwt = await Tokens.GenerateJwt(identity
        //    //                                 , _jwtFactory, credentials.UserName
        //    //                                 , _jwtOptions
        //    //                                 , new JsonSerializerSettings { Formatting = Formatting.Indented }
        //    //                                 , (roles != null && roles.Any()) ? roles : new List<String>());

        //    var jwt = await Tokens.GenerateJwt(identity
        //                                     , _jwtFactory, user.UserName
        //                                     , _jwtOptions
        //                                     , new JsonSerializerSettings { Formatting = Formatting.Indented }
        //                                     , (roles != null && roles.Any()) ? roles : new List<String>());

        //    var permissions = _permissionService.GetPermissionsByUser(user.Id).Select(p => p.Id).ToList();
        //    user.Permissions = JsonConvert.SerializeObject(permissions);
        //    await _userManager.UpdateAsync(user);
        //    return new OkObjectResult(jwt);
        //}

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            var checkAD = AuthenticateUser(userName, password);

            if (!checkAD)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }
            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null)
            {
                userName = userName + "@qtsc.com.vn";
                userToVerify = await _userManager.FindByNameAsync(userName);
                if (userToVerify == null)
                {
                    return await Task.FromResult<ClaimsIdentity>(null);
                }
            }

            // check the credentials
            return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userToVerify.UserName, userToVerify.Id));
        }


        //private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        //{
        //    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        //        return await Task.FromResult<ClaimsIdentity>(null);

        //    // get the user to verifty
        //    var userToVerify = await _userManager.FindByNameAsync(userName);

        //    if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

        //    // check the credentials
        //    if (await _userManager.CheckPasswordAsync(userToVerify, password))
        //    {
        //        return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
        //    }

        //    // Credentials are invalid, or account doesn't exist
        //    return await Task.FromResult<ClaimsIdentity>(null);
        //}

        [HttpGet("Permissions")]
        [Authorize]
        public ActionResult GetPermissions()
        {
            var username = User.Identity.Name;
            var _user = _userManager.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if(_user == null)
            {
                return NotFound();
            }
            return Ok(JsonConvert.DeserializeObject<List<Guid>>( _user.Permissions));
        }

        [Authorize]
        [HttpGet("info")]
        public ActionResult GetUserInfo()
        {
            return Ok();
        }

        private bool AuthenticateUser(string userName, string password)
        {
            bool ret = false;
            String domainName = _configuration.GetValue<String>("DomainAD");
            try
            {
                DirectoryEntry de = new DirectoryEntry("LDAP://" + domainName,
                                    userName, password);
                DirectorySearcher dsearch = new DirectorySearcher(de);
                SearchResult results = null;

                results = dsearch.FindOne();
                ret = true;
            }
            catch
            {
                ret = false;
            }

            return ret;
        }
    }
}