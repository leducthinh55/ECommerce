using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM.Data;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using CRM.Utils;
using CRM.Service.Utils;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractAppendixesController : ControllerBase
    {
        private readonly IContractAppendixService _contractAppendixService;

        public ContractAppendixesController(IContractAppendixService contractAppendixService)
        {
            _contractAppendixService = contractAppendixService;
        }
        [HttpPost]
        public ActionResult Post([FromBody]ContractAppendixCM model)
        {
            try
            {
                ContractAppendix contractAppendix = model.Adapt<ContractAppendix>();
                switch(model.Type)
                {
                    case (int)ContractAppendixType.AreaRent:
                    case (int)ContractAppendixType.AreaService:
                        contractAppendix.UnitPrice = null;
                        contractAppendix.UnitServicePrice = null;
                        break;
                    case (int)ContractAppendixType.PriceRent:
                        contractAppendix.Square = null;
                        contractAppendix.UnitServicePrice = null;
                        break;
                    case (int)ContractAppendixType.PriceService:
                        contractAppendix.Square = null;
                        contractAppendix.UnitPrice = null;
                        break;
                    default: return BadRequest();
                }
                _contractAppendixService.CreateContractAppendix(contractAppendix);
                _contractAppendixService.SaveContractAppendix();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody]ContractAppendixUM model)
        {
            ContractAppendix contractAppendix = _contractAppendixService.GetContractAppendix(model.Id);
            String Key = contractAppendix.Key; 
            if (contractAppendix == null) return NotFound();
            try
            {
                contractAppendix = model.Adapt(contractAppendix);
                if(contractAppendix.Key == null || contractAppendix.Key.Equals(""))
                {
                    contractAppendix.Key = Key; 
                }
                switch (model.Type)
                {
                    case (int)ContractAppendixType.AreaRent:
                    case (int)ContractAppendixType.AreaService:
                        contractAppendix.UnitPrice = null;
                        contractAppendix.UnitServicePrice = null;
                        break;
                    case (int)ContractAppendixType.PriceRent:
                        contractAppendix.Square = null;
                        contractAppendix.UnitServicePrice = null;
                        break;
                    case (int)ContractAppendixType.PriceService:
                        contractAppendix.Square = null;
                        contractAppendix.UnitPrice = null;
                        break;
                    default: return BadRequest();
                }
                _contractAppendixService.EditContractAppendix(contractAppendix);
                _contractAppendixService.SaveContractAppendix();
                return Ok(contractAppendix.Adapt<ContractAppendixVM>());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var appendix = _contractAppendixService.GetContractAppendix(id);
            if (appendix == null) return NotFound();
            _contractAppendixService.RemoveContractAppendix(appendix);
            _contractAppendixService.SaveContractAppendix();
            return Ok();
        }
    }
}