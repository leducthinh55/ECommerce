using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface IContractTelecomRepository : IRepository<ContractTelecom>
    {
    }

    public class ContractTelecomRepository : RepositoryBase<ContractTelecom>, IContractTelecomRepository
    {
        public ContractTelecomRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
