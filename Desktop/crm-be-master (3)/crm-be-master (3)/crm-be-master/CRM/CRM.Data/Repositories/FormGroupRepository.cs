using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface IFormGroupRepository : IRepository<FormGroup>
    {

    }
    public class FormGroupRepository : RepositoryBase<FormGroup>, IFormGroupRepository
    {
        public FormGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
