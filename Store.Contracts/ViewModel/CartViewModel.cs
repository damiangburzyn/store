using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts.ViewModel
{
  public   class CartViewModel :BaseViewModel
    {
        public string CartId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
       

        public virtual ProductViewModel Product { get; set; }
    }
}
