using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface ITransactionLogRepository : IRepository<TransactionLog>
    {

    }
    public class TransactionLogRepository : RepositoryBase<TransactionLog>, ITransactionLogRepository
    {
        public TransactionLogRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

}
