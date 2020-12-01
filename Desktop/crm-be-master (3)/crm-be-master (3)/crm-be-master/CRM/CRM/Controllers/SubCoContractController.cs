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
    public class SubCoContractController : ControllerBase
    {
        private readonly ISubCoContractService _subCoContractService;
        private readonly ISubCoContractServiceItemService _subCoContractServiceItemService;
        private readonly ICoContractTelServiceService _coContractTelServiceService;

        public SubCoContractController(ISubCoContractService subCoContractService, ISubCoContractServiceItemService subCoContractServiceItemService, ICoContractTelServiceService coContractTelServiceService)
        {
            _subCoContractService = subCoContractService;
            _subCoContractServiceItemService = subCoContractServiceItemService;
            _coContractTelServiceService = coContractTelServiceService;
        }

        [HttpGet]
        public ActionResult get()
        {
            var result = _subCoContractService.GetSubCoContracts()
                .Select(c => c.Adapt<SubCoContractVM>()).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult getContract(Guid id)
        {
            SubCoContract contract = _subCoContractService.GetSubCoContract(id);
            if (contract == null) return NotFound();
            var result = contract.Adapt <SubCoContractVMDetail>();
            result.Services = new List<SubCoContractServiceItemVM>();
            result.Services = contract.SubServices.Select(s => s.Adapt<SubCoContractServiceItemVM>()).ToList();
            return Ok(result);

        }

        [HttpPost]
        public ActionResult post(SubCoContractCM model)
        {
            try
            {
                var subContract = model.Adapt<SubCoContract>();
                _subCoContractService.CreateSubCoContract(subContract);
                foreach (var item in model.Services)
                {
                    var serviceItem = item.Adapt<SubCoContractServiceItem>();
                    var coCoTelService = _coContractTelServiceService.GetCoContractTelService(item.CoContractTelServiceId);
                    if (coCoTelService == null) return NotFound(new { Message = item.CoContractTelServiceId.ToString()});
                    serviceItem.CoContractTelServiceId = coCoTelService.Id;
                    serviceItem.ServiceId = coCoTelService.ServiceId;
                    serviceItem.SubContractId = subContract.Id;
                    _subCoContractServiceItemService.CreateSubCoContractServiceItem(serviceItem);
                }
                _subCoContractService.SaveChange();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Messages = ex.Message });
            }
            
        }

        [HttpPut]
        public ActionResult update(SubCoContractUM model)
        {
            try
            {
                var sub = _subCoContractService.GetSubCoContract(model.Id);
                if (sub == null) return NotFound();
                sub = model.Adapt(sub);
                var serviceIdNotUse = UpdateSubCoContractServiceItem(model.Services, sub);
                _subCoContractService.UpdateSubCoContract(sub);
                _subCoContractService.SaveChange();

                //Xóa các service không còn tồn tại
                foreach (var removeService in serviceIdNotUse)
                {
                    _subCoContractServiceItemService.DeleteSubCoContractServiceItem(removeService);
                    _subCoContractServiceItemService.SaveChange();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            
        }
        [HttpDelete("{id}")]
        public ActionResult Delete (Guid id)
        {
            try
            {
                var subContract = _subCoContractService.GetSubCoContract(id);
                if (subContract == null) return NotFound();
                _subCoContractService.DeleteSubCoContract(subContract);
                _subCoContractService.SaveChange();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            
        }

        private List<Guid> UpdateSubCoContractServiceItem(List<SubCoContractServiceItemVM> services,SubCoContract subCoContract)
        {
            var usedIds = new List<Guid>();
            foreach (var service in services)
            {
                var subCoContractServiceItem = subCoContract.SubServices.Where(c => c.Id == service.Id).FirstOrDefault();
                if (subCoContractServiceItem == null) //create
                {
                    var itemcreated = service.Adapt<SubCoContractServiceItemCM>().Adapt<SubCoContractServiceItem>();
                    var coCoTelService = _coContractTelServiceService.GetCoContractTelService(service.CoContractTelServiceId);
                    if (coCoTelService == null) throw new Exception(service.CoContractTelServiceId.ToString());
                    itemcreated.CoContractTelServiceId = coCoTelService.Id;
                    itemcreated.SubCoContract = subCoContract;
                    itemcreated.ServiceId = coCoTelService.ServiceId;
                    _subCoContractServiceItemService.CreateSubCoContractServiceItem(itemcreated);
                    subCoContract.SubServices.Add(itemcreated);
                    usedIds.Add(itemcreated.Id);
                }
                else //updated
                {
                    subCoContractServiceItem = service.Adapt(subCoContractServiceItem);
                    var coCoTelService = _coContractTelServiceService.GetCoContractTelService(service.CoContractTelServiceId);
                    subCoContractServiceItem.ServiceId = coCoTelService.ServiceId;
                    subCoContractServiceItem.CoContractTelServiceId = coCoTelService.Id;
                    usedIds.Add(subCoContractServiceItem.Id);
                }
            }
            return subCoContract.SubServices.Where(c => !usedIds.Contains(c.Id)).Select(c => c.Id).ToList();
        }

        [HttpDelete("SubCoContractItem/{id}")]
        public ActionResult DeteleItem(Guid id)
        {
            var item = _subCoContractServiceItemService.GetSubCoContractServiceItem(id);
            if (item == null) NotFound();
            _subCoContractServiceItemService.DeleteSubCoContractServiceItem(item.Id);
            return Ok();
        }
    }
}