using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IBankAccountService
    {
        IEnumerable<BankAccount> GetBankAccounts();
        BankAccount GetBankAccount(Guid id);
        void CreateBankAccount(BankAccount hsBankAccount);
        void EditBankAccount(BankAccount hsBankAccount);
        void RemoveBankAccount(Guid id);
        void RemoveBankAccount(Expression<Func<BankAccount, bool>> where);
        void SaveBankAccount();
    }
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BankAccountService(IBankAccountRepository bankAccountRepository, IUnitOfWork unitOfWork)
        {
            _bankAccountRepository = bankAccountRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BankAccount> GetBankAccounts()
        {
            return _bankAccountRepository.GetAll();
        }

        public BankAccount GetBankAccount(Guid id)
        {
            return _bankAccountRepository.GetById(id);
        }

        public void CreateBankAccount(BankAccount hsBankAccount)
        {
            _bankAccountRepository.Add(hsBankAccount);
            _unitOfWork.Commit();
        }

        public void EditBankAccount(BankAccount hsBankAccount)
        {
            _bankAccountRepository.Update(hsBankAccount);
        }

        public void RemoveBankAccount(Guid id)
        {
            var entity = _bankAccountRepository.GetById(id);
            _bankAccountRepository.Delete(entity);
        }

        public void RemoveBankAccount(Expression<Func<BankAccount, bool>> where)
        {
            _bankAccountRepository.Delete(where);
        }

        public void UpdateBankAccount(ICollection<BankAccount> bankAccounts)
        {
            throw new NotImplementedException();
        }

        public void SaveBankAccount()
        {
            _unitOfWork.Commit();
        }
    }
}
