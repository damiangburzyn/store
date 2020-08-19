using System;
using System.Collections.Generic;
using System.Text;


namespace Store.Data.EF.Entities
{
    public class ProductFile : BaseEntity
    {
        public long FileId { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual File File { get; set; }
        public int SortOrder { get; set; }
    }
}
