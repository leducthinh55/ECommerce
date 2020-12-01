using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface ICareHistoryService
    {
        IEnumerable<CareHistory> GetCareHistorys();
        IEnumerable<CareHistory> GetCareHistorys(Expression<Func<CareHistory, bool>> where);
        CareHistory GetCareHistory(Guid id);
        void CreateCareHistory(CareHistory CareHistory);
        void EditCareHistory(CareHistory CareHistory);
        void RemoveCareHistory(Guid id);
        void RemoveCareHistory(CareHistory CareHistory);
        void SaveCareHistory();
    }

    public class CareHistoryService : ICareHistoryService
    {
        private readonly ICareHistoryRepository _CareHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CareHistoryService(ICareHistoryRepository CareHistoryRepository, IUnitOfWork unitOfWork)
        {
            this._CareHistoryRepository = CareHistoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateCareHistory(CareHistory CareHistory)
        {
            _CareHistoryRepository.Add(CareHistory);
        }

        public void EditCareHistory(CareHistory CareHistory)
        {
            var entity = _CareHistoryRepository.GetById(CareHistory.Id);
            entity = CareHistory;
            _CareHistoryRepository.Update(entity);
        }

        public CareHistory GetCareHistory(Guid id)
        {
            return _CareHistoryRepository.GetById(id);
        }

        public IEnumerable<CareHistory> GetCareHistorys()
        {
            return _CareHistoryRepository.GetAll();
        }

        public void RemoveCareHistory(Guid id)
        {
            var entity = _CareHistoryRepository.GetById(id);
            _CareHistoryRepository.Delete(entity);
        }

        public void SaveCareHistory()
        {
            _unitOfWork.Commit();
        }

        public void RemoveCareHistory(CareHistory CareHistory)
        {
            _CareHistoryRepository.Delete(CareHistory);
        }

        public IEnumerable<CareHistory> GetCareHistorys(Expression<Func<CareHistory, bool>> where)
        {
            return _CareHistoryRepository.GetMany(where);
        }
    }
}
