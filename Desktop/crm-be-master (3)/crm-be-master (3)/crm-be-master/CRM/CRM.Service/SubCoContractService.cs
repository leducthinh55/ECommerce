using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    

    public interface ISubCoContractService
    {
        IEnumerable<SubCoContract> GetSubCoContracts();
        IEnumerable<SubCoContract> GetSubCoContracts(Expression<Func<SubCoContract, bool>> where);
        SubCoContract GetSubCoContract(Guid id);
        void CreateSubCoContract(SubCoContract SubCoContract);
        void UpdateSubCoContract(SubCoContract SubCoContract);
        void DeleteSubCoContract(SubCoContract SubCoContract);
        bool IsContractOfYear(SubCoContract SubCoContract, int year);
        void SaveChange();

    }
    public class SubCoContractService : ISubCoContractService
    {
        private readonly ISubCoContractRepository _SubCoContractRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubCoContractService(ISubCoContractRepository SubCoContractRepository, IUnitOfWork unitOfWork)
        {
            _SubCoContractRepository = SubCoContractRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateSubCoContract(SubCoContract SubCoContract)
        {
            _SubCoContractRepository.Add(SubCoContract);
        }

        public void DeleteSubCoContract(SubCoContract SubCoContract)
        {
            _SubCoContractRepository.Delete(SubCoContract);

        }

        public SubCoContract GetSubCoContract(Guid id)
        {
            return _SubCoContractRepository.GetById(id);
        }

        public IEnumerable<SubCoContract> GetSubCoContracts()
        {
            return _SubCoContractRepository.GetAll();
        }

        public void UpdateSubCoContract(SubCoContract SubCoContract)
        {
            _SubCoContractRepository.Update(SubCoContract);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<SubCoContract> GetSubCoContracts(Expression<Func<SubCoContract, bool>> where)
        {
            return _SubCoContractRepository.GetMany(where);
        }

        public bool IsContractOfYear(SubCoContract SubCoContract, int year)
        {
            if (SubCoContract.DateStart.Year > year) return false;
            if(SubCoContract.DateEnd != null)
            {
                if (SubCoContract.DateEnd.Value.Year < year) return false;
            }
            return true;
        }
    }
}
