using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface IEventLogRepository : IRepository<EventLog>
    {

    }
    public class EventLogRepository : RepositoryBase<EventLog>, IEventLogRepository
    {
        public EventLogRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
