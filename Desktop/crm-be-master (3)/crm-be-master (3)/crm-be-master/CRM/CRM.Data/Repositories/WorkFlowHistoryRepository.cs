using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface IWorkFlowHistoryRepository : IRepository<WorkFlowHistory>
    {

    }

    public class WorkFlowHistoryRepository : RepositoryBase<WorkFlowHistory>, IWorkFlowHistoryRepository
    {
        public WorkFlowHistoryRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
