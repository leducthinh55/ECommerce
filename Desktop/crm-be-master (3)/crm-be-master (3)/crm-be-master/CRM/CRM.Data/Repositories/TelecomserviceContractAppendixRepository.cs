using CRM.Data.Infrastructure;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Repositories
{
    public interface ITelecomserviceContractAppendixRepository: IRepository<TelecomserviceContractAppendix>
    {

    }
    public class TelecomserviceContractAppendixRepository : RepositoryBase<TelecomserviceContractAppendix>, ITelecomserviceContractAppendixRepository
    {
        public TelecomserviceContractAppendixRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
