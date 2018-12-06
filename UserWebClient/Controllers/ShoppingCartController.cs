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
        public double TotalPrice { get; private set; }

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
                order = _orderProxy.GetOrderById((int)Session["orderId"]);
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
                order = _orderProxy.GetOrderById((int)Session["orderId"]);
            }

            return View("HomeCart", order);
        }


        
    }
}