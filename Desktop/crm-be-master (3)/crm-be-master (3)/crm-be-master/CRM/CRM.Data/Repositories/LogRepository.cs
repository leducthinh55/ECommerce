using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {

    }

    public class LogRepository : RepositoryBase<Log>, ILogRepository
    {
        public LogRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
