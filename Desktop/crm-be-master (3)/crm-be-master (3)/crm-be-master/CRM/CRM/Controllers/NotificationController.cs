using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.Hubs;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Hangfire;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<CenterHub> _hubContext;
        private readonly UserManager<HsUser> _userManager;
        private readonly IHubUserConnectionService _hubService;
        private readonly IHsNotificationService _notiService;
        private readonly IPermissionService _permissionService;

        public NotificationController(IHubContext<CenterHub> hubContext, UserManager<HsUser> userManager, IHubUserConnectionService hubService, IHsNotificationService notiService, IPermissionService permissionService)
        {
            _hubContext = hubContext;
            _userManager = userManager;
            _hubService = hubService;
            _notiService = notiService;
            _permissionService = permissionService;
        }

        [HttpPost]
        public IActionResult Post([FromQuery]string userName, [FromBody]NotificationCM model)
        {
            var _user =  _userManager.FindByNameAsync(userName).Result;
            if(_user != null)
            {
                var notification = new HsNotification
                {
                    Title = model.Title,
                    Type = model.Type,
                    Body = model.Body,
                    UserId = _user.Id,
                    NData = model.NData == null ? null : JsonConvert.SerializeObject(model.NData),
                    IsSeen = false,
                    DateCreated = DateTime.Now
                };
                _notiService.CreateHsNotification(notification);
                _notiService.SaveHsNotification();

                var connections = _hubService.GetHubUserConnections(_ => _.UserId.Equals(_user.Id));
                _hubContext.Clients.Clients(connections.Select(_=>_.Connection).ToList())
                            .SendAsync("Notify", JsonConvert.SerializeObject(notification.Adapt<NotificationVM>()));
            }
            return Ok();
        }

        [HttpPost("SendNotiByPermission")]
        public IActionResult SendNotiByPermission([FromQuery]Guid permissionId, [FromBody]NotificationCM model)
        {
            BackgroundJob.Enqueue(
                    () => SendNotiAsync(permissionId, model));
            return Ok();
        }

        public async Task SendNotiAsync(Guid permissionId, NotificationCM model)
        {
            List<string> connections = new List<string>();
            try
            {
                var permission = _permissionService.GetPermission(permissionId);
                if (permission == null)
                {
                    return;
                }

                var userIds = _permissionService.GetUsersByPermission(permissionId);

                foreach (var userId in userIds)
                {
                    try
                    {
                        var notification = CreateHsNotification(userId, model);
                        _notiService.CreateHsNotification(notification);
                        _notiService.SaveHsNotification();

                        connections = connections.Union(_hubService.GetHubUserConnections(_ => _.UserId.Equals(userId))
                                        .Select(_ => _.Connection).ToList()).ToList();
                    }
                    catch { continue; }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            //Send notification
            var _notification = new NotificationVM
            {
                Title = model.Title,
                Type = model.Type,
                Body = model.Body,
                NData = model.NData == null ? null : JsonConvert.SerializeObject(model.NData),
                IsSeen = false,
                DateCreated = DateTime.Now
            };

            _hubContext.Clients.Clients(connections)
                            .SendAsync("Notify", JsonConvert.SerializeObject(_notification.Adapt<NotificationVM>()));
        }
        private HsNotification CreateHsNotification(string userId, NotificationCM model)
        {
            return new HsNotification
            {
                Title = model.Title,
                Type = model.Type,
                Body = model.Body,
                UserId = userId,
                NData = model.NData == null ? null : JsonConvert.SerializeObject(model.NData),
                IsSeen = false,
                DateCreated = DateTime.Now
            };
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetNotis()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();
            var data = _notiService.GetHsNotifications(_ => _.UserId.Equals(user.Id)).OrderByDescending(_=>_.DateCreated);
            List<NotificationVM> result = new List<NotificationVM>();
            foreach (var item in data)
            {
                result.Add(item.Adapt<NotificationVM>());
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPut("ToggleSeen/{id}")]
        public ActionResult SwapIsSeen(Guid id)
        {
            try
            {
                var noti = _notiService.GetHsNotification(id);
                if (noti == null) return NotFound();
                noti.IsSeen = !noti.IsSeen;
                _notiService.EditHsNotification(noti);
                _notiService.SaveHsNotification();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}