using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IPersonnelRepository : IRepository<Personnel>
    {

    }
    public class PersonnelRepository : RepositoryBase<Personnel>, IPersonnelRepository
    {
        public PersonnelRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
