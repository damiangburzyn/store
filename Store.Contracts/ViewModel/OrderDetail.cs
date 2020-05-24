using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts.ViewModel
{
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual ProductViewModel Product { get; set; }
        public virtual OrderViewModel Order { get; set; }
    }
}
