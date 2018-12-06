using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserWebClient.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly OrderService.IOrderService _orderProxy;

        public ShoppingCartController(OrderService.IOrderService orderProxy)
        {
            this._orderProxy = orderProxy;
        }

        //Get Order (Cart)
        public ActionResult HomeCart()
        {
            ModelLibrary.Order order = null;

            if (Session["orderId"] != null)
            {
               // order = _orderProxy.GetOrderById((int)Session["orderId"]);
            }

            return View("HomeCart", order);
        }

       //Get: Delete Item from order by ID
       public ActionResult Delete(int itemId )
        {
           _orderProxy.DeleteItemById((int)Session["orderId"],itemId);

            ModelLibrary.Order order = null;
            if (Session["orderId"] != null)
            {
                //order = _orderProxy.GetOrderById((int)Session["orderId"]);
            }

            return View("HomeCart", order);
        }

        [HttpPost]
        public ActionResult Charge(string stripeEmail, string stripeToken)
        {
            // Set your secret key: remember to change this to your live secret key in production
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.SetApiKey("sk_test_qNmHozWgCoVNFhqTVVytWScL");

            // Token is created using Checkout or Elements!
            // Get the payment token submitted by the form:
            //var token = model.Token; // Using ASP.NET MVC

            var options = new ChargeCreateOptions
            {
                Amount = 999,
                Currency = "usd",
                Description = "Example charge",
                SourceId = stripeToken,

            };
            var service = new ChargeService();
            Charge charge = service.Create(options);
            
            
            return View();
        }
    }
}