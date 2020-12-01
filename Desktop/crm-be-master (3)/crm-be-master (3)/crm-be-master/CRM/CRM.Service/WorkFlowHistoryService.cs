using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Service
{
    public interface IWorkFlowHistoryService
    {
        IEnumerable<WorkFlowHistory> GetWorkFlowHistories();
        WorkFlowHistory GetWorkFlowHistory(Guid id);
        ICollection<WorkFlowHistory> GetInProcess(Guid customerWorkFlowId);
        void CreateWorkFlowHistory(WorkFlowHistory WorkFlowHistory);
        void EditWorkFlowHistory(WorkFlowHistory WorkFlowHistory);
        void RemoveWorkFlowHistory(Guid id);
        void SaveWorkFlowHistory();
    }

    public class WorkFlowHistoryService : IWorkFlowHistoryService
    {
        private readonly IWorkFlowHistoryRepository _WorkFlowHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WorkFlowHistoryService(IWorkFlowHistoryRepository WorkFlowHistoryRepository, IUnitOfWork unitOfWork)
        {
            this._WorkFlowHistoryRepository = WorkFlowHistoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateWorkFlowHistory(WorkFlowHistory WorkFlowHistory)
        {
            _WorkFlowHistoryRepository.Add(WorkFlowHistory);
        }

        public void EditWorkFlowHistory(WorkFlowHistory WorkFlowHistory)
        {
            var entity = _WorkFlowHistoryRepository.GetById(WorkFlowHistory.Id);
            entity = WorkFlowHistory;
            _WorkFlowHistoryRepository.Update(entity);
        }

        public ICollection<WorkFlowHistory> GetInProcess(Guid customerWorkFlowId) => _WorkFlowHistoryRepository
                .GetMany(h => h.CustomerWorkFlowId.Equals(customerWorkFlowId) && h.Status == 1).ToList();

        public WorkFlowHistory GetWorkFlowHistory(Guid id)
        {
            return _WorkFlowHistoryRepository.GetById(id);
        }

        public IEnumerable<WorkFlowHistory> GetWorkFlowHistories()
        {
            return _WorkFlowHistoryRepository.GetAll();
        }

        public void RemoveWorkFlowHistory(Guid id)
        {
            var entity = _WorkFlowHistoryRepository.GetById(id);
            _WorkFlowHistoryRepository.Delete(entity);
        }

        public void SaveWorkFlowHistory()
        {
            _unitOfWork.Commit();
        }
    }
}
