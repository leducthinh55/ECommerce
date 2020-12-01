using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingService _BuildingService;

        public BuildingsController(IBuildingService BuildingService)
        {
            _BuildingService = BuildingService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            List<BuildingVM> result = new List<BuildingVM>();
            var data = _BuildingService.GetBuildings(_ => !_.IsDeteled);
            foreach (var item in data)
            {
                result.Add(item.Adapt<BuildingVM>());
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var Building = _BuildingService.GetBuilding(id);
            if (Building == null && Building.IsDeteled) return NotFound();
            return Ok(Building.Adapt<BuildingDetailVM>());
        }

        [HttpPost]
        public ActionResult Create(BuildingCM Building)
        {
            try
            {
                _BuildingService.CreateBuilding(Building.Adapt<Building>(),User.Identity.Name);
                _BuildingService.SaveBuilding();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpPut]
        public ActionResult Update(BuildingUM BuildingUM)
        {
            try
            {
                var Building = _BuildingService.GetBuilding(BuildingUM.Id);
                if (Building == null) return NotFound();
                Building = BuildingUM.Adapt(Building);
                _BuildingService.EditBuilding(Building, User.Identity.Name);
                _BuildingService.SaveBuilding();
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
                var Building = _BuildingService.GetBuilding(id);
                if (Building == null) return NotFound();
                _BuildingService.RemoveBuilding(Building, User.Identity.Name);
                _BuildingService.SaveBuilding();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}