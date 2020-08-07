using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts.ViewModel
{
    public class ProductDeliveryMethodViewModel :BaseViewModel
    {
        public long DeliveryId { get; set; }

        public long ProductId { get; set; }

        public int MaxCountInPackage { get; set; }

        public decimal Price { get; set; }

        public virtual ProductViewModel Product { get; set; }
        public virtual DeliveryMethodViewModel Delivery { get; set; }
    }
}
