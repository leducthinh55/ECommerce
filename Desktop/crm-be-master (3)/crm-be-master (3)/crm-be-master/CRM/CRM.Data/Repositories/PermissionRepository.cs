using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IPermissionRepository : IRepository<HsPermission>
    {

    }
    public class PermissionRepository : RepositoryBase<HsPermission>, IPermissionRepository
    {
        public PermissionRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
