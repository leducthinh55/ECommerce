using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
   
    public interface ISubCoContractRepository : IRepository<SubCoContract>
    {

    }

    public class SubCoContractRepository : RepositoryBase<SubCoContract>, ISubCoContractRepository
    {
        public SubCoContractRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
