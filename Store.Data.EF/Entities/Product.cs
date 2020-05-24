using Store.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace Store.Data.EF.Entities
{
    public class Product: BaseEntity
    {
        public bool IsBestseller { get; set; }
        public string Name { get; set; }

        public decimal CurrentPrice { get; set; }
        public decimal PreviousPrice { get; set; }

        public string Description { get; set; }
        public int Count { get; set; }

        public virtual ICollection<GalleryImage> Images { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        public virtual ICollection<ProductDeliveryMethod> DeliveryMethods { get; set; }
        

        public long[] ConnectedProdIds { get; set; }


        public ICollection<ProductFile> ProductFiles { get; } = new List<ProductFile>();

        public IEnumerable<File> Attachements => ProductFiles.OrderBy(pf => pf.SortOrder).Select(e => e.File);

     

        public ICollection<ProductCategory> ProductCategories { get; } = new List<ProductCategory>();

        public IEnumerable<Category> Categories => ProductCategories.Select(e => e.Category);

        private IEnumerable<Category> _categories;

   

        public void StoreCategories(IEnumerable<Category> categories)
        {
            _categories = categories;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categories;
        }

     

        private IEnumerable<File> _files;

        public void StoreFiles(IEnumerable<File> files)
        {
            _files = files;
        }

        public IEnumerable<File> GetFiles()
        {
            return _files;
        }

    }
}
