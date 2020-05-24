using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data.EF.Entities
{
public  class ProductDeliveryMethod : BaseEntity   {

        public long DeliveryId { get; set; }

        public long ProductId { get; set; }

        public int MaxCountInPackage { get; set; }

        public decimal Price { get; set; }

        public virtual Product Product { get; set; }
        public virtual DeliveryMethod Delivery { get; set; }
    }
}
