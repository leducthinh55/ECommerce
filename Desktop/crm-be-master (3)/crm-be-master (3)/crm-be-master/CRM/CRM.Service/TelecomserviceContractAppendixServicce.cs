using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface ITelecomserviceContractAppendixService
    {
        IEnumerable<TelecomserviceContractAppendix> GetTelecomserviceContractAppendixs();
        IEnumerable<TelecomserviceContractAppendix> GetTelecomserviceContractAppendixs(Expression<Func<TelecomserviceContractAppendix, bool>> where);
        TelecomserviceContractAppendix GetTelecomserviceContractAppendix(Guid id);
        void CreateTelecomserviceContractAppendix(TelecomserviceContractAppendix TelecomserviceContractAppendix);
        void EditTelecomserviceContractAppendix(TelecomserviceContractAppendix TelecomserviceContractAppendix);
        void RemoveTelecomserviceContractAppendix(Guid id);
        void RemoveTelecomserviceContractAppendix(TelecomserviceContractAppendix TelecomserviceContractAppendix);
        void SaveTelecomserviceContractAppendix();
    }
    public class TelecomserviceContractAppendixService : ITelecomserviceContractAppendixService
    {
        private readonly ITelecomserviceContractAppendixRepository _TelecomserviceContractAppendixRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TelecomserviceContractAppendixService(ITelecomserviceContractAppendixRepository TelecomserviceContractAppendixRepository, IUnitOfWork unitOfWork)
        {
            _TelecomserviceContractAppendixRepository = TelecomserviceContractAppendixRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateTelecomserviceContractAppendix(TelecomserviceContractAppendix TelecomserviceContractAppendix)
        {
            _TelecomserviceContractAppendixRepository.Add(TelecomserviceContractAppendix);
        }

        public void EditTelecomserviceContractAppendix(TelecomserviceContractAppendix TelecomserviceContractAppendix)
        {
            _TelecomserviceContractAppendixRepository.Update(TelecomserviceContractAppendix);
        }

        public TelecomserviceContractAppendix GetTelecomserviceContractAppendix(Guid id)
        {
            return _TelecomserviceContractAppendixRepository.GetById(id);
        }

        public IEnumerable<TelecomserviceContractAppendix> GetTelecomserviceContractAppendixs()
        {
            return _TelecomserviceContractAppendixRepository.GetAll();
        }

        public IEnumerable<TelecomserviceContractAppendix> GetTelecomserviceContractAppendixs(Expression<Func<TelecomserviceContractAppendix, bool>> where)
        {
            return _TelecomserviceContractAppendixRepository.GetMany(where);
        }

        public void RemoveTelecomserviceContractAppendix(Guid id)
        {
            var TelecomserviceContractAppendix = _TelecomserviceContractAppendixRepository.GetById(id);
            _TelecomserviceContractAppendixRepository.Delete(TelecomserviceContractAppendix);
        }

        public void RemoveTelecomserviceContractAppendix(TelecomserviceContractAppendix TelecomserviceContractAppendix)
        {
            _TelecomserviceContractAppendixRepository.Delete(TelecomserviceContractAppendix);
        }

        public void SaveTelecomserviceContractAppendix()
        {
            _unitOfWork.Commit();
        }
    }
}
