using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IBuildingRepository : IRepository<Building>
    {

    }

    public class BuildingRepository : RepositoryBase<Building>, IBuildingRepository
    {
        public BuildingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
