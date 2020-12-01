using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        CRMContext dbContext;

        public CRMContext Init()
        {
            return dbContext ?? (dbContext = new CRMContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
