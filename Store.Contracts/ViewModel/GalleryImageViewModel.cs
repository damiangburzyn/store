using System;
using System.Collections.Generic;
using System.Text;


namespace Store.Contracts.ViewModel
{ 
   public class GalleryImageViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public bool IsSpecial { get; set; }
   }
}
