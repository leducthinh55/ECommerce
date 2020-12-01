using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{


    public interface ISubCoContractServiceItemRepository : IRepository<SubCoContractServiceItem>
    {

    }

    public class SubCoContractServiceItemRepository : RepositoryBase<SubCoContractServiceItem>, ISubCoContractServiceItemRepository
    {
        public SubCoContractServiceItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
