
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IHsWorkFlowConnectionRepository : IRepository<HsWorkFlowConnection>
    { }
    public class HsWorkFlowConnectionRepository : RepositoryBase<HsWorkFlowConnection>, IHsWorkFlowConnectionRepository
    {
        public HsWorkFlowConnectionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
