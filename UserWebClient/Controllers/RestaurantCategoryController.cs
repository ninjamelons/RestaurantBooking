using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModelLibrary;

namespace UserWebClient.Controllers
{
    public class RestaurantCategoryController : Controller
    {
        RestaurantService.RestaurantServiceClient proxy = new RestaurantService.RestaurantServiceClient();

        // GET: RestaurantCategory
        public ActionResult Index()
        {
            return View(proxy.GetAllRestaurantCategories());
        }

        // GET: RestaurantCategory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RestaurantCategory/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: RestaurantCategory/Create
        [HttpPost]
        public ActionResult Create(RestaurantCategory cat)
        {
            try
            {
                proxy.CreateRestaurantCategory(cat);

                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: RestaurantCategory/Edit/5
        public ActionResult Edit(int id)
        {
            var cat = proxy.GetRestaurantCategory(id);
            return View("Edit", cat);
        }

        // POST: RestaurantCategory/Edit/5
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
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: RestaurantCategory/Delete/5
        public ActionResult Delete(int id)
        {
            var cat = proxy.GetRestaurantCategory(id);
            return View("Delete", cat);
        }

        // POST: RestaurantCategory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, RestaurantCategory cat)
        {
            try
            {
                proxy.DeleteRestaurantCategory(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}
