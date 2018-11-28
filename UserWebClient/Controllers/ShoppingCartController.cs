﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLibrary;
using DatabaseAccessLibrary;
using UserWebClient.Views;
using UserWebClient.Views.ViewModels;

/*
namespace UserWebClient.Controllers

{
    public class ShoppingCartController : Controller
    {
        readonly JustFeastDbDataContext orderDB = new JustFeastDbDataContext();
        private readonly object ShoppingCart;

        // Helper method to simplify shopping cart calls
        public static ShoppingCartController GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        private static ShoppingCartController GetCart(HttpContextBase httpContext)
        {
            throw new NotImplementedException();
        }

        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            // Retrieve the item from the database
            var addedItem = orderDB.Items
                .Single(item => item.id == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            int count = cart.AddToCart(addedItem);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(addedItem.Name) +
                
                    " has been added to your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = count,
                DeleteId = id
            };
            return Json(results);

            // Go back to the main store page for more shopping
            // return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the item to display confirmation

            // Get the name of the album to display confirmation
            string itemName = orderDB.Items
                .Single(item => item.id == id).Name;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = "One (1) " + Server.HtmlEncode(itemName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}*/