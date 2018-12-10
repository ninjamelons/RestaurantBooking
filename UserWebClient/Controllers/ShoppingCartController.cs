using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UserWebClient.Models;

namespace UserWebClient.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly OrderService.IOrderService _orderProxy;

        public ShoppingCartController(OrderService.IOrderService orderProxy)
        {
            this._orderProxy = orderProxy;
        }

        public ActionResult Index()
        {
            return View("HomeCart");
        }

        public ActionResult Error()
        {
            return RedirectToAction("Index");
        }

        //Get Order (Cart)
        public async Task<ActionResult> HomeCart()
        {
            HomeCartViewModel model = new HomeCartViewModel
            {
                Order = new ModelLibrary.Order { ItemsList = new List<ModelLibrary.OrderLineItem>() },
                TotalPrice = 0
            };

            if (Session["orderId"] != null)
            {
                model.Order = await _orderProxy.GetOrderByIdAsync((int)Session["orderId"]);
                model.TotalPrice = model.Order.TotalPriceCent;
            }
            
            return View(model);
        }

       //Get: Delete Item from order by ID
       public async Task<ActionResult> Delete(int itemId)
        {
            await _orderProxy.DeleteItemByIdAsync((int)Session["orderId"], itemId);

            HomeCartViewModel model = new HomeCartViewModel
            {
                Order = new ModelLibrary.Order { ItemsList = new List<ModelLibrary.OrderLineItem>() },
                TotalPrice = 0
            };

            if (Session["orderId"] != null)
            {
                model.Order = _orderProxy.GetOrderById((int)Session["orderId"]);
                model.TotalPrice = model.Order.TotalPriceCent;
            }

            return View("HomeCart", model);
        }

        [HttpPost]
        public async Task<ActionResult> Charge(string stripeEmail, string stripeToken)
        {
            // Set your secret key: remember to change this to your live secret key in production
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.SetApiKey("sk_test_qNmHozWgCoVNFhqTVVytWScL");

            var order = await _orderProxy.GetOrderByIdAsync((int)Session["orderId"]);

            // Token is created using Checkout or Elements!
            // Get the payment token submitted by the form:
            // var token = model.Token; // Using ASP.NET MVC

            var options = new ChargeCreateOptions
            {
                Amount = order.TotalPriceCent,
                Currency = "dkk",
                Description = "Example charge",
                SourceId = stripeToken,
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

            string chargeResult = "";
            if (charge.Paid)
            {
                chargeResult = $"Payment succeeded.";
                order.Accepted = true;
                await _orderProxy.UpdateOrderAsync(order);
            }
            else
                chargeResult = charge.Outcome.Reason;

            return View("OrderConfirmation", new PaymentConfirmationViewModel { Result = chargeResult, OrderId = order.OrderId, Amount = charge.Amount});
        }
    }
}