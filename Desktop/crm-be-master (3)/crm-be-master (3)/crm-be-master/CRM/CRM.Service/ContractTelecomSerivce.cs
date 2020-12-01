using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface IContractTelecomService
    {
        IEnumerable< ContractTelecom> GetContractTelecoms();
        IEnumerable< ContractTelecom> GetContractTelecoms(Expression<Func< ContractTelecom, bool>> where);
        ContractTelecom GetContractTelecom(Guid id);
        void CreateContractTelecom( ContractTelecom  ContractTelecom);
        void EditContractTelecom( ContractTelecom  ContractTelecom);
        void RemoveContractTelecom(Guid id);
        void RemoveContractTelecom( ContractTelecom  ContractTelecom);
        void SaveContractTelecom();
    }
    public class ContractTelecomService : IContractTelecomService
    {
        private readonly IContractTelecomRepository _contractTelecomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ContractTelecomService(IContractTelecomRepository contractTelecomRepository, IUnitOfWork unitOfWork)
        {
            _contractTelecomRepository = contractTelecomRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateContractTelecom(ContractTelecom ContractTelecom)
        {
            _contractTelecomRepository.Add(ContractTelecom);
        }

        public void EditContractTelecom(ContractTelecom ContractTelecom)
        {
            _contractTelecomRepository.Update(ContractTelecom);
        }

        public ContractTelecom GetContractTelecom(Guid id)
        {
            return _contractTelecomRepository.GetById(id);
        }

        public IEnumerable<ContractTelecom> GetContractTelecoms()
        {
            return _contractTelecomRepository.GetAll();
        }

        public IEnumerable<ContractTelecom> GetContractTelecoms(Expression<Func<ContractTelecom, bool>> where)
        {
            return _contractTelecomRepository.GetMany(where);
        }

        public void RemoveContractTelecom(Guid id)
        {
            var contractTelecom = _contractTelecomRepository.GetById(id);
            _contractTelecomRepository.Delete(contractTelecom);
        }

        public void RemoveContractTelecom(ContractTelecom ContractTelecom)
        {
            _contractTelecomRepository.Delete(ContractTelecom);
        }

        public void SaveContractTelecom()
        {
            _unitOfWork.Commit();
        }
    }
}
