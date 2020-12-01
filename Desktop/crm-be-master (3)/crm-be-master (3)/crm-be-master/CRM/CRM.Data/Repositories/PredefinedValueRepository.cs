
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IPredefinedValueRepository : IRepository<PredefinedValue> { }
    public class PredefinedValueRepository : RepositoryBase<PredefinedValue>, IPredefinedValueRepository
    {
        public PredefinedValueRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
