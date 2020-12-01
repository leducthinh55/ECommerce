using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using CRM.Service.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Service
{
    public static class FilePath
    {
        public static string CooperationContractFile = @"Document\Files\CooperationContract";
        public static string EventLogFile = @"Document\Files\EventLogFile";
    }
    public interface IEventLogFileService
    {
        IEnumerable<EventLogFile> GetEventLogFiles(Expression<Func<EventLogFile, bool>> where);
        EventLogFile GetEventLogFile(Guid id);
        void CreateEventLogFile(EventLogFile eventLogFile);
        void UpdateEventLogFile(EventLogFile eventLogFile);
        void DeleteEventLogFile(EventLogFile eventLogFile);
        Task<String> UploadFile(IFormFile file, string filePath);
        Task<FileSupport> DownloadFile(string path);
        void DeleteFile(string path);

        void SaveChanges();
    }
    public class EventLogFileService : IEventLogFileService
    {
        private readonly IEventLogFileRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public EventLogFileService(IEventLogFileRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateEventLogFile(EventLogFile eventLogFile)
        {
            _repository.Add(eventLogFile);
        }

        public void DeleteEventLogFile(EventLogFile eventLogFile)
        {
            eventLogFile.IsDeleted = true;
            _repository.Update(eventLogFile);
        }

        public void DeleteFile(string pathUrl)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), pathUrl);
                File.Delete(path);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<FileSupport> DownloadFile(string pathUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), pathUrl);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return new FileSupport
            {
                Stream = memory,
                FileName = pathUrl,
                ContentType = FileUtils.GetContentType(path)
            };

        }

        public EventLogFile GetEventLogFile(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<EventLogFile> GetEventLogFiles(Expression<Func<EventLogFile, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateEventLogFile(EventLogFile eventLogFile)
        {
            _repository.Update(eventLogFile);
        }

        public async Task<string> UploadFile(IFormFile file, string filePath)
        {
            string filename = DateTime.Now.ToString("ddMMyyyy_hhmmss") + file.FileName;
            string path = Path.Combine(Directory.GetCurrentDirectory(), filePath, filename);

            try
            {
                using (FileStream bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
                return filePath+@"\" + filename;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
