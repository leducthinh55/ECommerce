using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class Product
    {
        public Product()
        {
            PriceHistories = new List<PriceHistory>();
            Attributes = new List<AttributeValue>();
            ProductCategories = new List<ProductCategory>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<PriceHistory> PriceHistories { get; set; }
        public virtual ICollection<AttributeValue> Attributes { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
