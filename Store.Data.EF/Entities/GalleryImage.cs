using System;
using System.Collections.Generic;
using System.Text;


namespace Store.Data.EF.Entities
{ 
   public class GalleryImage : BaseEntity
   {
        public string Name { get; set; }

        public int SortOrder { get; set; }

        public bool IsSpecial { get; set; }
   }
}
