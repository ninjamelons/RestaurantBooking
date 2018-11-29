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
        //RestaurantService.RestaurantServiceClient proxy = new RestaurantService.RestaurantServiceClient();

        private readonly RestaurantService.IRestaurantService _proxy;
        private readonly OrderService.IOrderService _orderProxy;
        
        public RestaurantController(RestaurantService.IRestaurantService proxy,
            OrderService.IOrderService orderProxy)
        {
            this._proxy = proxy;
            this._orderProxy = orderProxy;
        }

        // GET: Restaurant
        public ActionResult Index()
        {
            return View(_proxy.GetAllRestaurants());
        }

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            //  var rescats = new Dictionary<string, RestaurantCategory>();
            //  rescats.Add("-No Category-", null);
            //  foreach (RestaurantCategory x in proxy.GetAllRestaurantCategories())
            //  {
            //      rescats.Add(x.Name, x);
            //  }
            //
            //  var model = new RestaurantViewModel { Restaurant = new Restaurant(), Categories = rescats };

            var categories = new List<RestaurantCategory>();
            categories.Add(new RestaurantCategory { Id = 0, Name = "-No Category-" });
            categories.AddRange(_proxy.GetAllRestaurantCategories());

            var model = new RestaurantViewModel { Restaurant = new Restaurant(), CategoryList = new SelectList(categories, "Id", "Name", 1) };

            return View("Create", model);
        }

        // POST: Restaurant/Create
        [HttpPost]
        public ActionResult Create(RestaurantViewModel model)
        {
            try
            {
                var res = model.Restaurant;
                res.Category = _proxy.GetRestaurantCategory(model.SelectedCategoryId);
                _proxy.CreateRestaurant(res);

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
            //  var rescats = new Dictionary<string, RestaurantCategory>();
            //  rescats.Add("-No Category-", null);
            //  foreach (RestaurantCategory x in proxy.GetAllRestaurantCategories())
            //  {
            //      rescats.Add(x.Name, x);
            //  }
            //
            //  RestaurantViewModel model = new RestaurantViewModel { Restaurant = proxy.GetRestaurant(id), Categories = rescats };

            var categories = new List<RestaurantCategory>();
            categories.Add(new RestaurantCategory { Id = 0, Name = "-No Category-" });
            categories.AddRange(_proxy.GetAllRestaurantCategories());

            var model = new RestaurantViewModel { Restaurant = _proxy.GetRestaurant(id), CategoryList = new SelectList(categories, "Id", "Name", 1) };

            return View("Edit", model);
        }

        // POST: Restaurant/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RestaurantViewModel model)
        {
            try
            {
                var res = model.Restaurant;
                res.Id = id;
                res.Category = _proxy.GetRestaurantCategory(model.SelectedCategoryId);
                _proxy.UpdateRestaurant(res);

                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: Restaurant/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var model = _proxy.GetRestaurant(id);
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
                _proxy.DeleteRestaurant(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: Restaurant/Browse
        public ActionResult Browse(int? categoryId, int? zipcode)
        {
            var model = new RestaurantBrowseModel();

            var categories = new List<RestaurantCategory>();
            categories.Add(new RestaurantCategory { Id = 0, Name = "-No Category-" });
            categories.AddRange(_proxy.GetAllRestaurantCategories());

            model.CategoryList = new SelectList(categories, "Id", "Name", 0);
            model.SelectedRestaurantCategoryId = 0;
            model.SelectedZipCode = 0;
            if (zipcode.HasValue)
                model.SelectedZipCode = zipcode.Value;
            if (categoryId.HasValue)
                model.SelectedRestaurantCategoryId = categoryId.Value;
            model.Restaurants = new List<Restaurant>();
            model.Page = 0;
            model.Amount = 100;

            model.Restaurants.AddRange(_proxy.GetRestaurantsPaged(model.SelectedZipCode, model.SelectedRestaurantCategoryId, model.Page, model.Amount, true, false));

            return View(model);
        }

           [HttpGet]
        // GET: RestaurantHome
        public ActionResult Home(int id)
        {
            RestaurantOrderModel model = new RestaurantOrderModel();
            model.Restaurant = this._proxy.GetRestaurantWithMenu(id);
            return View("Home", model);
        }

        [HttpGet]
        // GET Home menu with RestaurantOrderModel parameter
        public ActionResult HomeModel(RestaurantOrderModel model)
        {
            return RedirectToAction("Home", model);
        }

        [HttpGet]
        // GET add item to cart
        public ActionResult HomeCart(int resId, int orderId, int itemId)
        {
            #region Add item to cart

            _orderProxy.AddItemToOrder(orderId, itemId);

            #endregion

            #region Assign values
            var model = new RestaurantOrderModel();
            model.Restaurant = this._proxy.GetRestaurantWithMenu(resId);
            model.OrderId = orderId;
            #endregion

            return RedirectToAction("HomeModel", model);
        }

        [HttpGet]
        public ActionResult ReserveTable()
        {
            return View("ReserveTable");
        }

        [HttpPost]
        // POST Reserve a table and add orderId to SessionId
        public ActionResult ReserveTable(RestaurantOrderModel order)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("ReserveTable", order);

                //Session["orderId"] = this._proxy.ReserveTables();
                //restaurantId, tableId, datetime


                return RedirectToAction("HomeModel", order);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}
