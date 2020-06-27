using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace Store.Contracts.ViewModel
{
    public class ProductViewModel :  BaseViewModel
    {
        public bool IsBestseller { get; set; }
        public string Name { get; set; }

        public decimal CurrentPrice { get; set; }
        public decimal PreviousPrice { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public virtual IList<ContentViewModel> Images { get; set; }

        public virtual ICollection<MovieViewModel> Movies { get; set; }

      

        public long[] ConnectedProdIds { get; set; }




        public IList<ContentViewModel> Attachements { get; set; } = new List<ContentViewModel>();

     

        public ICollection<ProductCategoryViewModel> ProductCategories { get; } = new List<ProductCategoryViewModel>();

        public IEnumerable<CategoryViewModel> Categories => ProductCategories.Select(e => e.Category);

        private IEnumerable<CategoryViewModel> _categories;

   

        public void StoreCategories(IEnumerable<CategoryViewModel> categories)
        {
            _categories = categories;
        }

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            return _categories;
        }

     

        private IEnumerable<FileViewModel> _files;

        public void StoreFiles(IEnumerable<FileViewModel> files)
        {
            _files = files;
        }

        public IEnumerable<FileViewModel> GetFiles()
        {
            return _files;
        }

    }
}
