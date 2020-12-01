using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    
    public interface ICooperationContractService
    {
        IEnumerable<CooperationContract> GetCooperationContracts();
        IEnumerable<CooperationContract> GetCooperationContracts(Expression<Func<CooperationContract, bool>> where);
        CooperationContract GetCooperationContract(Guid id);
        void CreateCooperationContract(CooperationContract CooperationContract);
        void UpdateCooperationContract(CooperationContract CooperationContract);
        void DeleteCooperationContract(Guid id);
        void SaveChange();
    }
    public class CooperationContractService : ICooperationContractService
    {
        private readonly ICooperationContractRepository _CooperationContractRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CooperationContractService(ICooperationContractRepository CooperationContractRepository, IUnitOfWork unitOfWork)
        {
            _CooperationContractRepository = CooperationContractRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateCooperationContract(CooperationContract CooperationContract)
        {

            _CooperationContractRepository.Add(CooperationContract);
        }

        public void UpdateCooperationContract(CooperationContract CooperationContract)
        {
            _CooperationContractRepository.Update(CooperationContract);
        }

        public IEnumerable<CooperationContract> GetCooperationContracts(Expression<Func<CooperationContract, bool>> where)
        {
            return _CooperationContractRepository.GetMany(where);
        }

        public CooperationContract GetCooperationContract(Guid id)
        {
            return _CooperationContractRepository.GetById(id);
        }

        public IEnumerable<CooperationContract> GetCooperationContracts()
        {
            return _CooperationContractRepository.GetAll();
        }

        public void DeleteCooperationContract(Guid id)
        {
            var CooperationContract = _CooperationContractRepository.GetById(id);
            if (CooperationContract != null)
            {
                _CooperationContractRepository.Delete(CooperationContract);
            }
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}
