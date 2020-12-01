
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IErrorLogService
    {
        IEnumerable<ErrorLog> GetErrorLogs();
        IEnumerable<ErrorLog> GetErrorLogs(Expression<Func<ErrorLog, bool>> where);
        ErrorLog GetErrorLog(Guid id);
        void CreateErrorLog(ErrorLog ErrorLog);
        void UpdateErrorLog(ErrorLog ErrorLog);
        void DeleteErrorLog(Guid id);
        void SaveChange();
    }
    public class ErrorLogService : IErrorLogService
    {
        private readonly IErrorLogRepository _ErrorLogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ErrorLogService(IErrorLogRepository ErrorLogRepository, IUnitOfWork unitOfWork)
        {
            _ErrorLogRepository = ErrorLogRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateErrorLog(ErrorLog ErrorLog)
        {
            
            _ErrorLogRepository.Add(ErrorLog);
        }

        public void UpdateErrorLog(ErrorLog ErrorLog)
        {
            _ErrorLogRepository.Update(ErrorLog);
        }

        public IEnumerable<ErrorLog> GetErrorLogs(Expression<Func<ErrorLog, bool>> where)
        {
            return _ErrorLogRepository.GetMany(where);
        }

        public ErrorLog GetErrorLog(Guid id)
        {
            return _ErrorLogRepository.GetById(id);
        }

        public IEnumerable<ErrorLog> GetErrorLogs()
        {
            return _ErrorLogRepository.GetAll();
        }

        public void DeleteErrorLog(Guid id)
        {
            var ErrorLog = _ErrorLogRepository.GetById(id);
            if (ErrorLog != null)
            {
                _ErrorLogRepository.Delete(ErrorLog);
            }
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}
