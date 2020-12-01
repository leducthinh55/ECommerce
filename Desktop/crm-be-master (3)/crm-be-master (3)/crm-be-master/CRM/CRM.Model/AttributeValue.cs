using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class AttributeValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductAttributeId { get; set; }

        public String Value { get; set; }
        public Guid? PredefinedId { get; set; }



        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductAttributeId")]
        public virtual ProductAttribute ProductAttribute { get; set; }

        [ForeignKey("PredefinedId")]
        public virtual PredefinedValue Predefined { get; set; }
    }
}
