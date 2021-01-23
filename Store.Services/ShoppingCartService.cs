using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Database;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Store.Contracts.ViewModel.Shopping;
using System.Text.Encodings.Web;
using Store.Data.EF.Repositories;
using Store.Data.EF.Entities;

namespace Store.Services
{
   public class ShoppingCartService
    {
        private ApplicationDbContext _storeDb;
        public const string CartSessionKey = "CartId";
        public readonly ShoppingCart cart;
        public ShoppingCartService(ApplicationDbContext storeDb)
        {
            _storeDb = storeDb;
            cart = new ShoppingCart(storeDb);

        }

        public List<Cart> GetCartItems(HttpContext context)
        {
            var cartId = GetCartId(context);
            return cart.GetCartItems(cartId);
        }

        public decimal GetTotal(HttpContext context)
        {
            var cartId = GetCartId(context);
            return cart.GetTotal(cartId);
        }



        public void AddToCard(long id, HttpContext context, int count)
        {
            // Retrieve the product from the database

            var addedProduct = _storeDb.Products
                .Single(product => product.Id == id);

            // Add it to the shopping cart
            var cartId = GetCartId(context);
            cart.AddToCart(addedProduct, count, cartId);

            // Go back to the main store page for more shopping
        }

        public ShoppingCartRemoveViewModel RemoveFromCart(long id, HttpContext context)
        {

            // Remove the item from the cart
            var cartId = GetCartId(context);

            // Get the name of the product to display confirmation
            string productName = _storeDb.Carts
                .Single(item => item.Id == id).Product.Name;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id, cartId);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = HtmlEncoder.Default.Encode(productName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(cartId),
                CartCount = cart.GetCount(cartId),
                ItemCount = itemCount,
                DeleteId = id
            };

            return results;
        }

        public int CartSummary(HttpContext context)
        {
            var cartId = GetCartId(context);
            var cardCount = cart.GetCount(cartId);
            return cardCount;
        }



        public string GetCartId(HttpContext context)
        {
            if (context.Session.Get<string>(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session.Set(CartSessionKey,
                        context.User.Identity.Name);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session.Set(CartSessionKey, tempCartId.ToString());
                }
            }
            return context.Session.Get<string>(CartSessionKey);
        }


    }
}
