using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface IContractAppendixRepository : IRepository<ContractAppendix>
    {
    }

    public class ContractAppendixRepository : RepositoryBase<ContractAppendix>, IContractAppendixRepository
    {
        public ContractAppendixRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
