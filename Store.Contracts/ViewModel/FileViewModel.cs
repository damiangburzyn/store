using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Store.Contracts.ViewModel
{
    public class FileViewModel : BaseViewModel
    { 
        public string FileName { get; set; }
        public long CategoryId { get; set; }
      
        private ICollection<ProductFileViewModel> ProductFiles { get; } = new List<ProductFileViewModel>();

        public IEnumerable<ProductViewModel> Products => ProductFiles.Select(cm => cm.Product);

        public int SortOrder { get; set; }
    }
}
