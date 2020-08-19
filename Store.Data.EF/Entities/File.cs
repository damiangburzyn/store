using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Store.Data.EF.Entities
{
    public class File : BaseEntity
    { 
        public string FileName { get; set; }   
        private ICollection<ProductFile> ProductFiles { get; } = new List<ProductFile>();
        public IEnumerable<Product> Products => ProductFiles.Select(cm => cm.Product);
        public int SortOrder { get; set; }
    }
}
