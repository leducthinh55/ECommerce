using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IRoleOfGroupRepository : IRepository<HsRoleOfGroup>
    {

    }

    public class RoleOfGroupRepository : RepositoryBase<HsRoleOfGroup>, IRoleOfGroupRepository
    {
        public RoleOfGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

}
