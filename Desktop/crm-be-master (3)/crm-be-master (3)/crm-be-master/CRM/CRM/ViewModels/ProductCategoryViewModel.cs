using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class ProductsCategoryCM
    {
        public IEnumerable<Guid> ProductIds { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class ProductCategoriesCM
    {
        public IEnumerable<Guid> CategoryIds { get; set; }
        public Guid ProductId { get; set; }
    }
    public class ProductCategoriesDM
    {
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
    }
}
