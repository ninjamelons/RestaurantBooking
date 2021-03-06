﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ModelLibrary;

namespace UserWebClient.Controllers
{
    public class RestaurantCategoryController : AdminController
    {
        private readonly RestaurantService.IRestaurantService _proxy;

        public RestaurantCategoryController(RestaurantService.IRestaurantService proxy)
        {
            this._proxy = proxy;
        }

        // GET: RestaurantCategory
        public async Task<ActionResult> Index()
        {
            return View(await _proxy.GetAllRestaurantCategoriesAsync());
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
        public async Task<ActionResult> Create(RestaurantCategory cat)
        {
            try
            {
                await _proxy.CreateRestaurantCategoryAsync(cat);

                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: RestaurantCategory/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var cat = await _proxy.GetRestaurantCategoryAsync(id);
            return View("Edit", cat);
        }

        // POST: RestaurantCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: RestaurantCategory/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var cat = await _proxy.GetRestaurantCategoryAsync(id);
            return View("Delete", cat);
        }

        // POST: RestaurantCategory/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, RestaurantCategory cat)
        {
            try
            {
                await _proxy.DeleteRestaurantCategoryAsync(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}
