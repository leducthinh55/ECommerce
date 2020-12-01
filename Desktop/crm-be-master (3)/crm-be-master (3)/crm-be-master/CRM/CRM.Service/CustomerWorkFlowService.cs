using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface ICustomerWorkFlowService
    {
        IEnumerable<CustomerWorkFlow> GetCustomerWorkFlows();
        IEnumerable<CustomerWorkFlow> GetCustomerWorkFlows(Expression<Func<CustomerWorkFlow, bool>> where);
        CustomerWorkFlow GetCustomerWorkFlow(Guid id);
        void CreateCustomerWorkFlow(CustomerWorkFlow CustomerWorkFlow);
        void EditCustomerWorkFlow(CustomerWorkFlow CustomerWorkFlow);
        void RemoveCustomerWorkFlow(Guid id);
        void SaveCustomerWorkFlow();
    }

    public class CustomerWorkFlowService : ICustomerWorkFlowService
    {
        private readonly ICustomerWorkFlowRepository _CustomerWorkFlowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerWorkFlowService(ICustomerWorkFlowRepository CustomerWorkFlowRepository, IUnitOfWork unitOfWork)
        {
            this._CustomerWorkFlowRepository = CustomerWorkFlowRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateCustomerWorkFlow(CustomerWorkFlow CustomerWorkFlow)
        {
            _CustomerWorkFlowRepository.Add(CustomerWorkFlow);
        }

        public void EditCustomerWorkFlow(CustomerWorkFlow CustomerWorkFlow)
        {
            var entity = _CustomerWorkFlowRepository.GetById(CustomerWorkFlow.Id);
            entity = CustomerWorkFlow;
            _CustomerWorkFlowRepository.Update(entity);
        }

        public CustomerWorkFlow GetCustomerWorkFlow(Guid id)
        {
            return _CustomerWorkFlowRepository.GetById(id);
        }

        public IEnumerable<CustomerWorkFlow> GetCustomerWorkFlows()
        {
            return _CustomerWorkFlowRepository.GetAll();
        }

        public IEnumerable<CustomerWorkFlow> GetCustomerWorkFlows(Expression<Func<CustomerWorkFlow, bool>> where)
        {
            return _CustomerWorkFlowRepository.GetMany(where);
        }

        public void RemoveCustomerWorkFlow(Guid id)
        {
            var entity = _CustomerWorkFlowRepository.GetById(id);
            _CustomerWorkFlowRepository.Delete(entity);
        }

        public void SaveCustomerWorkFlow()
        {
            _unitOfWork.Commit();
        }
    }
}
