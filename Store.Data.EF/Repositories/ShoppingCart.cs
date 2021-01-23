using Store.Data.Database;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.EF.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.EF.Repositories
{
    public class ShoppingCart
    {
        public ShoppingCart(ApplicationDbContext storeDB)
        {
            this.storeDB = storeDB;
        }

        ApplicationDbContext storeDB { get; set; }

        public void AddToCart(Product product, int count, string shoppingCartId)
        {
            // Get the matching cart and product instances
            var cartItem = storeDB.Carts.SingleOrDefault(
                c => c.CartId == shoppingCartId
                && c.ProductId == product.Id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ProductId = product.Id,
                    CartId = shoppingCartId,
                    Count = count,
                    CreateDate = DateTime.Now
                };
                storeDB.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                for (int i = 0; i < count; i++)
                {
                    cartItem.Count++;
                }
            }
            // Save changes
            storeDB.SaveChanges();
        }
        public int RemoveFromCart(long id, string shoppingCartId)
        {
            // Get the cart
            var cartItem = storeDB.Carts.Single(
                cart => cart.CartId == shoppingCartId
                && cart.Id == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);
                }
                // Save changes
                storeDB.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart(string shoppingCartId)
        {
            var cartItems = storeDB.Carts.Where(
                cart => cart.CartId == shoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }
            // Save changes
            storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems(string shoppingCartId)
        {
            return storeDB.Carts.Include(a =>a.Product).Where(
                cart => cart.CartId == shoppingCartId).ToList();
        }
        public int GetCount(string shoppingCartId)
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in storeDB.Carts
                          where cartItems.CartId == shoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public decimal GetTotal(string shoppingCartId)
        {
            // Multiply product price by count of that product to get 
            // the current price for each of those products in the cart
            // sum all product price totals to get the cart total
            decimal? total = (from cartItems in storeDB.Carts
                              where cartItems.CartId == shoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Product.CurrentPrice).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateOrder(Order order, string shoppingCartId)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems(shoppingCartId);
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Product.CurrentPrice,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Product.CurrentPrice);

                storeDB.OrderDetails.Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;
            // Save the order
            storeDB.SaveChanges();
            // Empty the shopping cart
            EmptyCart(shoppingCartId);
            // Return the OrderId as the confirmation number
            return order.OrderId;
        }
        // We're using HttpContextBase to allow access to cookies.

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName, string shoppingCartId)
        {
            var shoppingCart = storeDB.Carts.Where(
                c => c.CartId == shoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            storeDB.SaveChanges();
        }
    }
}
