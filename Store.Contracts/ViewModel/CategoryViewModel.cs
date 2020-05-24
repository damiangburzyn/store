using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Store.Contracts.ViewModel
{
    public class CategoryViewModel : BaseViewModel
    {
        public int SortOrder { get; set; }
        public virtual string Name { get; set; }
        public string ShortName { get; set; }
        [MediaFile(relatedProperty: "LogoData")]
        public string Logo { get; set; }
        [Media]
        public string LogoData { get; set; }
        public long? ParentCategoryId { get; set; }

        public virtual CategoryViewModel ParentCategory { get; set; }

        public virtual ICollection<CategoryViewModel> SubCategories { get; set; }

        private ICollection<ProductCategoryViewModel> ProductCategories { get; } = new List<ProductCategoryViewModel>();

        public IEnumerable<ProductViewModel> Products => ProductCategories.Select(e => e.Product);
    }
}
