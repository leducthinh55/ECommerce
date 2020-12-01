using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        private List<MenuVM> GetSubData( Guid id)
        {
            var subData = _menuService.GetMenus(_ => _.ParentId.Equals(id));
            if (subData == null) return null;
            var list = new List<MenuVM>();
            foreach(var item in subData)
            {
                var menu = item.Adapt<MenuVM>();
                list.Add(menu);
                menu.SubMenu = GetSubData( item.Id);
            }
            return list;
        }
        [HttpGet]
        public IActionResult GetMenu()
        {
            List<GroupMenuVM> result = new List<GroupMenuVM>();
            // Change Expression to get Menu depend on Roles
            var _data = _menuService.GetMenus(_ => _.GroupName.Equals("Admin") || _.GroupName.Equals("Client"));
            foreach(var item in _data)
            {
                var menu = item.Adapt<GroupMenuVM>();
                result.Add(menu);
                menu.Items = GetSubData(item.Id);
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]MenuCM model)
        {
            try
            {
                var menu = model.Adapt<Menu>();
                _menuService.CreateMenu(menu);
                _menuService.SaveChanges();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }
    }
}