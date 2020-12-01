using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface IGlobalVariableValueRepository : IRepository<GlobalVariableValue>
    {

    }
    public class GlobalVariableValueRepository : RepositoryBase<GlobalVariableValue>, IGlobalVariableValueRepository
    {
        public GlobalVariableValueRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
