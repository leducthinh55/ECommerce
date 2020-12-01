using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using Word = Microsoft.Office.Interop.Word;

namespace CRM_HsTemplate.Controllers
{
    public class ContractController : ApiController
    {
        [HttpPost()]
        public async Task<HttpResponseMessage> GetContractAsync()
        {
            try
            {
                var requestContent = Request.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestContent);

                var missing = Missing.Value;
                var wordApp = new Word.Application();
                if (!data.TryGetValue("FileName", out string fileName))
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
                }

                string path = HttpContext.Current.Server.MapPath("~/Template/" + fileName);
                var wordDoc = wordApp.Documents.Add(path, missing, missing, missing);
                /*
                var contentControls = wordDoc.ContentControls;

                for (int i = 0; i < contentControls.Count + 1; i++)
                {
                    try
                    {
                        contentControls[i].Range.Select();
                        if (data.TryGetValue(contentControls[i].Tag.Trim(), out string value))
                        {
                            wordApp.Selection.TypeText(value);
                            contentControls[i].Delete();
                            i--;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                */
                foreach (Word.ContentControl contentControl in wordDoc.ContentControls)
                {
                    try
                    {
                        contentControl.Range.Select();
                        if (data.TryGetValue(contentControl.Tag.Trim(), out string value))
                        {
                            wordApp.Selection.TypeText(value);
                        }
                    }
                    catch (Exception) {}
                }

                foreach (Word.Shape shape in wordDoc.Shapes)
                {
                    try
                    {
                        foreach (Word.ContentControl contentControl in shape.TextFrame.TextRange.ContentControls)
                        {
                            try
                            {
                                contentControl.Range.Select();
                                if (data.TryGetValue(contentControl.Tag.Trim(), out string value))
                                {
                                    wordApp.Selection.TypeText(value);
                                }
                            }
                            catch (Exception) {}
                        }
                    }
                    catch (Exception) {}
                }

                string savePath = HttpContext.Current.Server.MapPath("~/Document/" + fileName);
                wordDoc.SaveAs2(savePath);
                wordDoc.Close();
                wordApp.Quit();

                var memory = new MemoryStream();
                using (var stream = new FileStream(savePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(memory.ToArray())
                };
                result.Content.Headers.ContentDisposition =
                     new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                     {
                         FileName = fileName
                     };
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue
                    ("application/octet-stream");
                return result;
                
            }
            catch (Exception e)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }
        }

    }
}
