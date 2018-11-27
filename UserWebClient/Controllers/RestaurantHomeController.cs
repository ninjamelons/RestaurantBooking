using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLibrary;
using UserWebClient.Models;
using UserWebClient.RestaurantService;
//using UserWebClient.OrderService;

namespace UserWebClient.Controllers
{
    public class RestaurantHomeController : Controller
    {
        private readonly IRestaurantService _restaurantProxy;
        /*private readonly IOrderService _orderProxy;*/

        public RestaurantHomeController(IRestaurantService restaurantService/*, IOrderService orderService*/)
        {
            this._restaurantProxy = restaurantService;
            /*this._orderProxy = orderService;*/
        }

        [HttpGet]
        // GET: RestaurantHome
        public ActionResult Index(int id)
        {
            RestaurantOrderModel model = new RestaurantOrderModel();
            model.Restaurant = this._restaurantProxy.GetRestaurant(id);
            model.menu = model.Restaurant.Menu;
            model.OrderId = 50;
            return View("Index", model);
        }

        [HttpPost]
        // POST add item to cart
        public ActionResult IndexCart(int resId, int orderId, int itemId)
        {
            #region Add item to cart

            /*_orderProxy.AddItemToCart(itemId, orderId);*/
            
            #endregion

            #region Assign values
            var model = new RestaurantOrderModel();
            model.Restaurant = this._restaurantProxy.GetRestaurant(resId);
            model.menu = model.Restaurant.Menu;
            model.OrderId = orderId;
            #endregion

            return RedirectToAction("Index", model);
        }
    }
}