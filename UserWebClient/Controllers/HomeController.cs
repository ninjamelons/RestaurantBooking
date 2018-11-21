
using DatabaseAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserWebClient.RestaurantService;

namespace UserWebClient.Controllers
{

    public class HomeController : Controller
    {
        RestaurantService.RestaurantServiceClient proxy = new RestaurantService.RestaurantServiceClient();

        public ActionResult Index(string SearchString)
        {
            ViewBag.Title = "Home Page";

           /* IEnumerable<ModelLibrary.Restaurant> serviceResult = this._proxy.GetAllRestaurants();

            var restaurants = serviceResult.Select<restaurantRes => new Restaurant
            {

            }
            */

            return View(proxy.GetAllRestaurants());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}