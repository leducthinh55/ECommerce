using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IGroupRepository : IRepository<HsGroup>
    {

    }
    public class GroupRepository : RepositoryBase<HsGroup>, IGroupRepository
    {
        public GroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
