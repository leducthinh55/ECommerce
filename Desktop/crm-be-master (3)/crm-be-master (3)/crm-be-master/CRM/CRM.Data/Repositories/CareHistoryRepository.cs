using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface ICareHistoryRepository : IRepository<CareHistory>
    {

    }

    public class CareHistoryRepository : RepositoryBase<CareHistory>, ICareHistoryRepository
    {
        public CareHistoryRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
