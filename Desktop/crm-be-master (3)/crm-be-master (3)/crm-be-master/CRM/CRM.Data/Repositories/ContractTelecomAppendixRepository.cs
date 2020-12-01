using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface IContractTelecomAppendixRepository : IRepository<ContractTelecomAppendix>
    {
    }
    public class ContractTelecomAppendixRepository : RepositoryBase<ContractTelecomAppendix>, IContractTelecomAppendixRepository
    {
        public ContractTelecomAppendixRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
