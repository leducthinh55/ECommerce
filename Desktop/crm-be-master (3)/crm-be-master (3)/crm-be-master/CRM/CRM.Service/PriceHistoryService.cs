using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;


namespace CRM.Service
{
    public interface IPriceHistoryService
    {
        IEnumerable<PriceHistory> GetPriceHistories();
        IEnumerable<PriceHistory> GetPriceHistories(Expression<Func<PriceHistory, bool>> where);
        PriceHistory GetPriceHistory(Guid id);
        void CreatePriceHistory(PriceHistory priceHistory);
        void UpdatePriceHistory(PriceHistory priceHistory);
        void DeletePriceHistory(PriceHistory priceHistory);
        void SaveChange();
    }
    public class PriceHistoryService : IPriceHistoryService
    {
        private readonly IPriceHistoryRepository _priceHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PriceHistoryService(IPriceHistoryRepository priceHistoryRepository, IUnitOfWork unitOfWork)
        {
            _priceHistoryRepository = priceHistoryRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreatePriceHistory(PriceHistory priceHistory)
        {
            _priceHistoryRepository.Add(priceHistory);
        }

        public void DeletePriceHistory(PriceHistory priceHistory)
        {
            _priceHistoryRepository.Delete(priceHistory);
        }

        public IEnumerable<PriceHistory> GetPriceHistories()
        {
            return _priceHistoryRepository.GetAll();
        }

        public IEnumerable<PriceHistory> GetPriceHistories(Expression<Func<PriceHistory, bool>> where)
        {
            return _priceHistoryRepository.GetMany(where);
        }

        public PriceHistory GetPriceHistory(Guid id)
        {
            return _priceHistoryRepository.GetById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void UpdatePriceHistory(PriceHistory priceHistory)
        {
            _priceHistoryRepository.Update(priceHistory);
        }
    }
}
