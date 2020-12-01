using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface ITelecomserviceService
    {
        IEnumerable<Telecomservice> GetTelecomservices();
        IEnumerable<Telecomservice> GetTelecomservices(Expression<Func<Telecomservice, bool>> where);
        Telecomservice GetTelecomservice(Guid id);
        void CreateTelecomservice(Telecomservice Telecomservice);
        void EditTelecomservice(Telecomservice Telecomservice);
        void RemoveTelecomservice(Guid id);
        void RemoveTelecomservice(Telecomservice Telecomservice);
        void SaveTelecomservice();
    }
    public class TelecomserviceService : ITelecomserviceService
    {
        private readonly ITelecomserviceRepository _TelecomserviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TelecomserviceService(ITelecomserviceRepository TelecomserviceRepository, IUnitOfWork unitOfWork)
        {
            _TelecomserviceRepository = TelecomserviceRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateTelecomservice(Telecomservice Telecomservice)
        {
            _TelecomserviceRepository.Add(Telecomservice);
        }

        public void EditTelecomservice(Telecomservice Telecomservice)
        {
            _TelecomserviceRepository.Update(Telecomservice);
        }

        public Telecomservice GetTelecomservice(Guid id)
        {
            return _TelecomserviceRepository.GetById(id);
        }

        public IEnumerable<Telecomservice> GetTelecomservices()
        {
            return _TelecomserviceRepository.GetAll();
        }

        public IEnumerable<Telecomservice> GetTelecomservices(Expression<Func<Telecomservice, bool>> where)
        {
            return _TelecomserviceRepository.GetMany(where);
        }

        public void RemoveTelecomservice(Guid id)
        {
            var Telecomservice = _TelecomserviceRepository.GetById(id);
            _TelecomserviceRepository.Delete(Telecomservice);
        }

        public void RemoveTelecomservice(Telecomservice Telecomservice)
        {
            _TelecomserviceRepository.Delete(Telecomservice);
        }

        public void SaveTelecomservice()
        {
            _unitOfWork.Commit();
        }
    }
}
