using ModelLibrary;
using ControllerLibrary;
using DatabaseAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserWebClient.Models;

namespace UserWebClient.Controllers
{
   
    public class HomeController : Controller
    {
        RestaurantService.RestaurantServiceClient proxy = new RestaurantService.RestaurantServiceClient();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Search)
        {

            /*var model = new RestaurantBrowseModel();

            model.SelectedZipCode = 0;
            if (zipcode.HasValue)
                model.SelectedZipCode = zipcode.Value;
            model.Restaurants = new List<ModelLibrary.Restaurant>();
            model.Page = 0;
            model.Amount = 100;

            model.Restaurants.AddRange(_proxy.GetRestaurantsPaged(model.SelectedZipCode, model.SelectedRestaurantCategoryId, model.Page, model.Amount, true, false));

            return View("~/Views/Restaurant/Browse.cshtml", model);*/
            if (Search != null && Search != "")
                return View("SearchResults", proxy.GetAllRestaurantsByZipCode(Int32.Parse(Search)));
            return View();

        }

        public ActionResult About()
        {

            return View();

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}