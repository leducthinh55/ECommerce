using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface IGlobalVariableRepository : IRepository<GlobalVariable>
    {

    }
    public class GlobalVariableRepository : RepositoryBase<GlobalVariable>, IGlobalVariableRepository
    {
        public GlobalVariableRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
