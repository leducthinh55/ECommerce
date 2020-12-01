using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface ICommonTelecomserviceRepository : IRepository<CommonTelecomservice>
    {
    }

    public class CommonTelecomserviceRepository : RepositoryBase<CommonTelecomservice>, ICommonTelecomserviceRepository
    {
        public CommonTelecomserviceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
