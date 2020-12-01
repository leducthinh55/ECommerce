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

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CooperationContractController : ControllerBase
    {
        private readonly ICooperationContractService _contractService;
        private readonly ICoContractTelServiceService _serviceService;
        private readonly ITelecomserviceService _telecomservice;
        private readonly IEventLogFileService _fileService;

        public CooperationContractController(ICooperationContractService contractService, ICoContractTelServiceService serviceService, ITelecomserviceService telecomservice, IEventLogFileService fileService)
        {
            _contractService = contractService;
            _serviceService = serviceService;
            _telecomservice = telecomservice;
            _fileService = fileService;
        }

        [HttpGet]
        public ActionResult getContracts()
        {
            var result = _contractService.GetCooperationContracts().Select(c => c.Adapt<CooperationContractVM>()).OrderByDescending(c => c.DateStart).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult getContract(Guid id)
        {
            CooperationContract contract = _contractService.GetCooperationContract(id);
            if (contract == null) return NotFound();
            var result = contract.Adapt<CooperationContractVMDetail>();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult createContract([FromBody]CooperationContractCM model)
        {
            var contract = model.Adapt<CooperationContract>();
            _contractService.CreateCooperationContract(contract);
            _contractService.SaveChange();
            return StatusCode(201);
        }

        [HttpPut]
        public ActionResult UpdateContract([FromBody] CooperationContractVMDetail model)
        {
            var contract = model.Adapt<CooperationContract>();
            _contractService.UpdateCooperationContract(contract);
            _contractService.SaveChange();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DelteContract(Guid id)
        {
            var contract = _contractService.GetCooperationContract(id);
            if (contract == null) return NotFound();
            foreach (var item in contract.ServiceCMs)
            {
                _serviceService.RemoveCoContractTelService(item.Id);
            }
            _contractService.SaveChange();
            return Ok();
        }

        #region CooperationContractService
        [HttpGet("{id}/CooperationContractService")]
        public ActionResult GetServiceByCooperationContractId(Guid id)
        {
            var result = _serviceService.GetCoContractTelServices(c => c.ContractId == id).Select(c => c.Adapt<CoContractTelServiceVM>());
            return Ok(result);
        }

        [HttpGet("CooperationContractService/{id}")]
        public ActionResult GetCooperationContractServiceDetail(Guid id)
        { 
            var coContractTelService = _serviceService.GetCoContractTelService(id);
            if (coContractTelService == null) return NotFound();
            return Ok(coContractTelService.Adapt<CoContractTelServiceDetailVM>());
        }

        [HttpPost("CooperationContractService")]
        public ActionResult CreateCooperationContractService([FromBody] CoContractTelServiceCM model)
        {
            var coContractTelService = model.Adapt<CoContractTelService>();
            //check service
            if (_telecomservice.GetTelecomservice(model.ServiceId) == null) return BadRequest(new { Message = "Service Not found!" });
            //check CooperationContractId
            var contract = _contractService.GetCooperationContract(model.CooperationContractId);
            if (contract == null) return BadRequest(new { Message = "CooperationContractId Not found!" });
            //check exist
            if (contract.ServiceCMs.FirstOrDefault(c => c.ServiceId == model.ServiceId && !c.IsClosed) != null) return BadRequest(new { Message = "Service đã tồn tại và đang mở" });
            coContractTelService.ContractId = model.CooperationContractId;
            _serviceService.CreateCoContractTelService(coContractTelService);
            _serviceService.SaveCoContractTelService();
            return StatusCode(201, coContractTelService.Adapt<CoContractTelServiceVM>());
        }

        [HttpPut("CooperationContractService")]
        public ActionResult UpdateCooperationContractService([FromBody] CoContractTelServiceUM model)
        {
            var coContractTelService = _serviceService.GetCoContractTelService(model.Id);
            if (coContractTelService == null) return BadRequest(new { Message = "coContractTelService Not found!" });
            //check service
            if (_telecomservice.GetTelecomservice(model.ServiceId) == null) return BadRequest(new { Message = "Service Not found!" });
            //TODO : Check lại 
            //check exist
            var contract = coContractTelService.CooperationContract;
            if (coContractTelService.ServiceId != model.ServiceId && contract.ServiceCMs.FirstOrDefault(c => c.ServiceId == model.ServiceId && !c.IsClosed) != null) return BadRequest(new { Message = "Service đã tồn tại và đang mở" });
            try
            {
                coContractTelService = model.Adapt(coContractTelService);
                _serviceService.EditCoContractTelService(coContractTelService);
                _serviceService.SaveCoContractTelService();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            
            return Ok();
        }
        
        [HttpPut("CooperationContractService/{id}/Close")]
        public ActionResult UpdateCooperationContractServiceClose(Guid id)
        {
            var coContractTelService = _serviceService.GetCoContractTelService(id);
            if (coContractTelService == null) return BadRequest(new { Message = "coContractTelService Not found!" });
            
            try
            {
                coContractTelService.IsClosed = true;
                _serviceService.EditCoContractTelService(coContractTelService);
                _serviceService.SaveCoContractTelService();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            
            return Ok();
        }

        [HttpDelete("CooperationContractService/{id}")]
        public ActionResult DeleteCooperationContractService(Guid id)
        {
            try
            {
                var service = _serviceService.GetCoContractTelService(id);
                if (service == null) return NotFound();
                var link = service.AppendixLink;
                _serviceService.RemoveCoContractTelService(service);
                if (!String.IsNullOrEmpty(link))
                {
                    _fileService.DeleteFile(link);
                }
                _serviceService.SaveCoContractTelService();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            return Ok();
        }

        [HttpGet("File")]
        public async Task<ActionResult> GetFileAsync(string appendixLink)
        {
            try
            {
                var file = await _fileService.DownloadFile(appendixLink);
                return File(file.Stream, file.ContentType);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPut("CooperationContractService/{id}/File")]
        public ActionResult UploadFile(IFormFile file, Guid id)
        {
            var coContractTelService = _serviceService.GetCoContractTelService(id);
            if (coContractTelService == null) return BadRequest(new { Message = "coContractTelService Not found; Pls create CooperationContractService First!" });
            try
            {
                var filename = _fileService.UploadFile(file, FilePath.CooperationContractFile);
                if (coContractTelService.AppendixLink != null)
                {
                    _fileService.DeleteFile(coContractTelService.AppendixLink);
                }
                coContractTelService.AppendixLink = filename.Result;
                _serviceService.EditCoContractTelService(coContractTelService);
                _serviceService.SaveCoContractTelService();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            return Ok();
        }
        #endregion
    }
}
