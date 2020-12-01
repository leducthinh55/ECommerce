using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface IEventLogFileRepository : IRepository<EventLogFile>
    {

    }
    public class EventLogFileRepository : RepositoryBase<EventLogFile>, IEventLogFileRepository
    {
        public EventLogFileRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
