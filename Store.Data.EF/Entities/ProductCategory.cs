using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data.EF.Entities
{
    public class ProductCategory : BaseEntity
    {
        public long CategoryId { get; set; }
        public long ProductId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}
