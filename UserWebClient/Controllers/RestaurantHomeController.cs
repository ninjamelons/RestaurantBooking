using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserWebClient.RestaurantService;
using UserWebClient.OrderService;

namespace UserWebClient.Controllers
{
    public class RestaurantHomeController : Controller
    {
        private readonly IRestaurantService _restaurantProxy;
        private readonly IOrderService _orderProxy;

        public RestaurantHomeController(IRestaurantService restaurantService)
        {
            this._restaurantProxy = restaurantService;
        }

        [HttpGet]
        // GET: RestaurantHome
        public ActionResult Index(int id)
        {
            var serviceResult = this._restaurantProxy.GetRestaurant(id);

            return View("Index", serviceResult);
        }

        // POST add item to cart
        public ActionResult AddToCart(int id)
        {
            this._orderProxy.AddItemToOrder(id);
            return View("Index");
        }
    }
}