using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CRM.Service;
using CRM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HsTemplateController : ControllerBase
    {
        private readonly string redirectUrl = "http://ctemplate.hisoft.vn/api/HsTemplate";
        //private readonly string redirectUrl = "http://localhost:50120/api/HsTemplate";

        public readonly IHsTemplateService _hsTemplateService;

        public HsTemplateController(IHsTemplateService hsTemplateService)
        {
            _hsTemplateService = hsTemplateService;
        }
        [HttpPost]
        public async Task<ActionResult> UploadTemplate(IFormFile file, Guid instanceId)
        {
            try
            {
                var hsTemplate =  _hsTemplateService.UploadFile(file, instanceId);
                List<string> data = null;

                using (var httpClient = new HttpClient())
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    using (var content = new MultipartFormDataContent())
                    {
                        content.Add(new StreamContent(file.OpenReadStream())
                        {
                            Headers =
                            {
                                ContentLength = file.Length,
                                ContentType = new MediaTypeHeaderValue(file.ContentType)
                            }
                        }, "File", fileName);
                        
                        var response = await httpClient.PostAsync(redirectUrl, content);
                        var resContent = await response.Content.ReadAsStringAsync();
                        data = JsonConvert.DeserializeObject<List<string>>(resContent);
                    }
                }

                return Ok(new HsTemplateFieldsVM {
                    Id = hsTemplate.Id,
                    Fields = data
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public ActionResult GetTemplate(Guid instanceId)
        {
            try
            {
                var hsTemplates = _hsTemplateService.GetHsTemplates(_ => _.InstanceId == instanceId).ToList();

                List<HsTemplateVM> result = new List<HsTemplateVM>();
                foreach (var item in hsTemplates)
                {
                    result.Add(new HsTemplateVM {
                        Id = item.Id,
                        Name = item.Name,
                        Path = item.Path,
                        Date = item.Date
                    });
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        public ActionResult DeleteTemplate(Guid id)
        {
            try
            {
                _hsTemplateService.DeleteHsTemplate(id);
                _hsTemplateService.SaveChange();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("FormId")]
        public ActionResult UpdateFormId(Guid templateId, Guid formID)
        {
            try
            {
                var hsTemplate = _hsTemplateService.GetHsTemplate(templateId);
                if (hsTemplate == null) return NotFound();

                hsTemplate.FormId = formID;
                _hsTemplateService.UpdateHsTemplate(hsTemplate);
                _hsTemplateService.SaveChange();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("Download")]
        public ActionResult DownloadTemplate(Guid id)
        {
            try
            {
                var hsTemplate = _hsTemplateService.GetHsTemplate(id);
                if (hsTemplate == null) return NotFound();

                var file = _hsTemplateService.DownloadFile(id).Result;
                return File(file.Stream, file.ContentType, file.FileName);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}