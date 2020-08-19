using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data.EF.Entities
{
   public class DeliveryMethod: BaseEntity
    {
        public string Name { get; set; }
        public string  Description { get; set; }
        public virtual ICollection<ProductDeliveryMethod> ProductDeliveryMethods { get; set; }
    }
}
