using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Word = Microsoft.Office.Interop.Word;

namespace CRM_HsTemplate.Controllers
{
    public class TestController : ApiController
    {
        [HttpPost()]
        public IHttpActionResult UploadTemplate()
        {
            try
            {
                var requestContent = Request.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestContent);

                var missing = Missing.Value;
                var wordApp = new Word.Application();
                if (!data.TryGetValue("FileName", out string fileName))
                {
                    return NotFound();
                }

                string path = HttpContext.Current.Server.MapPath("~/Template/" + fileName);
                var wordDoc = wordApp.Documents.Add(path, missing, missing, missing);

                var contentControls = wordDoc.ContentControls;

                for (int i = 0; i < contentControls.Count + 1; i++)
                {
                    try
                    {
                        contentControls[i].Range.Select();
                        if (data.TryGetValue(contentControls[i].Tag.Trim(), out string value))
                        {
                            wordApp.Selection.TypeText(value);
                            /*
                            contentControls[i].Delete();
                            i--;
                            */
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                string savePath = HttpContext.Current.Server.MapPath("~/Document/" + fileName);
                wordDoc.SaveAs2(savePath);
                wordDoc.Close();
                wordApp.Quit();

                return Ok(savePath);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
