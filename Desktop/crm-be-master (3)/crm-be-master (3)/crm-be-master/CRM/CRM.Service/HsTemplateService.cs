
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using CRM.Service.Utils;
using Microsoft.AspNetCore.Http;

namespace CRM.Service
{
    public interface IHsTemplateService
    {
        IEnumerable<HsTemplate> GetHsTemplates();
        HsTemplate GetHsTemplate(Expression<Func<HsTemplate, bool>> where);
        IEnumerable<HsTemplate> GetHsTemplates(Expression<Func<HsTemplate, bool>> where);
        HsTemplate GetHsTemplate(Guid id);
        Task<FileSupport> DownloadFile(Guid id);
        void CreateHsTemplate(HsTemplate HsTemplate);
        void UpdateHsTemplate(HsTemplate HsTemplate);
        void DeleteHsTemplate(Guid id);
        void SaveChange();

        HsTemplate UploadFile(IFormFile file, Guid instanceId);

    }
    public class HsTemplateService : IHsTemplateService
    {
        private readonly IHsTemplateRepository _hsTemplateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HsTemplateService(IHsTemplateRepository HsTemplateRepository, IUnitOfWork unitOfWork)
        {
            _hsTemplateRepository = HsTemplateRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateHsTemplate(HsTemplate HsTemplate)
        {
            _hsTemplateRepository.Add(HsTemplate);
        }

        public void UpdateHsTemplate(HsTemplate HsTemplate)
        {
            _hsTemplateRepository.Update(HsTemplate);
        }

        public HsTemplate GetHsTemplate(Expression<Func<HsTemplate, bool>> @where)
        {
            return _hsTemplateRepository.Get(where);
        }

        public IEnumerable<HsTemplate> GetHsTemplates(Expression<Func<HsTemplate, bool>> where)
        {
            return _hsTemplateRepository.GetMany(where);
        }

        public HsTemplate GetHsTemplate(Guid id)
        {
            return _hsTemplateRepository.GetById(id);
        }

        public IEnumerable<HsTemplate> GetHsTemplates()
        {
            return _hsTemplateRepository.GetAll();
        }

        public void DeleteHsTemplate(Guid id)
        {
            var template = _hsTemplateRepository.GetById(id);
            _hsTemplateRepository.Delete(template);
        }

        public async Task<FileSupport> DownloadFile(Guid id)
        {
            var workFlowHistoryFile = GetHsTemplate(id);
            if (workFlowHistoryFile == null)
            {
                return null;
            }

            string filename = workFlowHistoryFile.Name;
            string _path = workFlowHistoryFile.Path;
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                _path);

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
                ContentType = GetContentType(path)
            };
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public HsTemplate UploadFile(IFormFile file, Guid instanceId)
        {
            string fileName = file.FileName;
            var path = "";
                path = Path.Combine(Directory.GetCurrentDirectory(), "Document\\Contract\\Template", fileName);
            using (var bits = new FileStream(path, FileMode.Create))
            {
                 file.CopyTo(bits);
            }

            var hsTemplate = new HsTemplate
            {
                Name = fileName,
                Path = Path.Combine("Document\\Contract\\Template", fileName),
                Date = DateTime.Now,
                InstanceId = instanceId
            };

            var dbTemplate = GetHsTemplate(n => n.Name.Equals(fileName) && n.InstanceId == instanceId);
            if (dbTemplate != null)
            {
                DeleteHsTemplate(dbTemplate.Id);
            }
            CreateHsTemplate(hsTemplate);
            SaveChange();

            return hsTemplate;
        }

        public ICollection<string> GetFields(string templatePath)
        {
            throw new Exception("Service not implemented");
        }
    }
}
