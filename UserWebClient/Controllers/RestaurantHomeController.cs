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
        //private readonly IOrderService _orderProxy;

        public RestaurantHomeController(IRestaurantService restaurantService)
        {
            this._restaurantProxy = restaurantService;
        }

        [HttpGet]
        // GET: RestaurantHome
        public ActionResult Index(int id)
        {
            RestaurantOrderModel model = new RestaurantOrderModel();
            model.Restaurant = this._restaurantProxy.GetRestaurant(id);
            model.menu = model.Restaurant.Menu;
            return View("Index", model);
        }

        [HttpPost]
        // POST add item to cart
        public ActionResult AddToCart(int resId, int orderId, int itemId)
        {
            #region Add item to cart

            

            #endregion

            var model = new RestaurantOrderModel();
            model.Restaurant = this._restaurantProxy.GetRestaurant(resId);
            model.menu = model.Restaurant.Menu;
            model.OrderId = orderId;
            return View("Index", model);
        }
    }
}