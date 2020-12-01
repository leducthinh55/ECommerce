using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface IEventLogService
    {
        IEnumerable<EventLog> GetEventLogs();
        IEnumerable<EventLog> GetEventLogs(Expression<Func<EventLog, bool>> where);
        EventLog GetEventLog(Guid id);
        void CreateEventLog(EventLog eventLog);
        void UpdateEventLog(EventLog eventLog);
        void DeleteEventLog(EventLog eventLog);
        void SaveChanges();
    }
    public class EventLogService : IEventLogService
    {
        private readonly IEventLogRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public EventLogService(IEventLogRepository repository, IUnitOfWork iUnitOfWork)
        {
            _repository = repository;
            _unitOfWork = iUnitOfWork;
        }

        public void CreateEventLog(EventLog eventLog)
        {
            eventLog.DateCreated = DateTime.Now;
            _repository.Add(eventLog);
        }
        public void DeleteEventLog(EventLog eventLog)
        {
            eventLog.IsDeleted = true;
            foreach (var item in eventLog.Files)
            {
                item.IsDeleted = true;
            }
        }

        public EventLog GetEventLog(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<EventLog> GetEventLogs()
        {
            return _repository.GetAll();
        }

        public IEnumerable<EventLog> GetEventLogs(Expression<Func<EventLog, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateEventLog(EventLog eventLog)
        {
            _repository.Update(eventLog);
        }
    }
}
