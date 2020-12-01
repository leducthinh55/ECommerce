using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface IPriceHistoryRepository : IRepository<PriceHistory>
    {

    }
    public class PriceHistoryRepository : RepositoryBase<PriceHistory>, IPriceHistoryRepository
    {
        // change protected to public
        public PriceHistoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
