using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface ICustomerTypeService
    {
        IEnumerable<CustomerType> GetCustomerTypes();
        IEnumerable<CustomerType> GetCustomerTypes(Expression<Func<CustomerType,bool>> where);
        CustomerType GetCustomerType(Guid id);
        void CreateCustomerType(CustomerType customerType);
        void UpdateCustomerType(CustomerType customerType);
        void SaveChanges();
    }
    public class CustomerTypeService : ICustomerTypeService
    {
        private readonly ICustomerTypeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerTypeService(ICustomerTypeRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateCustomerType(CustomerType customerType)
        {
            _repository.Add(customerType);
        }

        public CustomerType GetCustomerType(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<CustomerType> GetCustomerTypes()
        {
            return _repository.GetAll();
        }

        public IEnumerable<CustomerType> GetCustomerTypes(Expression<Func<CustomerType, bool>> where)
        {
            return _repository.GetMany(where); 
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateCustomerType(CustomerType customerType)
        {
            _repository.Update(customerType);
        }
    }
}
