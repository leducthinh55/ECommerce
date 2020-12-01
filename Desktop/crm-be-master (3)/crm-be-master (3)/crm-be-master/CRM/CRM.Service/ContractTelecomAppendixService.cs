using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface IContractTelecomAppendixService
    {
        IEnumerable<ContractTelecomAppendix> GetContractTelecomAppendixs();
        IEnumerable<ContractTelecomAppendix> GetContractTelecomAppendixs(Expression<Func<ContractTelecomAppendix, bool>> where);
        ContractTelecomAppendix GetContractTelecomAppendix(Guid id);
        void CreateContractTelecomAppendix(ContractTelecomAppendix ContractTelecomAppendix);
        void EditContractTelecomAppendix(ContractTelecomAppendix ContractTelecomAppendix);
        void RemoveContractTelecomAppendix(Guid id);
        void RemoveContractTelecomAppendix(ContractTelecomAppendix ContractTelecomAppendix);
        void SaveContractTelecomAppendix();
        bool IsRunningAppendice(ContractTelecomAppendix contractAppendix, int currentYear);
    }
    public class ContractTelecomAppendixService : IContractTelecomAppendixService
    {
        private readonly IContractTelecomAppendixRepository _ContractTelecomAppendixRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ContractTelecomAppendixService(IContractTelecomAppendixRepository ContractTelecomAppendixRepository, IUnitOfWork unitOfWork)
        {
            _ContractTelecomAppendixRepository = ContractTelecomAppendixRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateContractTelecomAppendix(ContractTelecomAppendix ContractTelecomAppendix)
        {
            _ContractTelecomAppendixRepository.Add(ContractTelecomAppendix);
        }

        public void EditContractTelecomAppendix(ContractTelecomAppendix ContractTelecomAppendix)
        {
            _ContractTelecomAppendixRepository.Update(ContractTelecomAppendix);
        }

        public ContractTelecomAppendix GetContractTelecomAppendix(Guid id)
        {
            return _ContractTelecomAppendixRepository.GetById(id);
        }

        public IEnumerable<ContractTelecomAppendix> GetContractTelecomAppendixs()
        {
            return _ContractTelecomAppendixRepository.GetAll();
        }

        public IEnumerable<ContractTelecomAppendix> GetContractTelecomAppendixs(Expression<Func<ContractTelecomAppendix, bool>> where)
        {
            return _ContractTelecomAppendixRepository.GetMany(where);
        }

        public bool IsRunningAppendice(ContractTelecomAppendix contractAppendix, int currentYear)
        {
            var currentDate = DateTime.Now.Date;
            var beginDate = new DateTime(currentYear, 01, 01);
            if (currentYear != DateTime.Now.Year) currentDate = new DateTime(currentYear, 31, 12);
            if (contractAppendix.DateAccept == null) return false;
            if (contractAppendix.DateEnd < beginDate) return false;
            if (contractAppendix.DateAccept > currentDate) return false;
            return true;
        }

        public void RemoveContractTelecomAppendix(Guid id)
        {
            var ContractTelecomAppendix = _ContractTelecomAppendixRepository.GetById(id);
            _ContractTelecomAppendixRepository.Delete(ContractTelecomAppendix);
        }

        public void RemoveContractTelecomAppendix(ContractTelecomAppendix ContractTelecomAppendix)
        {
            _ContractTelecomAppendixRepository.Delete(ContractTelecomAppendix);
        }

        public void SaveContractTelecomAppendix()
        {
            _unitOfWork.Commit();
        }
    }
}
