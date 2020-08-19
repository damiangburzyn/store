using System;
using System.Collections.Generic;

namespace Store.Contracts.ViewModel
{
   public class GalleryViewModel : BaseViewModel
   {
        public string Name { get; set; }
        public virtual ICollection<GalleryImageViewModel> Images { get; set; }
    }
}
