
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IHsWorkFlowRepository : IRepository<HsWorkFlow>
    {

    }

    public class HsWorkFlowRepository : RepositoryBase<HsWorkFlow>, IHsWorkFlowRepository
    {
        public HsWorkFlowRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
