using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface ITelecomserviceRepository:  IRepository<Telecomservice>
    {
    }
    public class TelecomserviceRepository : RepositoryBase<Telecomservice>, ITelecomserviceRepository
    {
        public TelecomserviceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
