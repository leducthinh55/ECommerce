using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using CRM.Model;
using CRM.Service;

namespace CRM.Hubs
{
    [Authorize]
    public class CenterHub : Hub
    {
        private IHttpContextAccessor _contextAccessor;
        private readonly UserManager<HsUser> _userManager;
        private readonly IHubUserConnectionService _hubService;
        private HttpContext _context { get { return _contextAccessor.HttpContext; } }

        public CenterHub(IHttpContextAccessor contextAccessor, UserManager<HsUser> userManager, IHubUserConnectionService hubUserConnectionService)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _hubService = hubUserConnectionService;
        }

        public override async Task OnConnectedAsync()
        {
            var username = _context.User.Identity.Name;
            var _user = _userManager.FindByNameAsync(username).Result;
            if(_user != null)
            {
                var connectionId = Context.ConnectionId;
                _hubService.CreateHubUserConnection(new HubUserConnection
                {
                    UserId = _user.Id,
                    Connection = connectionId
                });
                _hubService.SaveHubUserConnection();
                await base.OnConnectedAsync();
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = _context.User;
            _hubService.RemoveHubUserConnection(_ => _.Connection.Equals(Context.ConnectionId));
            _hubService.SaveHubUserConnection();
            await base.OnDisconnectedAsync(exception);
        }
    }
}
