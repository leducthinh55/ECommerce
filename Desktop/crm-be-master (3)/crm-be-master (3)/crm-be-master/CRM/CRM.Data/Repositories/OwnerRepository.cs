using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IOwnerRepository : IRepository<Owner>
    {

    }
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
