using System;
using System.IO;
using System.Threading.Tasks;
using CRM.Data.Infrastructure;
using CRM.Model;
using Microsoft.AspNetCore.Http;

namespace CRM.Data.Repositories
{
    public interface IHsTemplateRepository : IRepository<HsTemplate>
    {

    }
    public class HsTemplateRepository : RepositoryBase<HsTemplate>, IHsTemplateRepository
    {
        public HsTemplateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
       
    }
}
