using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IContractRepository : IRepository<Contract>
    {

    }

    public class ContractRepository : RepositoryBase<Contract>, IContractRepository
    {
        public ContractRepository(IDbFactory dbFactory) : base(dbFactory) { }

    }
}
