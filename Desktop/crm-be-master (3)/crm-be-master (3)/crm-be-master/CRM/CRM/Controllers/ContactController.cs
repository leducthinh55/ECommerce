using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ICustomerService _customerService;

        public ContactController(IContactService contactService, ICustomerService customerService)
        {
            _contactService = contactService;
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult GetByPhone(string phone)
        {
            var contacts = _contactService.GetContacts().Where(_ => _.Phone.Split(',').Contains(phone));
            List<CallCenterContactVM> result = new List<CallCenterContactVM>();
            foreach (var item in contacts)
            {
                var _item = item.Adapt<CallCenterContactVM>();
                _item.Customer = item.Customer.Adapt<CustomerDetailVM>();
                result.Add(_item);
            }
            return Ok(result);
        }

        [HttpGet("Customer")]
        public ActionResult SearchCustomer(string name, string tax, string code)
        {
            name = name != null ? name : "";
            tax = tax != null ? tax : "";
            code = code != null ? code : "";

            var customers = _customerService.GetCustomers(
                _ => _.Name.Contains(name) && _.TaxCode.Contains(tax) && _.Code.Contains(code)
                ).Select(_ => _.Adapt<CustomerDetailVM>());
            return Ok(customers);
        }

        [HttpGet("All")]
        public IActionResult Get(int index = 1, int pageSize = 10, string name = null, string position = null, string customer = null)
        {
            var all = _contactService.GetContacts()
                .Where(c =>
                        (name == null || c.Name.Contains(name))
                    && (position == null || (c.Position != null && c.Position.ToLower().Contains(position)))
                    && (customer == null || (c.Customer != null && c.Customer.Name.ToLower().Contains(customer)))
                );
            var rs = all
                .Select(c =>
                    new
                    {
                        c.Id,
                        c.Name,
                        c.Position,
                        c.Phone,
                        c.Email,
                        c.Address,
                        c.BirthDate,
                        Customer = c.Customer.Name,
                    }
                )
                .Skip((index - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(new
            {
                List = rs,
                Index = index,
                Total = all.Count(),
            });
        }

        [HttpPost("GetList")]
        public IActionResult GetList([FromBody] Guid[] ids)
        {
            return Ok(_contactService.GetContacts(_ => ids.Contains(_.Id))
                .Select(c =>
                    new
                    {
                        c.Id,
                        c.Name,
                        c.Position,
                        c.Phone,
                        c.Email,
                        c.Address,
                        c.BirthDate,
                        Customer = c.Customer.Name,
                    }
                ).ToList());
        }
    }
}