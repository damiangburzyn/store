using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Database;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Store.Contracts.ViewModel.Shopping;
using System.Text.Encodings.Web;

namespace Store.Serices
{
   public class ShoppingCartService
    {
        private ApplicationDbContext _storeDb;

        public ShoppingCartService(ApplicationDbContext storeDb)
        {
            _storeDb = storeDb;
        }

        public ShoppingCart GetCart(HttpContext context)
        {
          return  ShoppingCart.GetCart(context, _storeDb);
        }
        public ShoppingCart GetCart(ControllerBase controller)
        {
            return ShoppingCart.GetCart(controller, _storeDb);
        }

        public void AddToCard(long id, HttpContext context)
        {
            // Retrieve the album from the database

            var addedProduct = _storeDb.Products
                .Single(album => album.Id == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(context, _storeDb);

            cart.AddToCart(addedProduct);

            // Go back to the main store page for more shopping
        }

        public ShoppingCartRemoveViewModel RemoveFromCart(long id, HttpContext context)
        {

            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(context, _storeDb);

            // Get the name of the album to display confirmation
            string albumName = _storeDb.Carts
                .Single(item => item.Id == id).Product.Name;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = HtmlEncoder.Default.Encode(albumName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return results;
        }

        public int CartSummary(HttpContext httpContext)
        {
            var cart = ShoppingCart.GetCart(httpContext, _storeDb);
            var cardCount = cart.GetCount();
            return cardCount;
        }
    }
}
