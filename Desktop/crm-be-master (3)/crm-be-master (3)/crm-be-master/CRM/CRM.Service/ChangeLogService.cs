using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IChangeLogService
    {
        IEnumerable<ChangeLog> GetChangeLogs();
        ChangeLog GetChangeLog(Guid id);
        void CreateChangeLog(ChangeLog hsChangeLog);
        void EditChangeLog(ChangeLog hsChangeLog);
        void RemoveChangeLog(Guid id);
        void RemoveChangeLog(Expression<Func<ChangeLog, bool>> where);
        void SaveChangeLog();
    }
    public class ChangeLogService : IChangeLogService
    {
        private readonly IChangeLogRepository _changeLogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeLogService(IChangeLogRepository changeLogRepository, IUnitOfWork unitOfWork)
        {
            _changeLogRepository = changeLogRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ChangeLog> GetChangeLogs()
        {
            return _changeLogRepository.GetAll();
        }

        public ChangeLog GetChangeLog(Guid id)
        {
            return _changeLogRepository.GetById(id);
        }

        public void CreateChangeLog(ChangeLog hsChangeLog)
        {
            _changeLogRepository.Add(hsChangeLog);
            _unitOfWork.Commit();
        }

        public void EditChangeLog(ChangeLog hsChangeLog)
        {
            _changeLogRepository.Update(hsChangeLog);
        }

        public void RemoveChangeLog(Guid id)
        {
            var entity = _changeLogRepository.GetById(id);
            _changeLogRepository.Delete(entity);
        }

        public void RemoveChangeLog(Expression<Func<ChangeLog, bool>> @where)
        {
            _changeLogRepository.Delete(where);
        }

        public void SaveChangeLog()
        {
            _unitOfWork.Commit();
        }
    }
}

