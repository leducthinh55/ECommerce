using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.Service;
using CRM.Utils;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    public class ContractTelecomAppendixController : ControllerBase
    {
        private static readonly string ERROR = "Not found {0} : {1}";
        private static readonly string TAG = "teltecomservice";
        private readonly IContractTelecomAppendixService _ContractTelecomAppendixService;
        private readonly ITelecomserviceService _telecomserviceService;
        private readonly ITelecomserviceContractAppendixService _telecomserviceContractAppendixService;

        public ContractTelecomAppendixController(IContractTelecomAppendixService contractTelecomAppendixService, ITelecomserviceService telecomserviceService, ITelecomserviceContractAppendixService telecomserviceContractAppendixService)
        {
            _ContractTelecomAppendixService = contractTelecomAppendixService;
            _telecomserviceService = telecomserviceService;
            _telecomserviceContractAppendixService = telecomserviceContractAppendixService;
        }

        //[HttpGet]
        //public ActionResult Get()
        //{
        //    var result = _ContractTelecomAppendixService.GetContractTelecomAppendixs().Select(s => s.Adapt<ContractTelecomAppendixVM>());
        //    return Ok(result);
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var contractTelecomAppendix = _ContractTelecomAppendixService.GetContractTelecomAppendix(id);
            if (contractTelecomAppendix == null)
            {
                return NotFound();
            }
            var result = contractTelecomAppendix.Adapt<ContractTelecomAppendixDetailVM>();
            result.ContractNo = contractTelecomAppendix.ContractTelecom.ContractNo;
            if (contractTelecomAppendix.Type == 0)
            {
                if (contractTelecomAppendix.TelecomserviceContractAppendices.Count > 0) result.Services = new List<TelecomserviceContractAppendixVM>();
                foreach (var telecomserviceContractAppendis in contractTelecomAppendix.TelecomserviceContractAppendices)
                {
                    var appendixDetail = telecomserviceContractAppendis.Adapt<TelecomserviceContractAppendixVM>();
                    appendixDetail.Data = JsonConvert.DeserializeObject(telecomserviceContractAppendis.Data);
                    result.Services.Add(appendixDetail);
                }
            }
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody]ContractTelecomAppendixCM model)
        {
            try
            {
                var contractTelecomAppendix = model.Adapt<ContractTelecomAppendix>();
                _ContractTelecomAppendixService.CreateContractTelecomAppendix(contractTelecomAppendix);
                if (contractTelecomAppendix.Type == 0)
                {
                    _ContractTelecomAppendixService.CreateContractTelecomAppendix(contractTelecomAppendix);
                    foreach (var TelecomserviceContractAppendixCM in model.Services)
                    {
                        var telecomservice = _telecomserviceService.GetTelecomservice(TelecomserviceContractAppendixCM.TelecomserviceId);
                        var error = string.Format(ERROR, TAG, TelecomserviceContractAppendixCM.TelecomserviceId.ToString());
                        if (telecomservice == null) return NotFound(new { Error = error });
                        _telecomserviceContractAppendixService.CreateTelecomserviceContractAppendix(new TelecomserviceContractAppendix
                        {
                            UnitAmount = TelecomserviceContractAppendixCM.UnitAmount,
                            Data = JsonConvert.SerializeObject(TelecomserviceContractAppendixCM.Data),
                            Quantity = TelecomserviceContractAppendixCM.Quantity,
                            Telecomservice = telecomservice,
                            ContractTelecomAppendix = contractTelecomAppendix,
                            DateEnd = TelecomserviceContractAppendixCM.DateEnd
                        });
                    }
                }
                _ContractTelecomAppendixService.SaveContractTelecomAppendix();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        // POST api/values
        [HttpPut]
        public ActionResult Put([FromBody]ContractTelecomAppendixUM model)
        {
            try
            {
                var contractTelecomAppendix = _ContractTelecomAppendixService.GetContractTelecomAppendix(model.Id);
                contractTelecomAppendix = model.Adapt(contractTelecomAppendix);
                if (contractTelecomAppendix.Type == 0)
                {
                    foreach (var TelecomserviceContractAppendixDM in contractTelecomAppendix.TelecomserviceContractAppendices) 
                    { 
                        _telecomserviceContractAppendixService
                            .RemoveTelecomserviceContractAppendix(TelecomserviceContractAppendixDM);
                    }
                    foreach (var TelecomserviceContractAppendixCM in model.Services)
                    {
                        var telecomservice = _telecomserviceService.GetTelecomservice(TelecomserviceContractAppendixCM.TelecomserviceId);
                        var error = string.Format(ERROR, TAG, TelecomserviceContractAppendixCM.TelecomserviceId.ToString());
                        if (telecomservice == null) return NotFound(new { Error = error });
                        _telecomserviceContractAppendixService.CreateTelecomserviceContractAppendix(new TelecomserviceContractAppendix
                        {
                            UnitAmount = TelecomserviceContractAppendixCM.UnitAmount,
                            Data = JsonConvert.SerializeObject(TelecomserviceContractAppendixCM.Data),
                            Quantity = TelecomserviceContractAppendixCM.Quantity,
                            Telecomservice = telecomservice,
                            ContractTelecomAppendix = contractTelecomAppendix,
                            DateEnd = TelecomserviceContractAppendixCM.DateEnd
                        });
                    }
                }
                _ContractTelecomAppendixService.SaveContractTelecomAppendix();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpPut("{id}/PayOff")]
        public ActionResult PayOff(Guid id)
        {
            var append = _ContractTelecomAppendixService.GetContractTelecomAppendix(id);
            if (append == null) return NotFound();
            if (append.Status == (int)ContractStatus.PAYOFF) return BadRequest();
            append.Status = (int)ContractStatus.PAYOFF;
            append.DateEnd = DateTime.Now;
            _telecomserviceContractAppendixService.SaveTelecomserviceContractAppendix();
            return Ok();
        }

        [HttpPut("{id}/DateAccept")]
        public ActionResult updateDateConfirm(Guid id, [FromBody]ContractTelecomAppxDateAccept model)
        {
            var append = _ContractTelecomAppendixService.GetContractTelecomAppendix(id);
            if (append == null) return NotFound();
            if (append.DateAccept != null || append.Status == (int)ContractStatus.PAYOFF) return BadRequest();
            append.DateAccept = model.DateAccept;
            _telecomserviceContractAppendixService.SaveTelecomserviceContractAppendix();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var append = _ContractTelecomAppendixService.GetContractTelecomAppendix(id);
            if (append == null) return NotFound();
            _ContractTelecomAppendixService.RemoveContractTelecomAppendix(id);
            _ContractTelecomAppendixService.SaveContractTelecomAppendix();
            return Ok();
        }
    }
}
