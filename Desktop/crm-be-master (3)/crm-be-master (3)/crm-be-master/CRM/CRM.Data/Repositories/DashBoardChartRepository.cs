using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface IDashBoardChartRepository : IRepository<DashBoardChart>
    {

    }
    public class DashBoardChartRepository : RepositoryBase<DashBoardChart>, IDashBoardChartRepository
    {
        public DashBoardChartRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
