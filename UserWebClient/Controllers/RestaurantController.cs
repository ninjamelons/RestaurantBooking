using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModelLibrary;
using UserWebClient.Models;
using System.Threading.Tasks;

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
        public async Task<ActionResult> Index()
        {
            if (Session["roleId"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["roleId"].ToString() != "1")
                return RedirectToAction("Index", "Home");

            var restaurants = await _proxy.GetAllRestaurantsAsync();

            return View(restaurants);
        }

        // GET: Restaurant/Create
        public async Task<ActionResult> Create()
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
            categories.AddRange(await _proxy.GetAllRestaurantCategoriesAsync());

            var model = new RestaurantViewModel { Restaurant = new Restaurant(), CategoryList = new SelectList(categories, "Id", "Name", 1) };

            return View("Create", model);
        }

        // POST: Restaurant/Create
        [HttpPost]
        public async Task<ActionResult> Create(RestaurantViewModel model)
        {
            try
            {
                var res = model.Restaurant;
                model.Restaurant.Id = (int)Session["customerId"];
                res.Category = await _proxy.GetRestaurantCategoryAsync(model.SelectedCategoryId);
                await _proxy.CreateRestaurantAsync(res);

                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: Restaurant/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (Session["roleId"] == null)
                return RedirectToAction("Index", "Home");
            if (Session["roleId"].ToString() != "1")
                return RedirectToAction("Index", "Home");

            var categories = new List<RestaurantCategory>();
            categories.Add(new RestaurantCategory { Id = 0, Name = "-No Category-" });
            categories.AddRange(await _proxy.GetAllRestaurantCategoriesAsync());

            var model = new RestaurantViewModel { Restaurant = await _proxy.GetRestaurantAsync(id), CategoryList = new SelectList(categories, "Id", "Name", 1) };

            return View("Edit", model);
        }

        // POST: Restaurant/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, RestaurantViewModel model)
        {
            try
            {
                if (Session["roleId"] == null)
                    return RedirectToAction("Index", "Home");
                if (Session["roleId"].ToString() != "1")
                    return RedirectToAction("Index", "Home");

                var res = model.Restaurant;
                res.Id = id;
                res.Category = await _proxy.GetRestaurantCategoryAsync(model.SelectedCategoryId);
                await _proxy.UpdateRestaurantAsync(res);

                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: Restaurant/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (Session["roleId"] == null)
                    return RedirectToAction("Index", "Home");
                if (Session["roleId"].ToString() != "1")
                    return RedirectToAction("Index", "Home");

                var model = await _proxy.GetRestaurantAsync(id);
                return View(model);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // POST: Restaurant/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                if (Session["roleId"] == null)
                    return RedirectToAction("Index", "Home");
                if (Session["roleId"].ToString() != "1")
                    return RedirectToAction("Index", "Home");

                await _proxy.DeleteRestaurantAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: Restaurant/Browse
        public async Task<ActionResult> Browse(int? categoryId, int? zipcode)
        {
            var model = new RestaurantBrowseModel();

            var categories = new List<RestaurantCategory>();
            categories.Add(new RestaurantCategory { Id = 0, Name = "-No Category-" });
            categories.AddRange(await _proxy.GetAllRestaurantCategoriesAsync());

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
        public async Task<ActionResult> Home(int id)
        {
            RestaurantOrderModel model = new RestaurantOrderModel();
            Session["Restaurant"] = await _proxy.GetRestaurantWithMenuAsync(id);
            return View("Home", model);
        }

        [HttpGet]
        // GET Home menu with RestaurantOrderModel parameter
        public ActionResult HomeModel(RestaurantOrderModel model)
        {
            return View("Home", model);
        }

        [HttpGet]
        // GET add item to cart
        public async Task<ActionResult> HomeCart(int resId, int orderId, int itemId)
        {
            _orderProxy.AddItemToOrder(orderId, itemId, resId);
            
            var model = new RestaurantOrderModel();
            Session["Restaurant"] = await _proxy.GetRestaurantWithMenuAsync(resId);

            return RedirectToAction("HomeModel", model);
        }

        [HttpGet]
        public ActionResult ReserveTable()
        {
            return View("ReserveTable");
        }

        [HttpPost]
        // POST Reserve a table and add orderId to SessionId
        public async Task<ActionResult> ReserveTable(RestaurantOrderModel order)
        {
            try
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                if(errors.Count() > 1)
                    return View("ReserveTable", order);

                var res = (Restaurant)Session["Restaurant"];

                Session["orderId"] = await _proxy.ReserveTablesAsync(res.Id, order.NoSeats,
                    order.ReserveDateTime);
                
                return RedirectToAction("HomeModel", order);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Owner()
        {
            if (Session["customerId"] == null)
                return RedirectToAction("Index", "Home");

            int id = (int) Session["customerId"];
            var model = await _proxy.GetRestaurantAsync(id);
            if(model == null)
                return RedirectToAction("Index", "Home");

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Owner(Restaurant res)
        {
            try
            {
                await _proxy.UpdateRestaurantAsync(res);
                return RedirectToAction("Home", new { id = res.Id });
            } catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}