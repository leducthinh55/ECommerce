using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface ICustomerWorkFlowRepository : IRepository<CustomerWorkFlow>
    {

    }

    public class CustomerWorkFlowRepository : RepositoryBase<CustomerWorkFlow>, ICustomerWorkFlowRepository
    {
        public CustomerWorkFlowRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
