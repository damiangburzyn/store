using System;
using System.Collections.Generic;
using System.Text;


namespace Store.Contracts.ViewModel
{
    public class ProductFileViewModel : BaseViewModel
    {
        public long FileId { get; set; }
        public long ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }
        public virtual FileViewModel File { get; set; }
        public int SortOrder { get; set; }
    }
}
