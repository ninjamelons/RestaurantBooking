using ModelLibrary;
using ControllerLibrary;
using DatabaseAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserWebClient.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly RestaurantService.IRestaurantService _proxy;
        public HomeController(RestaurantService.IRestaurantService proxy)
        {
            this._proxy = proxy;
        }

        public ActionResult Index(string Search)
        {
            //IEnumerable<ModelLibrary.Restaurant> serviceResult = _proxy.GetAllRestaurants();
            /*var restaurants = serviceResult.Select(restaurantRes => new ModelLibrary.Restaurant
            {
                Name = restaurantRes.Name,
                Address = restaurantRes.Address,
                ZipCode = restaurantRes.ZipCode,
                PhoneNo = restaurantRes.PhoneNo
            });

            if (Search != null && Search != "")
                restaurants = restaurants.Where(x => x.ZipCode.Contains(Search));*/

            if (Search != null && Search != "")
              return View(_proxy.GetAllRestaurantsByZipCode(Int32.Parse(Search)));

            return View(_proxy.GetAllRestaurants());
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