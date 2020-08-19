using System;
using System.Collections.Generic;
using Store.Data.EF.Entities;

namespace Store.Data.EF.Entities
{
   public class Gallery : BaseEntity
   {
        public string Name { get; set; }
        public virtual ICollection<GalleryImage> Images { get; set; }
    }
}
