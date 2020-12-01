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
    public class PriceHistoryController : ControllerBase
    {
        private readonly IPriceHistoryService _priceHistoryService;

        public PriceHistoryController(IPriceHistoryService priceHistoryService)
        {
            _priceHistoryService = priceHistoryService;
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            List<PriceHistoryVM> result = new List<PriceHistoryVM>();
            var data = _priceHistoryService.GetPriceHistories();
            foreach (var item in data)
            {
                result.Add(item.Adapt<PriceHistoryVM>());
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var priceHistory = _priceHistoryService.GetPriceHistory(id);
            if (priceHistory == null) return NotFound();
            return Ok(priceHistory.Adapt<PriceHistoryVM>());
        }

        [HttpPost]
        public ActionResult Post([FromBody] PriceHistoryCM priceHistoryCM)
        {
            try
            {
                var priceHistory = priceHistoryCM.Adapt<PriceHistory>();
                _priceHistoryService.CreatePriceHistory(priceHistory);
                _priceHistoryService.SaveChange();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }
        //PUT   --> Update
        [HttpPut]
        public ActionResult Put([FromBody] PriceHistoryUM priceHistoryUM)
        {
            try
            {
                var priceHistory = _priceHistoryService.GetPriceHistory(priceHistoryUM.Id);
                if (priceHistory == null) return NotFound();
                priceHistory = priceHistoryUM.Adapt(priceHistory);
                _priceHistoryService.UpdatePriceHistory(priceHistory);
                _priceHistoryService.SaveChange();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        //DELETE  -->Delete
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var priceHistory = _priceHistoryService.GetPriceHistory(id);
                if (priceHistory == null) return NotFound();
                _priceHistoryService.DeletePriceHistory(priceHistory);
                _priceHistoryService.SaveChange();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
