using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface ITelecomserviceParameterRepository : IRepository<TelecomserviceParameter>
    {

    }
    public class TelecomserviceParameterRepository : RepositoryBase<TelecomserviceParameter>, ITelecomserviceParameterRepository
    {
        public TelecomserviceParameterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
