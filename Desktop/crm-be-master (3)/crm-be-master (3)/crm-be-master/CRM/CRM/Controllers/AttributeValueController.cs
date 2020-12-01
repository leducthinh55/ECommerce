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
    [ApiController]
    public class AttributeValueController : Controller
    {
        private readonly IAttributeValueService _attributeValueService;

        public AttributeValueController(IAttributeValueService attributeValueService)
        {
            _attributeValueService = attributeValueService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _attributeValueService.GetAttributeValues();
            List<AttributeValueViewModel> result = new List<AttributeValueViewModel>();
            foreach(var item in data)
            {
                result.Add(item.Adapt<AttributeValueViewModel>());
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var attributeValue = _attributeValueService.GetAttributeValue(id);
            return Ok(attributeValue.Adapt<AttributeValueViewModel>());
        }

        [HttpPost]
        public ActionResult Create([FromBody] AttributeValueCreateModel vm)
        {
            
            try
            {
                var attributeValue = vm.Adapt<AttributeValue>();
                _attributeValueService.CreateAttributeValue(attributeValue);
                _attributeValueService.SaveAttributeValue();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpPut]
        public ActionResult Put([FromBody]AttributeValueUM vm)
        {
            try
            {
                var attributeValue = _attributeValueService.GetAttributeValue(vm.Id);
                if (attributeValue == null) return NotFound();
                attributeValue = vm.Adapt(attributeValue);
                _attributeValueService.EditAttributeValue(attributeValue);
                _attributeValueService.SaveAttributeValue(); 
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
                _attributeValueService.RemoveAttributeValue(id);
                _attributeValueService.SaveAttributeValue(); ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteByProductAttr(AttributeValueDM dm)
        {
            try
            {
                var attributeValue = _attributeValueService.GetAttributeValue(_=>_.ProductId.Equals(dm.ProductId)
                                                                                && _.ProductAttributeId.Equals(dm.ProductAttributeId));
                if (attributeValue == null) return NotFound();
                _attributeValueService.RemoveAttributeValue(attributeValue) ;
                _attributeValueService.SaveAttributeValue(); ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}