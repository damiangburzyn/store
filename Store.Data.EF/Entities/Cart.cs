using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data.EF.Entities
{
  public   class Cart :BaseEntity
    {
        public string CartId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public virtual Product Product { get; set; }
    }
}
