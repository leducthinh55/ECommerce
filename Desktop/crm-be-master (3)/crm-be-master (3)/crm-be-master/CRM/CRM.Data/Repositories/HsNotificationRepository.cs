
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IHsNotificationRepository : IRepository<HsNotification>
    {

    }

    public class HsNotificationRepository : RepositoryBase<HsNotification>, IHsNotificationRepository
    {
        public HsNotificationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
