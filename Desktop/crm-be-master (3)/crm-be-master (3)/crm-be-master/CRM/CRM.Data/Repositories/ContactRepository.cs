using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {

    }

    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
