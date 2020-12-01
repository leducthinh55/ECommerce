using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
   
    public interface ICoContractTelServiceService
    {
        IEnumerable<CoContractTelService> GetCoContractTelServices();
        IEnumerable<CoContractTelService> GetCoContractTelServices(Expression<Func<CoContractTelService, bool>> where);
        CoContractTelService GetCoContractTelService(Guid id);
        void CreateCoContractTelService(CoContractTelService CoContractTelService);
        void EditCoContractTelService(CoContractTelService CoContractTelService);
        void RemoveCoContractTelService(Guid id);
        void RemoveCoContractTelService(CoContractTelService CoContractTelService);
        void SaveCoContractTelService();
    }

    public class CoContractTelServiceService : ICoContractTelServiceService
    {
        private readonly ICoContractTelServiceRepository _CoContractTelServiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CoContractTelServiceService(ICoContractTelServiceRepository CoContractTelServiceRepository, IUnitOfWork unitOfWork)
        {
            this._CoContractTelServiceRepository = CoContractTelServiceRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateCoContractTelService(CoContractTelService CoContractTelService)
        {
            _CoContractTelServiceRepository.Add(CoContractTelService);
        }

        public void EditCoContractTelService(CoContractTelService CoContractTelService)
        {
            var entity = _CoContractTelServiceRepository.GetById(CoContractTelService.Id);
            entity = CoContractTelService;
            _CoContractTelServiceRepository.Update(entity);
        }

        public CoContractTelService GetCoContractTelService(Guid id)
        {
            return _CoContractTelServiceRepository.GetById(id);
        }

        public IEnumerable<CoContractTelService> GetCoContractTelServices()
        {
            return _CoContractTelServiceRepository.GetAll();
        }

        public void RemoveCoContractTelService(Guid id)
        {
            var entity = _CoContractTelServiceRepository.GetById(id);
            _CoContractTelServiceRepository.Delete(entity);
        }

        public void SaveCoContractTelService()
        {
            _unitOfWork.Commit();
        }

        public void RemoveCoContractTelService(CoContractTelService CoContractTelService)
        {
            _CoContractTelServiceRepository.Delete(CoContractTelService);
        }

        public IEnumerable<CoContractTelService> GetCoContractTelServices(Expression<Func<CoContractTelService, bool>> where)
        {
            return _CoContractTelServiceRepository.GetMany(where);
        }
    }
}
