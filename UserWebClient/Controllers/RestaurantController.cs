using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModelLibrary;
using UserWebClient.Models;

namespace UserWebClient.Controllers
{
    public class RestaurantController : Controller
    {
        RestaurantService.RestaurantServiceClient proxy = new RestaurantService.RestaurantServiceClient();

        // private readonly RestaurantService.IRestaurantService _proxy;
        //
        // public RestaurantController(RestaurantService.IRestaurantService proxy)
        // {
        //     this._proxy = proxy;
        // }

        // GET: Restaurant
        public ActionResult Index()
        {
            return View(proxy.GetAllRestaurants());
        }

        // GET: Restaurant/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            var rescats = new Dictionary<string, RestaurantCategory>();
            rescats.Add("-No Category-", null);
            foreach (RestaurantCategory x in proxy.GetAllRestaurantCategories())
            {
                rescats.Add(x.Name, x);
            }

            var model = new RestaurantViewModel { Restaurant = new Restaurant(), Categories = rescats };

            return View("Create", model);
        }

        // POST: Restaurant/Create
        [HttpPost]
        public ActionResult Create(RestaurantViewModel model)
        {
            try
            {
                proxy.CreateRestaurant(model.Restaurant);
          
                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: Restaurant/Edit/5
        public ActionResult Edit(int id)
        {
            var rescats = new Dictionary<string, RestaurantCategory>();
            rescats.Add("-No Category-", null);
            foreach (RestaurantCategory x in proxy.GetAllRestaurantCategories())
            {
                rescats.Add(x.Name, x);
            }

            RestaurantViewModel model = new RestaurantViewModel { Restaurant = proxy.GetRestaurant(id), Categories = rescats };

            return View("Edit", model);
        }

        // POST: Restaurant/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RestaurantViewModel model)
        {
           // try
           // {
           //     proxy.UpdateRestaurant(model.Restaurant);
           //
           //     return RedirectToAction("Index");
           // }
           // catch
           // {
           //     return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
           // }

            proxy.UpdateRestaurant(model.Restaurant);

            return RedirectToAction("Index");
        }

        // GET: Restaurant/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var model = proxy.GetRestaurant(id);
                return View(model);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // POST: Restaurant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                proxy.DeleteRestaurant(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}
