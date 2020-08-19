using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Store.Data.EF.Entities
{
    public class Category : BaseEntity
    {
        public int SortOrder { get; set; }
        public virtual string Name { get; set; }
        public string ShortName { get; set; }
        public string Logo { get; set; }
        public long? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        private ICollection<ProductCategory> ProductCategories { get; } = new List<ProductCategory>();
        public IEnumerable<Product> Products => ProductCategories.Select(e => e.Product);
    }
}
