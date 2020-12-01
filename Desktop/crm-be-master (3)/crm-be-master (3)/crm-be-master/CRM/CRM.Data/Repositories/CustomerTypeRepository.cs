using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface ICustomerTypeRepository : IRepository<CustomerType>
    {

    }
    public class CustomerTypeRepository : RepositoryBase<CustomerType>, ICustomerTypeRepository
    {
        public CustomerTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
