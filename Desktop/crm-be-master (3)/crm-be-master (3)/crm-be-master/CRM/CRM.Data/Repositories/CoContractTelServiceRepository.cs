using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    

    public interface ICoContractTelServiceRepository : IRepository<CoContractTelService>
    {

    }

    public class CoContractTelServiceRepository : RepositoryBase<CoContractTelService>, ICoContractTelServiceRepository
    {
        public CoContractTelServiceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
