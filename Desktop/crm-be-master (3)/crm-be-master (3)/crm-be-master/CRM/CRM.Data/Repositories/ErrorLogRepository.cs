using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IErrorLogRepository : IRepository<ErrorLog>
    {

    }

    public class ErrorLogRepository : RepositoryBase<ErrorLog>, IErrorLogRepository
    {
        public ErrorLogRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
