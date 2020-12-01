using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface ITelecomserviceParameterService
    {
        IEnumerable<TelecomserviceParameter> GetTelecomserviceParameters();
        IEnumerable<TelecomserviceParameter> GetTelecomserviceParameters(Expression<Func<TelecomserviceParameter, bool>> where);
        TelecomserviceParameter GetTelecomserviceParameter(Guid id);
        void CreateTelecomserviceParameter(TelecomserviceParameter TelecomserviceParameter);
        void EditTelecomserviceParameter(TelecomserviceParameter TelecomserviceParameter);
        void RemoveTelecomserviceParameter(Guid id);
        void RemoveTelecomserviceParameter(TelecomserviceParameter TelecomserviceParameter);
        void SaveTelecomserviceParameter();
    }
    public class TelecomserviceParameterService : ITelecomserviceParameterService
    {
        private readonly ITelecomserviceParameterRepository _TelecomserviceParameterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TelecomserviceParameterService(ITelecomserviceParameterRepository TelecomserviceParameterRepository, IUnitOfWork unitOfWork)
        {
            _TelecomserviceParameterRepository = TelecomserviceParameterRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateTelecomserviceParameter(TelecomserviceParameter TelecomserviceParameter)
        {
            _TelecomserviceParameterRepository.Add(TelecomserviceParameter);
        }

        public void EditTelecomserviceParameter(TelecomserviceParameter TelecomserviceParameter)
        {
            _TelecomserviceParameterRepository.Update(TelecomserviceParameter);
        }

        public TelecomserviceParameter GetTelecomserviceParameter(Guid id)
        {
            return _TelecomserviceParameterRepository.GetById(id);
        }

        public IEnumerable<TelecomserviceParameter> GetTelecomserviceParameters()
        {
            return _TelecomserviceParameterRepository.GetAll();
        }

        public IEnumerable<TelecomserviceParameter> GetTelecomserviceParameters(Expression<Func<TelecomserviceParameter, bool>> where)
        {
            return _TelecomserviceParameterRepository.GetMany(where);
        }

        public void RemoveTelecomserviceParameter(Guid id)
        {
            var TelecomserviceParameter = _TelecomserviceParameterRepository.GetById(id);
            _TelecomserviceParameterRepository.Delete(TelecomserviceParameter);
        }

        public void RemoveTelecomserviceParameter(TelecomserviceParameter TelecomserviceParameter)
        {
            _TelecomserviceParameterRepository.Delete(TelecomserviceParameter);
        }

        public void SaveTelecomserviceParameter()
        {
            _unitOfWork.Commit();
        }
    }
}
