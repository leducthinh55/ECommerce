using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IIdentityCardRepository : IRepository<IdentityCard>
    {

    }
    public class IdentityCardRepository : RepositoryBase<IdentityCard>, IIdentityCardRepository
    {
        public IdentityCardRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

}
