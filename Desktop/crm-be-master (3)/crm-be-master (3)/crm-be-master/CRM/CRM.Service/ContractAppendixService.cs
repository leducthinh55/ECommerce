using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    //ContractAppendix
    public interface IContractAppendixService
    {
        IEnumerable<ContractAppendix> GetContractAppendixs();
        IEnumerable<ContractAppendix> GetContractAppendixs(Expression<Func<ContractAppendix, bool>> where);
        ContractAppendix GetContractAppendix(Guid id);
        void CreateContractAppendix(ContractAppendix ContractAppendix);
        void EditContractAppendix(ContractAppendix ContractAppendix);
        void RemoveContractAppendix(Guid id);
        void RemoveContractAppendix(ContractAppendix ContractAppendix);
        void SaveContractAppendix();
        bool IsRunningAppendice(ContractAppendix contractAppendix, DateTime date);
    }
    public class ContractAppendixService : IContractAppendixService
    {
        private readonly IContractAppendixRepository _ContractAppendixRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ContractAppendixService(IContractAppendixRepository ContractAppendixRepository, IUnitOfWork unitOfWork)
        {
            _ContractAppendixRepository = ContractAppendixRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateContractAppendix(ContractAppendix ContractAppendix)
        {
            _ContractAppendixRepository.Add(ContractAppendix);
        }

        public void EditContractAppendix(ContractAppendix ContractAppendix)
        {
            _ContractAppendixRepository.Update(ContractAppendix);
        }

        public ContractAppendix GetContractAppendix(Guid id)
        {
            return _ContractAppendixRepository.GetById(id);
        }

        public IEnumerable<ContractAppendix> GetContractAppendixs()
        {
            return _ContractAppendixRepository.GetAll();
        }

        public IEnumerable<ContractAppendix> GetContractAppendixs(Expression<Func<ContractAppendix, bool>> where)
        {
            return _ContractAppendixRepository.GetMany(where);
        }

        public void RemoveContractAppendix(Guid id)
        {
            var ContractAppendix = _ContractAppendixRepository.GetById(id);
            _ContractAppendixRepository.Delete(ContractAppendix);
        }

        public void RemoveContractAppendix(ContractAppendix ContractAppendix)
        {
            _ContractAppendixRepository.Delete(ContractAppendix);
        }

        public void SaveContractAppendix()
        {
            _unitOfWork.Commit();
        }
        //public IEnumerable<ContractAppendix> GetContractAppendiesOnDate(Contract contract, DateTime date)
        //{
        //    var appendices = contract.ContractAppendices.Where(a => IsRunningAppendice(a, date) && a.Type == (int)ContractAppendixType.AreaRent);
        //    return appendices;
        //}

        public bool IsRunningAppendice(ContractAppendix contractAppendix, DateTime date)
        {
            var result = true;
            result = result && contractAppendix.DateStart <= date.Date;
            result = result && contractAppendix.DateEnd != null ? contractAppendix.DateEnd.Value >= date.Date : true;
            return result;
        }
    }
}
