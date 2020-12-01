using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IChangeLogRepository : IRepository<ChangeLog>
    {

    }
    public class ChangeLogRepository : RepositoryBase<ChangeLog>, IChangeLogRepository
    {
        public ChangeLogRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

}
