using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IDeputyRepository : IRepository<Deputy>
    {

    }
    public class DeputyRepository : RepositoryBase<Deputy>, IDeputyRepository
    {
        public DeputyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
