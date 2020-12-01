using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    public class ContractController : Controller
    {
        private readonly string redirectUrl = "http://ctemplate.hisoft.vn/api/Contract";
        //private readonly string redirectUrl = "http://localhost:50120/api/Contract";

        private readonly IContractService _ContractService;
        private readonly IHsTemplateService _hsTemplateService;
        private readonly IWorkFlowHistoryFileService _workFlowHistoryFileService;
        private readonly IContractAppendixService _contractAppendixService;

        public ContractController( IContractService contractService, IHsTemplateService hsTemplateService, IWorkFlowHistoryFileService workFlowHistoryFileService, IContractAppendixService contractAppendixService)
        {
            _ContractService = contractService;
            _hsTemplateService = hsTemplateService;
            _workFlowHistoryFileService = workFlowHistoryFileService;
            _contractAppendixService = contractAppendixService;
        }



        //[HttpGet]
        //public ActionResult contract()
        //{
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        // 
        //    }
        //    return Ok();
        //}
        [HttpGet]
        public ActionResult GetContracts()
        {
            var rs = new List<ContractVM>();
            var data = _ContractService.GetContracts().ToList();
            foreach (var contract in data)
            {
                var _item = contract.Adapt<ContractVM>();
                //_item.CurrentSquare = _ContractService.GetSquareByDate(contract, DateTime.Now);
                //_item.CurrentUnitPrice = _ContractService.GetPriceByDate(contract, DateTime.Now);
                //_item.CurrentUnitServicePrice = _ContractService.GetServicePriceByDate(contract, DateTime.Now);

                rs.Add(_item);
            }

            return Ok(rs);
        }
        [HttpPost]
        public async Task<ActionResult> AddContract([FromBody]ContractCM model)
        {
            try
            {
                var template = _hsTemplateService.GetHsTemplate(model.TemplateId);
                if (template == null) return NotFound();
                
                #region Filters FormulaCaculator and NumberToText
                Dictionary<string, string> data = new Dictionary<string, string>(model.Data);
                foreach (string key in data.Keys)
                {
                    if (key.EndsWith("_Number"))
                    {
                        data.TryGetValue(key, out string value);
                        if (!Double.TryParse(value.Replace(",", "-").Replace(".", ",").Replace("-", "."), out double d))
                        {
                            
                        }
                        model.Data.Add(key.Replace("_Number", "_Text"), _ContractService.NumberToText(d, ref value));
                        model.Data[key] = value.Replace(".", "-").Replace(",", ".").Replace("-", ",");
                    }
                    else if (key.Contains("/Formula"))
                    {
                        string[] arr = key.Split("/");
                        string[] tags = arr[2].Split(",");
                        double[] numbers = new double[tags.Length];
                        for (int i = 0; i < numbers.Length; i++)
                        {
                            data.TryGetValue(tags[i], out string _value);
                            if (!Double.TryParse(_value.Replace(",", "-").Replace(".", ",").Replace("-", "."), out double d))
                            {

                            }
                            numbers[i] = d;
                        }

                        string value = "";
                        double number = _ContractService.FormulaCaculator(numbers, arr[3]);
                        model.Data.Add(arr[0] + "_Text", _ContractService.NumberToText(number, ref value));
                        model.Data[key] = value.Replace(".", "-").Replace(",", ".").Replace("-", ",");
                    }
                }
                #endregion

                model.Data.Add("FileName", template.Name);
                HttpResponseMessage response = null;
                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model.Data));
                    response = await httpClient.PostAsync(redirectUrl, content);
                    try
                    {
                        response.EnsureSuccessStatusCode();
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
                
                DateTime date = DateTime.Now;
                string fileName = template.Name.Substring(0, template.Name.IndexOf(".docx")) + "_" + date.ToString("ddMMyyyyhhmmss") + ".docx";
                using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Document\\Files\\" + fileName), FileMode.Create))
                {
                    await (await response.Content.ReadAsStreamAsync()).CopyToAsync(stream);
                }

                var workFlowHistoryFile = new Model.WorkFlowHistoryFile
                {
                    Name = fileName,
                    Path = "Document\\Files\\" + fileName,
                    WorkFlowHistoryId = model.WorkFlowHistoryId,
                    Date = date,
                    IsTemplate = false
                };
                _workFlowHistoryFileService.CreateWorkFlowHistoryFile(workFlowHistoryFile);
                
                return Ok(new { workFlowHistoryFile.Id });
                //return File(await response.Content.ReadAsByteArrayAsync(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetContract(Guid id)
        {
            var contract = _ContractService.GetContractById(id);
            var rs = contract.Adapt<ContractVM>();
            //rs.CurrentSquare = _ContractService.GetSquareByDate(contract, DateTime.Now);
            //rs.CurrentUnitPrice = _ContractService.GetPriceByDate(contract, DateTime.Now);
            //rs.CurrentUnitServicePrice = _ContractService.GetServicePriceByDate(contract, DateTime.Now);
            return Ok(rs);
        }

        [HttpPut]
        public ActionResult Put([FromBody]ContractUM model)
        {
            try
            {
                var contract = _ContractService.GetContractById(model.Id);

                if (contract == null) return NotFound();

                if (contract.ContractNo != null) return BadRequest("Contract already have ContractNo!");

                contract = model.Adapt(contract);
                _ContractService.UpdateContract(contract);
                _ContractService.SaveContract();
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
                var contract = _ContractService.GetContractById(id);

                if (contract == null) return NotFound();
                if (contract.ContractNo != null) return BadRequest("Contract already have ContractNo!");

                _ContractService.DeleteContract(contract);
                _ContractService.SaveContract();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpPost("Sync")]
        //private ActionResult Sync([FromBody]List<ContractSyncModel> data)
        //{
        //    foreach (var item in data)
        //    {
        //        var contract = new Contract()
        //        {
        //            CompanyName = item.CompanyName,
        //            Contact = item.Contact,
        //            ContractDate = null,
        //            ContractNo = item.ContractNumber,
        //            CustomerId = null,
        //            CustomerWorkFlowCode = "",
        //            Email = item.Email,
        //            EndDate = null,
        //            Fax = item.Fax,
        //            Floor = item.Floor,
        //            Note = item.Note,
        //            Phone = item.Phone,
        //            Position = item.Position,
        //            Room = item.Room,
        //            StartDate = null,
        //            UpPriceDate = null,
        //            OtherPrice = item.OtherPrice,
        //            Building = item.Building,
        //        };
        //        try
        //        {
        //            contract.DownSquare = string.IsNullOrEmpty(item.DownSquare) ? 0 : double.Parse(item.DownSquare);
        //            contract.LevelUpPrice = string.IsNullOrEmpty(item.LevelUpPrice) ? 0 : double.Parse(item.LevelUpPrice);
        //            contract.Amount = string.IsNullOrEmpty(item.Amount) ? 0 : double.Parse(item.Amount);
        //            contract.ServiceAmount = string.IsNullOrEmpty(item.ServiceAmount) ? 0 : double.Parse(item.ServiceAmount);
        //            contract.Square = string.IsNullOrEmpty(item.Square) ? 0 : double.Parse(item.Square);
        //            contract.UpSquare = string.IsNullOrEmpty(item.UpSquare) ? 0 : double.Parse(item.UpSquare);
        //            contract.UsingSquare = string.IsNullOrEmpty(item.UsingSquare) ? 0 : double.Parse(item.UsingSquare);
        //        }
        //        catch (Exception e)
        //        {

        //        }
        //        if (!string.IsNullOrEmpty(item.ContractDate))
        //        {
        //            contract.ContractDate = DateTime.ParseExact(item.ContractDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        }
        //        if (!string.IsNullOrEmpty(item.EndDate))
        //        {
        //            contract.EndDate = DateTime.ParseExact(item.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        }
        //        _ContractService.Create(contract);
        //    }
        //    _ContractService.SaveContract();
        //    return Ok();
        //}

        [HttpGet("{id}/Appendix")]
        public ActionResult GetAppendices(Guid id) {
            var appendices = _contractAppendixService.GetContractAppendixs(_ => _.ContractId == id).Select(_ =>_.Adapt<ContractAppendixVM>()).ToList();
            return Ok(appendices);
        }

    }
}
