using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IPermissionOfRoleRepository : IRepository<HsPermissionOfRole>
    {
    }
    public class PermissionOfRoleRepository : RepositoryBase<HsPermissionOfRole>, IPermissionOfRoleRepository
    {
        public PermissionOfRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
