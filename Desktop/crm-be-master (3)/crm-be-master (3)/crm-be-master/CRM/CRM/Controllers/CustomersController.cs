using CRM.HangfireJob;
using CRM.Model;
using CRM.Service;
using CRM.Utils;
using CRM.ViewModels;
using Hangfire;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogService _logService;
        private readonly ITransactionLogService _transactionLogService;
        private readonly ICustomerTypeService _customerTypeService;
        private readonly IContactService _contactService;
        private readonly IErrorLogService _errorLogService;
        private readonly ITestBackgroud _testBackgroud;
        private readonly UserManager<HsUser> _userManager;
        private readonly IGlobalVariableValueService _globalVariableValueService;
        private const string NULL = "NULL";

        public CustomersController(ICustomerService customerService,
          ILogService logService,
          ITransactionLogService transactionLogService,
          ICustomerTypeService customerTypeService,
           IContactService contactService,
           ITestBackgroud testBackgroud,
           UserManager<HsUser> userManager,
           IGlobalVariableValueService globalVariableValueService,
           IErrorLogService errorLogService)
        {
            _customerService = customerService;
            _transactionLogService = transactionLogService;
            _customerTypeService = customerTypeService;
            _contactService = contactService;
            _errorLogService = errorLogService;
            _testBackgroud = testBackgroud;
            _userManager = userManager;
            _globalVariableValueService = globalVariableValueService;
            _logService = logService;
        }
        [HttpGet]
        public ActionResult Get(string search = "")
        {
            var result = _customerService.GetCustomers(c => c.Name.Contains(search)).Select(c => c.Adapt<CustomerVM>()).ToList();
            foreach (var item in result)
            {
                try
                {
                    if (item.ObjectType != null) item.ObjectType = JsonConvert.DeserializeObject((String)item.ObjectType);
                    if (item.MarketType != null) item.MarketType = JsonConvert.DeserializeObject((String)item.MarketType);
                    if (item.CompanyType != null) item.CompanyType = JsonConvert.DeserializeObject((String)item.CompanyType);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = "Can't convert customer : " + item.Id + " With error : " + ex.Message } );
                }
                
            }
            return Ok(result);
            //try
            //{
            //    TestBackgroud backgroud = new TestBackgroud(_customerService, _customerTypeService, _errorLogService);
            //    backgroud.UpdateCustomers(c => c.Personnel.TotalEmployee != (c.Personnel.TotalEmployeeInSide + c.Personnel.TotalEmployeeOutSide), User.Identity.Name);
            //    return Ok();
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.InnerException);
            //}

        }

        [HttpGet("Paging")]
        public ActionResult Get(string name, string address, int index = 1, int pageSize = 10)
        {
            name = name == null ? "" : name;
            address = address == null ? "" : address;

            var result = _customerService._GetCustomers(c => c.Name.Contains(name));
            var data = result.ToPageList<CustomerVM, Customer>(index, pageSize);
            foreach (var item in data.List)
            {
                try
                {
                    if (item.ObjectType != null) item.ObjectType = JsonConvert.DeserializeObject((String)item.ObjectType);
                    if (item.MarketType != null) item.MarketType = JsonConvert.DeserializeObject((String)item.MarketType);
                    if (item.CompanyType != null) item.CompanyType = JsonConvert.DeserializeObject((String)item.CompanyType);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = "Can't convert customer : " + item.Id + " With error : " + ex.Message });
                }
            }
            return Ok(data);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public ActionResult<Customer> GetById(Guid id, DateTime? selectedDate)
        {
            var customer = _customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            if (selectedDate != null)
            {
                _customerService.AdaptChange(customer, selectedDate.Value);
            }
            var result = customer.Adapt<CustomerDetailVM>();
            try
            {
                if (customer.ObjectType != null && !customer.ObjectType.Equals(NULL)) result.ObjectType = JsonConvert.DeserializeObject(customer.ObjectType);
                if (customer.MarketType != null && !customer.MarketType.Equals(NULL)) result.MarketType = JsonConvert.DeserializeObject(customer.MarketType);
                if (customer.CompanyType != null && !customer.CompanyType.Equals(NULL)) result.CompanyType = JsonConvert.DeserializeObject(customer.CompanyType);
                if (customer.MarketActive != null && !customer.MarketActive.Equals(NULL)) result.MarketActive = JsonConvert.DeserializeObject(customer.MarketActive);
            }
            catch (Exception ex)
            {
                    return BadRequest(new { Message = "Can't convert customer : " + customer.Id + " With error : " + ex.Message });
            }
            result.SignDayActivities = customer.Contracts.Select(c => c.StartDate).FirstOrDefault();
            return Ok(result);
        }

        [HttpGet("{id}/Contacts")]
        public ActionResult GetContactByCustomerId(Guid id, string search = null)
        {
            try
            {
                var contacts = _contactService
                    .GetContacts(_ => (search == null || _.Name.ToLower().Contains(search.ToLower())) && _.CustomerId.Equals(id))
                    .Select(_ => _.Adapt<ContactVM>()).ToList();

                return Ok(contacts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]CustomerCM model)
        {
            try
            {
                var oldcustomer = _customerService.GetCustomers().Last();
                if (oldcustomer.Name == model.Name) return BadRequest();
                var customer = model.Adapt<Customer>();
                customer.IsReal = false;
                customer.CustomerTypeId = _customerTypeService.GetCustomerTypes().FirstOrDefault().Id;
                _customerService.CreateCustomer(customer, User.Identity.Name);
                return StatusCode(201, new { Id = customer.Id });
            }
            catch (Exception e)
            {
                //_customerService.RemoveCustomer(customer.Id);
                return BadRequest(e.Message);
            }
        }

        //[HttpPost("DataTest")]
        //public ActionResult PostCustomerTest([FromBody]CustomerCM model)
        //{
        //    try
        //    {
        //        var oldcustomer = _customerService.GetCustomers().Last();
        //        if (oldcustomer.Name == model.Name) return BadRequest();
        //        var customer = model.Adapt<Customer>();
        //        customer.IsReal = false;
        //        customer.CustomerTypeId = _customerTypeService.GetCustomerTypes().FirstOrDefault().Id;
        //        _customerService.CreateCustomer(customer, User.Identity.Name);
        //        return StatusCode(201, new { Id = customer.Id });
        //    }
        //    catch (Exception e)
        //    {
        //        //_customerService.RemoveCustomer(customer.Id);
        //        return BadRequest(e.Message);
        //    }
        //}

        [HttpPost("GetList")]
        public ActionResult GetByListId([FromBody] Guid[] ids)
        {
            var customers = _customerService.GetCustomers(_ => ids.Contains(_.Id)).ToList();
            var result = new List<CustomerDetailVM>();
            foreach (var c in customers)
            {
                var customer = c.Adapt<CustomerDetailVM>();
                if (c.ObjectType != null) customer.ObjectType = JsonConvert.DeserializeObject(c.ObjectType);
                result.Add(customer);
            }
            return Ok(result);
        }



        [HttpPost("LIST")]
        public ActionResult PostList([FromBody]List<Object> models)
        {
            try
            {
                _testBackgroud.InsertCustomers(models, User.Identity.Name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPost("photo/{id}")]
        public async Task<ActionResult> UploadPhoto(Guid id, IFormFile file)
        {
            string photoId = await _customerService.UploadPhoto(file);

            var customer = _customerService.GetCustomer(id);
            customer.ProfilePicture = photoId;
            _customerService.SaveCustomer();
            return Ok(customer);
        }

        [HttpPost("{id}/Contacts")]
        public ActionResult CreateContact(Guid id, [FromBody]ContactCM model)
        {
            try
            {
                var contact = model.Adapt<Contact>();
                contact.CustomerId = id;

                _contactService.CreateContact(contact);
                _contactService.SaveContact();

                return StatusCode(201, contact.Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody] CustomerUM model)
        {
            try
            {
                var customer = _customerService.GetCustomer(model.Id);
                if (customer == null) return NotFound();
                var oldCustomer = customer.Adapt<CustomerDetailVM>().Adapt<Customer>();

                customer = model.Adapt(customer);
                customer.ObjectType = model.ObjectType != null ? JsonConvert.SerializeObject(model.ObjectType) : null;
                customer.MarketType = model.MarketType != null ? JsonConvert.SerializeObject(model.MarketType) : null;
                customer.CompanyType = model.CompanyType != null ? JsonConvert.SerializeObject(model.CompanyType) : null;
                customer.MarketActive = model.MarketActive != null ? JsonConvert.SerializeObject(model.MarketActive) : null;
                _customerService.EditCustomer(customer,oldCustomer, User.Identity.Name);
                _customerService.SaveCustomer();
                
                return Ok();
            }
            catch (Exception e)
            {
                _logService.CreateLog(e, this.GetType().FullName);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}/Contacts")]
        public ActionResult UpdateContact(Guid id, [FromBody]ContactUM model)
        {
            try
            {
                var contact = _contactService
                    .GetContacts(_ => _.Id == model.Id && _.CustomerId == id)
                    .FirstOrDefault();
                if (contact == null) return NotFound();

                _contactService.EditContact(model.Adapt(contact));
                _contactService.SaveContact();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var customer = _customerService.GetCustomer(id);
                if (customer == null) return NotFound();

                _transactionLogService.UpdateTransaction(customer, null, User.Identity.Name);

                _customerService.DeleteCustomer(customer);
                _customerService.SaveCustomer();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}/Contacts/{contactId}")]
        public ActionResult DeleteContact(Guid id, Guid contactId)
        {
            try
            {
                var contact = _contactService
                    .GetContacts(_ => _.Id == contactId && _.CustomerId == id)
                    .FirstOrDefault();
                if (contact == null) return NotFound();

                _contactService.RemoveContact(contact);
                _contactService.SaveContact();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Export")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database
            await Task.Yield();
            var file = _customerService.GetExcel();

            return File(file.Stream, file.ContentType, file.FileName); 
        }
        [HttpGet("ExportWithURL")]
        public ActionResult Export(string name)
        {
            string excelName = $"CustomerList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            string downloadUrl = "Document/Files/ExportFile/" + excelName;

            //// query data from database  
            //await Task.Yield();

            BackgroundJob.Enqueue(
    () => _testBackgroud.ExportCustomerHangfire(name,downloadUrl,_userManager.GetUserAsync(User).Result));

            //_customerService.GetDownloadUrlExcel(file);
            return Ok(new { URL = downloadUrl });
        }

        [HttpGet("File")]
        public ActionResult GetFile(string filePath)
        {
            var file = _globalVariableValueService.DowloadFile(filePath).Result;
            return File(file.Stream, file.ContentType, file.FileName);
        }
        //[HttpPost("Convert")]
        //[AllowAnonymous]
        //public IActionResult Convert()
        //{
        //    _customerService.GetCustomers().Select(c =>
        //    new Contact
        //    {
        //        Address = "",
        //        BirthDate = null,
        //        CustomerId = c.Id,
        //        Email = c.DeputyMail,
        //        Name = c.DeputyName,
        //        Functional = "",
        //        Note = "",
        //        Phone = c.DeputyTel,
        //        Position = c.DeputyPosition,
        //        Gender = c.DeputyGender,
        //        Nation = c.DeputyNation,
        //    }
        //    ).ToList().ForEach(c => _contactService.CreateContact(c));
        //    _contactService.SaveContact();
        //    return Ok();
        //}

        [HttpPost("ImportWithURL")]
        public ActionResult Import(string name)
        {
            _customerService.ImportCustomerFromExcel(name);
            return Ok();
        }

    }
}