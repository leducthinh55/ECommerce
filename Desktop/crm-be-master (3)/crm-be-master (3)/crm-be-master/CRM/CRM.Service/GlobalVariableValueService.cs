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
    public interface IGlobalVariableValueService
    {
        IEnumerable<GlobalVariableValue> GetGlobalVariableValues();
        IEnumerable<GlobalVariableValue> GetGlobalVariableValues(Expression<Func<GlobalVariableValue, bool>> where);
        GlobalVariableValue GetGlobalVariableValue(Guid Id);
        void CreateGlobalVariableValue(GlobalVariableValue globalVariableValue);
        void UpdateGlobalVariableValue(GlobalVariableValue globalVariableValue);
        void DeleteGlobalVariableValue(GlobalVariableValue globalVariableValue);
        void SaveChanges();

        Task<string> UploadFile(IFormFile file);
        Task<FileSupport> DowloadFile(string fileName);
        void DeleteFile(string fileName);

    }
    public class GlobalVariableValueService : IGlobalVariableValueService
    {
        private readonly IGlobalVariableValueRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public GlobalVariableValueService(IGlobalVariableValueRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateGlobalVariableValue(GlobalVariableValue globalVariableValue)
        {
            globalVariableValue.DateCreated = DateTime.Now;
            _repository.Add(globalVariableValue);
        }

        public void DeleteFile(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.Delete(path);
        }

        public void DeleteGlobalVariableValue(GlobalVariableValue globalVariableValue)
        {
            _repository.Delete(globalVariableValue);
        }

        public async Task<FileSupport> DowloadFile(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            

            return new FileSupport
            {
                Stream = memory,
                FileName = fileName,
                ContentType = FileUtils.GetContentType(path)
            };

        }

        public GlobalVariableValue GetGlobalVariableValue(Guid Id)
        {
            return _repository.GetById(Id);
        }

        public IEnumerable<GlobalVariableValue> GetGlobalVariableValues()
        {
            return _repository.GetAll();
        }

        public IEnumerable<GlobalVariableValue> GetGlobalVariableValues(Expression<Func<GlobalVariableValue, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateGlobalVariableValue(GlobalVariableValue globalVariableValue)
        {
            _repository.Update(globalVariableValue);
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            // generate filename
            string fileName = file.FileName;
            fileName = DateTime.Now.ToString("dMyyyyhmmss_") + fileName;

            //get Path
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Document/Files/GlobalVariableFile", fileName);

            try
            {
                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
                return "Document/Files/GlobalVariableFile/" + fileName;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
