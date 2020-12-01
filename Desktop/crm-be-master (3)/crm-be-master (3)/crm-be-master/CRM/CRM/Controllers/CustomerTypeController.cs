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
using Newtonsoft.Json;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypeController : ControllerBase
    {
        private readonly ICustomerTypeService _customerTypeService;
        private readonly ICustomerService _customerService;

        public CustomerTypeController(ICustomerTypeService customerTypeService, ICustomerService customerService)
        {
            _customerTypeService = customerTypeService;
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult GetCustomerTypes()
        {
            var data = _customerTypeService.GetCustomerTypes();
            List<CustomerTypeVM> result = new List<CustomerTypeVM>();
            foreach (var item in data)
            {
                result.Add(item.Adapt<CustomerTypeVM>());
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult CreateCustomerType(CustomerTypeCM model)
        {
            try
            {
                _customerTypeService.CreateCustomerType(model.Adapt<CustomerType>());
                _customerTypeService.SaveChanges();
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}