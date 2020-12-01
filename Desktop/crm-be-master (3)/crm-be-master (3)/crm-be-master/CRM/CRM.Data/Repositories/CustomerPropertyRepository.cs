using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface ICustomerPropertyRepository : IRepository<CustomerProperty>
    {

    }
    public class CustomerPropertyRepository : RepositoryBase<CustomerProperty>, ICustomerPropertyRepository
    {
        public CustomerPropertyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
