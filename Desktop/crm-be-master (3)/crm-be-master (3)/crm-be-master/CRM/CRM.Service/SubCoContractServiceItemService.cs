using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface ISubCoContractServiceItemService
    {
        IEnumerable<SubCoContractServiceItem> GetSubCoContractServiceItems();
        IEnumerable<SubCoContractServiceItem> GetSubCoContractServiceItems(Expression<Func<SubCoContractServiceItem, bool>> where);
        SubCoContractServiceItem GetSubCoContractServiceItem(Guid id);
        void CreateSubCoContractServiceItem(SubCoContractServiceItem SubCoContractServiceItem);
        void UpdateSubCoContractServiceItem(SubCoContractServiceItem SubCoContractServiceItem);
        void DeleteSubCoContractServiceItem(Guid id);
        void SaveChange();
    }
    public class SubCoContractServiceItemService : ISubCoContractServiceItemService
    {
        private readonly ISubCoContractServiceItemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SubCoContractServiceItemService(ISubCoContractServiceItemRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateSubCoContractServiceItem(SubCoContractServiceItem SubCoContractServiceItem)
        {
            _repository.Add(SubCoContractServiceItem);
        }

        public void DeleteSubCoContractServiceItem(Guid id)
        {
            var SubCoContractServiceItem = _repository.GetById(id);
            _repository.Delete(SubCoContractServiceItem);
        }

        public SubCoContractServiceItem GetSubCoContractServiceItem(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<SubCoContractServiceItem> GetSubCoContractServiceItems()
        {
            return _repository.GetAll();
        }

        public IEnumerable<SubCoContractServiceItem> GetSubCoContractServiceItems(Expression<Func<SubCoContractServiceItem, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void UpdateSubCoContractServiceItem(SubCoContractServiceItem SubCoContractServiceItem)
        {
            _repository.Update(SubCoContractServiceItem);
        }
    }
}
