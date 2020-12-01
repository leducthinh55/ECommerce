using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IFormRepository : IRepository<Form>
    {

    }
    public class FormRepository : RepositoryBase<Form>, IFormRepository
    {
        public FormRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
