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
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CareHistoriesController : ControllerBase
    {
        private readonly ICareHistoryService _careHistoryService;

        public CareHistoriesController(ICareHistoryService careHistoryService)
        {
            _careHistoryService = careHistoryService;
        }

        [HttpGet("/api/Customers/{customerId}/CareHistories")]
        public IActionResult Get(Guid customerId) => Ok(_careHistoryService.GetCareHistorys(c => c.CustomerId.Equals(customerId)).Select(c => c.Adapt<CareHistoryViewModel>()).ToList());

        [HttpPost("/api/Customers/{customerId}/CareHistories")]
        public IActionResult Post(Guid customerId, [FromBody] CareHistoryCreateModel model)
        {
            var careHistory = model.Adapt<CareHistory>();
            careHistory.CustomerId = customerId;
            _careHistoryService.CreateCareHistory(careHistory);
            _careHistoryService.SaveCareHistory();
            return Ok(careHistory.Adapt<CareHistoryViewModel>());
        }

        [HttpPut("/api/Customers/{customerId}/CareHistories")]
        public IActionResult Put([FromBody]CareHistoryViewModel model)
        {
            var e = _careHistoryService.GetCareHistory(model.Id);
            model.Adapt(e);
            _careHistoryService.EditCareHistory(e);
            _careHistoryService.SaveCareHistory();
            return Ok();
        }

        [HttpDelete("/api/Customers/{customerId}/CareHistories")]
        public IActionResult Delete(Guid customerId, Guid id)
        {
            var e = _careHistoryService.GetCareHistory(id);
            _careHistoryService.RemoveCareHistory(e);
            _careHistoryService.SaveCareHistory();
            return Ok();
        }
    }
}