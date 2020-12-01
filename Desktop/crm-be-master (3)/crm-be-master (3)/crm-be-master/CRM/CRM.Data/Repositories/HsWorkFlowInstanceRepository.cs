
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IHsWorkFlowInstanceRepository : IRepository<HsWorkFlowInstance>
    {

    }

    public class HsWorkFlowInstanceRepository : RepositoryBase<HsWorkFlowInstance>, IHsWorkFlowInstanceRepository
    {
        public HsWorkFlowInstanceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
