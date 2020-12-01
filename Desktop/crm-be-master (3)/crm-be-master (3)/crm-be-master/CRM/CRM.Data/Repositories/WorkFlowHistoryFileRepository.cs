using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CRM.Data.Infrastructure;
using CRM.Model;
using Microsoft.AspNetCore.Http;

namespace CRM.Data.Repositories
{
    public interface IWorkFlowHistoryFileRepository : IRepository<WorkFlowHistoryFile>
    {
        Task<WorkFlowHistoryFile> UploadFile(IFormFile file, Guid workFlowHistoryId, bool IsTemplate);

    }
    public class WorkFlowHistoryFileRepository : RepositoryBase<WorkFlowHistoryFile>, IWorkFlowHistoryFileRepository
    {
        public WorkFlowHistoryFileRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<WorkFlowHistoryFile> UploadFile(IFormFile file, Guid workFlowHistoryId, bool IsTemplate)
        {
            string fileName = file.FileName.Split('.')[0];
            string currentDate = DateTime.Now.ToString("dMyyyy");
            string currentTime = DateTime.Now.ToString("hmmss");
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName += "_" + currentDate + currentTime + extension;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Document\\Files", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return null;
            }
            var workFlowHistoryFile = new WorkFlowHistoryFile
            {
                Name = fileName,
                Date = DateTime.Now,
                Path = "Document\\Files" + fileName,
                WorkFlowHistoryId = workFlowHistoryId,
                IsTemplate = IsTemplate
            };
            return workFlowHistoryFile;
        }
    }
}
