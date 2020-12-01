using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    public class ContractTelecomController : ControllerBase
    {
        private readonly IContractTelecomService _contractTelecomService;
        private readonly ICustomerService _customerService;
        private readonly IContractTelecomAppendixService _contractTelecomAppendixService;
        private readonly IWorkFlowHistoryService _workFlowHistoryService;

        public ContractTelecomController(IContractTelecomService contractTelecomService, ICustomerService customerService, IContractTelecomAppendixService contractTelecomAppendixService, IWorkFlowHistoryService workFlowHistoryService)
        {
            _contractTelecomService = contractTelecomService;
            _customerService = customerService;
            _contractTelecomAppendixService = contractTelecomAppendixService;
            _workFlowHistoryService = workFlowHistoryService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            //var contracts = _contractTelecomService.GetContractTelecoms();
            var result = _contractTelecomService.GetContractTelecoms().Select(s => s.Adapt<ContractTelecomVM>());
            //foreach (var contract in contracts)
            //{
            //    var customer = contract.Customer;
            //    var con = contract.Adapt<ContractTelecomVM>();
            //    con.Name = customer.Name;
            //    con.BirthDate = customer.Owner.Birthday != null ? customer.Owner.Birthday : null;
            //    //con.Phone = customer.;
            //    con.Email = customer.Email;
            //    con.Position = customer.Owner.Position;
            //    result.Add(con);
            //}
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var result = _contractTelecomService.GetContractTelecom(id).Adapt<ContractTelecomVM>();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}/Appendix")]
        public ActionResult GetAppendixByContractId(Guid id)
        {
            var contractTelecom = _contractTelecomService.GetContractTelecom(id);
            if (contractTelecom == null)
            {
                return NotFound();
            }
            var result = contractTelecom.ContractTelecomAppendices.Select(s => s.Adapt<ContractTelecomAppendixVM>());
            return Ok(result);
        }


        //private bool IsRunningContractTelecom(ContractTelecom c)
        //{
        //    var appendixs = _contractTelecomAppendixService.GetContractTelecomAppendixs(_ => _.ContractTelecomId == c.Id).ToList();
        //    foreach (var appendix in appendixs)
        //    {
        //        if (appendix.Status != -1)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private bool IsExtensionContractTelecom(ContractTelecom c)
        //{
        //    var workFlowHistories = _workFlowHistoryService.GetWorkFlowHistories().ToList();
        //    workFlowHistories = workFlowHistories.Where(_ => _.CustomerWorkFlowId == c.CustomerWorkflowId).ToList();
        //    var FormData = workFlowHistories.Where(_ => _.FormData.Contains("\"autoReneval\":\"Có\"") && _.InstanceName.Equals("Ký hợp đồng"));
        //    if (FormData != null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //[HttpPut("ExtensionContract")]
        //public ActionResult ExtensionTelecomContract()
        //{
        //    var TelecomContracts = _contractTelecomService.GetContractTelecoms().ToList();
        //    TelecomContracts = TelecomContracts.Where(_ => IsRunningContractTelecom(_)).ToList();
        //    TelecomContracts = TelecomContracts.Where(_ => IsExtensionContractTelecom(_)).ToList();

        //    foreach (var telecomContract in TelecomContracts)
        //    {
        //        var DateEnd = telecomContract.DateEnd;
        //        if (DateEnd != null && DateEnd == new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
        //        {
        //            DateEnd = DateEnd.AddYears(1);
        //            telecomContract.DateEnd = DateEnd;
        //            _contractTelecomService.EditContractTelecom(telecomContract);
        //            _contractTelecomService.SaveContractTelecom();
        //        }
        //    }
        //    return Ok();
        //}
    }
}
