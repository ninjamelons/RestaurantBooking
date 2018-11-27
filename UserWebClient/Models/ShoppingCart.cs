using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLibrary;
using DatabaseAccessLibrary;
using UserWebClient.Models;
/*
namespace UserWebClient.Models
{
    public class ShoppingCart
    {
        JustFeastDbDataContext orderDB = new JustFeastDbDataContext();
        private readonly Cart cartItem;

        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        private string GetCartId(HttpContextBase context)
        {
            throw new NotImplementedException();
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public int AddToCart(DatabaseAccessLibrary.Item item)
      
            {

            var cartItem = orderDB.Orders.SingleOrDefault(
               c => c.CartId == ShoppingCartId 
               && c.OrderLineItems == item.id);

            // Create a new cart item if no cart item exists
            cartItem = new Cart
                {
                    ItemId = item.id,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                orderDB.Orders.A(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            orderDB.SaveChanges();

            return cartItem.Count;
        }
        public int RemoveFromCart(int id)
        {


            // Get the cart

            var cartItem = orderDB.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.ItemId == id);


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
                    orderDB.Carts.Remove(cartItem);
                }
                // Save changes
                orderDB.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = orderDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                orderDB.Carts.Remove(cartItem);
            }
            // Save changes
            orderDB.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return orderDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in orderDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply item price by count of that item to get 
            // the current price for each of those items in the cart
            // sum all item price totals to get the cart total
            decimal? total = (from cartItems in orderDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Item.Price).Sum();

            return total ?? decimal.Zero;
        }


    }
}