using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using CRM.Service.Utils;
using Microsoft.AspNetCore.Http;

namespace CRM.Service
{
    public interface IWorkFlowHistoryFileService
    {
        IEnumerable<WorkFlowHistoryFile> GetWorkFlowHistoryFiles();
        WorkFlowHistoryFile GetWorkFlowHistoryFile(Guid id);
        void CreateWorkFlowHistoryFile(WorkFlowHistoryFile WorkFlowHistoryFile);
        void EditWorkFlowHistoryFile(WorkFlowHistoryFile WorkFlowHistoryFile);
        void RemoveWorkFlowHistoryFile(Guid id);
        void SaveWorkFlowHistoryFile();
        Task<WorkFlowHistoryFile> UploadFile(IFormFile file, Guid workFlowHistoryId, bool IsTemplate);
        Task<FileSupport> DownloadFile(Guid id);
        IEnumerable<WorkFlowHistoryFile> GetTemplates(Guid instanceId);
    }
    public class WorkFlowHistoryFileService : IWorkFlowHistoryFileService
    {
        private readonly IWorkFlowHistoryFileRepository _workFlowHistoryFileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WorkFlowHistoryFileService(IWorkFlowHistoryFileRepository workFlowHistoryFileRepository, IUnitOfWork unitOfWork)
        {
            _workFlowHistoryFileRepository = workFlowHistoryFileRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<WorkFlowHistoryFile> GetWorkFlowHistoryFiles()
        {
            return _workFlowHistoryFileRepository.GetAll();
        }

        public WorkFlowHistoryFile GetWorkFlowHistoryFile(Guid id)
        {
            return _workFlowHistoryFileRepository.GetById(id);
        }

        public void CreateWorkFlowHistoryFile(WorkFlowHistoryFile WorkFlowHistoryFile)
        {
            _workFlowHistoryFileRepository.Add(WorkFlowHistoryFile);
            _unitOfWork.Commit();
        }

        public void EditWorkFlowHistoryFile(WorkFlowHistoryFile WorkFlowHistoryFile)
        {
            _workFlowHistoryFileRepository.Update(WorkFlowHistoryFile);
        }

        public void RemoveWorkFlowHistoryFile(Guid id)
        {
            var entity = _workFlowHistoryFileRepository.GetById(id);
            _workFlowHistoryFileRepository.Delete(entity);
        }

        public void SaveWorkFlowHistoryFile()
        {
            _unitOfWork.Commit();
        }

        public async Task<WorkFlowHistoryFile> UploadFile(IFormFile file, Guid workFlowHistoryId, bool isTemplate)
        {
            var workFlowHistoryFile = await _workFlowHistoryFileRepository.UploadFile(file, workFlowHistoryId, isTemplate);
            CreateWorkFlowHistoryFile(workFlowHistoryFile);
            return workFlowHistoryFile;
        }

        public async Task<FileSupport> DownloadFile(Guid id)
        {
            var workFlowHistoryFile = GetWorkFlowHistoryFile(id);
            if (workFlowHistoryFile == null)
            {
                return null;
            }

            string filename = workFlowHistoryFile.Name;
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Document/Files", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return new FileSupport
            {
                Stream = memory,
                FileName = filename,
                ContentType = FileUtils.GetContentType(path)
            };
        }
        public IEnumerable<WorkFlowHistoryFile> GetTemplates(Guid instanceId)
        {
            var templates = _workFlowHistoryFileRepository.GetMany(n => n.IsTemplate);
            return templates;
        }
    }
}

