using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IRoleOfUserRepository : IRepository<HsRoleOfUser>
    {

    }

    public class RoleOfUserRepository : RepositoryBase<HsRoleOfUser>, IRoleOfUserRepository
    {
        public RoleOfUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
    
}
