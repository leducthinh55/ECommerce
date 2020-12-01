using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IGroupUserRepository : IRepository<HsGroupUser>
    {

    }
    public class GroupUserRepository : RepositoryBase<HsGroupUser>, IGroupUserRepository
    {
        public GroupUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
