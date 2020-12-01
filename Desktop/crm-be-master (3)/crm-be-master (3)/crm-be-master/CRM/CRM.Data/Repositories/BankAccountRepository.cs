using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {

    }

    public class BankAccountRepository : RepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
