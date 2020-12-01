using System;
using System.Collections.Generic;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;


namespace CRM.Controllers
{
    [Route("api/[controller]")]
    public class ProductAttributeController : ControllerBase
    {
        private readonly IProductAttributeService _productAttributeService;

        public ProductAttributeController(IProductAttributeService productAttributeService)
        {
            _productAttributeService = productAttributeService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<ProductAttributeVM> result = new List<ProductAttributeVM>();
            var data = _productAttributeService.GetAttributeServices(_=>_.IsDeleted == false);
            foreach(var item in data)
            {
                result.Add(item.Adapt<ProductAttributeVM>());
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var productAttribute = _productAttributeService.GetAttributeService(id);
            if (productAttribute == null) return NotFound();
            return Ok(productAttribute.Adapt<ProductAttributeVM>());
        }

        [HttpGet("{id}/PredefinedValues")]
        public ActionResult GetPredefinedValues(Guid id)
        {
            var productAttribute = _productAttributeService.GetAttributeService(id);
            if (productAttribute == null) return NotFound();
            List<PredefinedValueVM> result = new List<PredefinedValueVM>();
            foreach (var item in productAttribute.PredefinedValues)
            {
                result.Add(item.Adapt<PredefinedValueVM>());
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductAttributeCM productAttributeCM)
        {
            try
            {
                var productAttribute = productAttributeCM.Adapt<ProductAttribute>();
                _productAttributeService.CreateAttributeService(productAttribute);
                _productAttributeService.SaveChange();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpPut]
        public ActionResult Put([FromBody] ProductAttributeUM productAttributeUM)
        {
            try
            {
                var productAttribute = _productAttributeService.GetAttributeService(productAttributeUM.Id);
                if (productAttribute == null) return NotFound();

                productAttribute = productAttributeUM.Adapt(productAttribute);
                _productAttributeService.UpdateAttributeService(productAttribute);
                _productAttributeService.SaveChange();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (Guid id)
        {
            try
            {
                var productAttribute = _productAttributeService.GetAttributeService(id);
                if (productAttribute == null) return NotFound();
                _productAttributeService.DeleteAttributeService(productAttribute);
                _productAttributeService.SaveChange();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
