using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface ILogService
    {
        IEnumerable<Log> GetLogs(Expression<Func<Log, bool>> where);
        Log GetLog(Guid id);
        void CreateLog(Exception e, string callerClass);
        void UpdateLog(Log Log);
        void DeleteLog(Log Log);
        void SaveChanges();
    }
    public class LogService : ILogService
    {
        private readonly ILogRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public LogService(ILogRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateLog(Exception e, string callerClass)
        {
            var log = new Log
            {
                Date = DateTime.Now,
                Exception = e.StackTrace,
                Level = "ERROR",
                Logger = callerClass,
                Message = e.Message
            };
            _repository.Add(log);
            SaveChanges();
        }

        public void DeleteLog(Log Log)
        {
            _repository.Delete(Log);
        }

        public Log GetLog(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Log> GetLogs(Expression<Func<Log, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateLog(Log Log)
        {
            _repository.Update(Log);
        }
    }
}
