using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    

    public interface ICooperationContractRepository : IRepository<CooperationContract>
    {

    }

    public class CooperationContractRepository : RepositoryBase<CooperationContract>, ICooperationContractRepository
    {
        public CooperationContractRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
