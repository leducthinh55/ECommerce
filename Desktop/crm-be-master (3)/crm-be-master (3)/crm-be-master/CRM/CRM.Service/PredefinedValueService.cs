using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IPredefinedValueService
    {
        IEnumerable<PredefinedValue> GetPredefinedValues();
        IEnumerable<PredefinedValue> GetPredefinedValues(Expression<Func<PredefinedValue, bool>> where);
        PredefinedValue GetPredefinedValue(Guid id);
        void CreatePredefinedValue(PredefinedValue predefinedValue);
        void UpdatePredefinedValue(PredefinedValue predefinedValue);
        void DeletePredefinedValue(Guid id);
        void SaveChange();
    }
    public class PredefinedValueService : IPredefinedValueService
    {
        private readonly IPredefinedValueRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PredefinedValueService(IPredefinedValueRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreatePredefinedValue(PredefinedValue predefinedValue)
        {
            _repository.Add(predefinedValue);
        }

        public void DeletePredefinedValue(Guid id)
        {
            var predefinedValue = _repository.GetById(id);
            _repository.Delete(predefinedValue);
        }

        public PredefinedValue GetPredefinedValue(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<PredefinedValue> GetPredefinedValues()
        {
            return _repository.GetAll();
        }

        public IEnumerable<PredefinedValue> GetPredefinedValues(Expression<Func<PredefinedValue, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void UpdatePredefinedValue(PredefinedValue predefinedValue)
        {
            _repository.Update(predefinedValue);
        }
    }
}
