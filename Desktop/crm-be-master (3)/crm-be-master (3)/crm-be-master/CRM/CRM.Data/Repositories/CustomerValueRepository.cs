using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface ICustomerValueRepository : IRepository<CustomerValue>
    {

    }
    public class CustomerValueRepository : RepositoryBase<CustomerValue>, ICustomerValueRepository
    {
        public CustomerValueRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
