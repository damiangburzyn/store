using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Store.Contracts.ViewModel
{
    public class CategoryViewModel : BaseViewModel
    {
        public CategoryViewModel()
        {
            Logo = new ContentViewModel();
        }

        public int SortOrder { get; set; }
        
        public  string Name { get; set; }
       
        public string ShortName { get; set; }
       
        public ContentViewModel Logo { get; set; }
      
        public long? ParentCategoryId { get; set; }

        public  CategoryViewModel ParentCategory { get; set; }

        public  ICollection<CategoryViewModel> SubCategories { get; set; }

        private ICollection<ProductCategoryViewModel> ProductCategories { get; } = new List<ProductCategoryViewModel>();

        public IEnumerable<ProductViewModel> Products => ProductCategories.Select(e => e.Product);
    }
}
