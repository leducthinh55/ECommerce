
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface ICustomerPropertyService
    {
        IEnumerable<CustomerProperty> GetCustomerPropertys();
        IEnumerable<CustomerProperty> GetCustomerPropertys(Expression<Func<CustomerProperty, bool>> where);
        CustomerProperty GetCustomerProperty(Guid id);
        void CreateCustomerProperty(CustomerProperty CustomerProperty);
        void UpdateCustomerProperty(CustomerProperty CustomerProperty);
        void DeleteCustomerProperty(Guid id);
        void SaveChange();
    }
    public class CustomerPropertyService : ICustomerPropertyService
    {
        private readonly ICustomerPropertyRepository _CustomerPropertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerPropertyService(ICustomerPropertyRepository CustomerPropertyRepository, IUnitOfWork unitOfWork)
        {
            _CustomerPropertyRepository = CustomerPropertyRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateCustomerProperty(CustomerProperty CustomerProperty)
        {
            _CustomerPropertyRepository.Add(CustomerProperty);
        }

        public void UpdateCustomerProperty(CustomerProperty CustomerProperty)
        {
            _CustomerPropertyRepository.Update(CustomerProperty);
        }

        public IEnumerable<CustomerProperty> GetCustomerPropertys(Expression<Func<CustomerProperty, bool>> where)
        {
            return _CustomerPropertyRepository.GetMany(where);
        }

        public CustomerProperty GetCustomerProperty(Guid id)
        {
            return _CustomerPropertyRepository.GetById(id);
        }

        public IEnumerable<CustomerProperty> GetCustomerPropertys()
        {
            return _CustomerPropertyRepository.GetAll();
        }

        public void DeleteCustomerProperty(Guid id)
        {
            var CustomerProperty = _CustomerPropertyRepository.GetById(id);
            CustomerProperty.IsDeleted = true;
            _CustomerPropertyRepository.Update(CustomerProperty);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}
