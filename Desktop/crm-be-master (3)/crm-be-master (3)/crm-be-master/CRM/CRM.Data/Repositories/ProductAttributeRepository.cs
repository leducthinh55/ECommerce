
using CRM.Data.Infrastructure;
using CRM.Model;

namespace CRM.Data.Repositories
{
    public interface IProductAttributeRepository : IRepository<ProductAttribute>
    {

    }
    public class ProductAttributeRepository : RepositoryBase<ProductAttribute>, IProductAttributeRepository
    {
        public ProductAttributeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
