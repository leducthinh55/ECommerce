using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IAttributeValueRepository : IRepository<AttributeValue>
    {

    }
    public class AttributeValueRepository : RepositoryBase<AttributeValue>, IAttributeValueRepository
    {
        public AttributeValueRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
