using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IAmountRepository : IRepository<Amount>
    {

    }
    public class AmountRepository : RepositoryBase<Amount>, IAmountRepository
    {
        public AmountRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
