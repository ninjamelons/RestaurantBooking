using System;
using System.Collections.Generic;
using System.Linq;
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

            return View();
        }

        // GET: Restaurant/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            var rescats = new Dictionary<string, int>();
           // foreach (RestaurantCategory x in proxy.GetAllRestaurantCategories())
           // {
           //     rescats.Add(x.Name, x.Id);
           // }

            var model = new RestaurantViewModel { Restaurant = new Restaurant(), Categories = rescats };

            return View("Create", model);
        }

        // POST: Restaurant/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurant/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Restaurant/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Restaurant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
