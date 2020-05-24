using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts.ViewModel
{
    public class ShoppingCartViewModel
    {
        public List<CartViewModel> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}
