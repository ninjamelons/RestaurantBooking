using ControllerLibrary;
using DatabaseAccessLibrary;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantService
{
    class AdminService : IAdminService
    {
        #region Item
        ItemDb itemDb = new ItemDb();
        ItemCatDb itemCatDb = new ItemCatDb();
        ItemCtrl itemCtrl = new ItemCtrl();
        public void CreateItem(ModelLibrary.Item item, int menuId)
        {
            itemCtrl.CreateItem(item, menuId);

        }

        public void CreateItemCat(ModelLibrary.ItemCat itemCat)
        {
            itemCtrl.CreateItemCat(itemCat);
        }

        public void DeleteItem(ModelLibrary.Item item, int menuId)
        {
            var dbItem = itemCtrl.ConvertItemToDb(item, menuId);
            itemDb.DeleteItem(dbItem);
        }

        public void DeleteItemCat(ModelLibrary.ItemCat itemCat)
        {

            var dbItemCat = itemCtrl.ConvertItemCatToDb(itemCat);
            itemCatDb.DeleteItemCat(dbItemCat);
        }

        public IEnumerable<ModelLibrary.ItemCat> GetAllItemCategories()
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var itemCat = db.ItemCats.ToList();
            List<ModelLibrary.ItemCat> modelItemCats = new List<ModelLibrary.ItemCat>();
            foreach (var a in itemCat)
            {
                modelItemCats.Add(itemCtrl.ConvertItemCatToModel(a));
            }
            return modelItemCats;
        }

        public IEnumerable<ModelLibrary.Item> GetAllItemsByMenu(int menuId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var res = db.Items.Where(x => x.menuId == menuId).ToList();
            List<ModelLibrary.Item> modelItem = new List<ModelLibrary.Item>();
            foreach (var x in res)
            {
                modelItem.Add(itemCtrl.ConvertItemToModel(x));
            }
            return modelItem;

        }

        public IEnumerable<ModelLibrary.Item> GetAllItemsByCategory(int categoryId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var res = db.Items.Where(x => x.itemCatId == categoryId).ToList();
            List<ModelLibrary.Item> modelItem = new List<ModelLibrary.Item>();
            foreach (var x in res)
            {
                modelItem.Add(itemCtrl.ConvertItemToModel(x));
            }
            return modelItem;
        }

        public void UpdateItem(ModelLibrary.Item beforeItem, ModelLibrary.Item afterItem, int menuId)
        {
            itemCtrl.UpdateItem(beforeItem, afterItem, menuId);
        }

        public void UpdateItemCat(ModelLibrary.ItemCat beforeItemCat, ModelLibrary.ItemCat afterItemCat)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            ItemCtrl itemCtrl = new ItemCtrl();
            itemCtrl.UpdateItemCat(beforeItemCat, afterItemCat);
        }
        #endregion

        #region Menu
        MenuCtrl menuCtrl = new MenuCtrl();
        public void CreateMenu(ModelLibrary.Menu menu, int restaurantId)
        {
            var menuCtrl = new MenuCtrl();
            menuCtrl.CreateMenu(menu, restaurantId);
        }
        public void DeleteMenu(ModelLibrary.Menu menu, int restaurantId)
        {
            var menuCtrl = new MenuCtrl();
            var menuDb = new MenuDb();
            var dbMenu = menuCtrl.ConvertMenuToDb(menu, restaurantId);
            menuDb.DeleteMenu(dbMenu);
        }

        public IEnumerable<ModelLibrary.Menu> GetAllMenusByRestaurant(int restaurantId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            MenuCtrl menuCtrl = new MenuCtrl();
            var res = db.Menus.Where(x => x.restaurantId == restaurantId).ToList();
            List<ModelLibrary.Menu> modelMenu = new List<ModelLibrary.Menu>();
            foreach (var x in res)
            {
                modelMenu.Add(menuCtrl.ConvertMenuToModel(x));
            }
            return modelMenu;

        }

        public void UpdateMenu(ModelLibrary.Menu beforeMenu, ModelLibrary.Menu afterMenu, int restaurantId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            MenuCtrl menuCtrl = new MenuCtrl();
            menuCtrl.UpdateMenu(beforeMenu, afterMenu, restaurantId);
        }
        #endregion

        #region Order
        public void AddItemToOrder(int orderId, int itemId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var item = db.Items.SingleOrDefault(i => i.id == itemId);
            var exists =
                from lineItem in db.OrderLineItems
                where lineItem.orderId == orderId && lineItem.itemId == itemId
                select lineItem;

            if (exists.Any() && exists.FirstOrDefault() != null)
            {
                exists.FirstOrDefault().quantity++;
            }
            else
            {
                var oli = new DatabaseAccessLibrary.OrderLineItem
                {
                    orderId = orderId,
                    itemId = item.id,
                    quantity = 1
                };
                db.OrderLineItems.InsertOnSubmit(oli);
            }
            db.SubmitChanges();
        }

        public void CreateOrder(ModelLibrary.Order order)
        {
            OrderCtrl ordC = new OrderCtrl();
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            db.Orders.InsertOnSubmit(ordC.ConvertOrder(order));
            db.SubmitChanges();
        }

        public ModelLibrary.Order GetOrderById(int id)
        {
            OrderCtrl ordC = new OrderCtrl();
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            return ordC.ConvertOrderToModel(db.Orders.SingleOrDefault(o => o.id == id));
        }

        public void UpdateOrder(ModelLibrary.Order order)
        {
            var ordC = new OrderCtrl();
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var ord = db.Orders.SingleOrDefault(o => o.id == Convert.ToInt32(order.OrderId));
            ord = ordC.ConvertOrder(order);
            db.SubmitChanges();
        }
        #endregion

        #region Price
        PriceCtrl priceCtrl = new PriceCtrl();
        PriceDb priceDb = new PriceDb();
        public void CreatePrice(ModelLibrary.Price price)
        {
            priceCtrl.CreatePrice(price);
        }

        public void DeletePrice(ModelLibrary.Price price)
        {
            var dbPrice = priceCtrl.ConvertPriceToDb(price);
            priceDb.DeletePrice(dbPrice);
        }

        public IEnumerable<ModelLibrary.Price> GetAllPricesByItem(string id)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var price = db.Prices.ToList();
            List<ModelLibrary.Price> modelPrices = new List<ModelLibrary.Price>();
            foreach (var a in price)
            {
                modelPrices.Add(priceCtrl.ConvertPriceToModel(a));
            }
            return modelPrices;
        }

        public void UpdatePrice(ModelLibrary.Price beforePrice, ModelLibrary.Price afterPrice)
        {

            priceCtrl.UpdatePrice(beforePrice, afterPrice);
        }
        #endregion

        #region Restaurant
        public void CreateTable(Table table)
        {
            TableCtrl tableCtrl = new TableCtrl();
            tableCtrl.CreateTable(table);
        }
        public IEnumerable<Table> GetAllTables(int restaurantId)
        {
            TableCtrl tableCtrl = new TableCtrl();
            return tableCtrl.GetRestaurantTables(restaurantId);
        }
        public Table GetTable(Table table)
        {
            TableCtrl tableCtrl = new TableCtrl();
            return tableCtrl.GetTable(table);
        }
        public void UpdateTable(Table oldTable, Table newTable)
        {
            TableCtrl tableCtrl = new TableCtrl();
            tableCtrl.UpdateTable(oldTable, newTable);
        }
        public void DeleteTable(Table table)
        {
            TableCtrl tableCtrl = new TableCtrl();
            tableCtrl.DeleteTable(table);
        }


        public IEnumerable<ModelLibrary.Restaurant> GetAllRestaurants()
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var res = db.Restaurants.ToList();
            List<ModelLibrary.Restaurant> mRes = new List<ModelLibrary.Restaurant>();
            foreach (var x in res)
            {
                mRes.Add(RestaurantCtrl.ConvertRestaurantToModel(x));
            }
            return mRes;
        }
        public IEnumerable<ModelLibrary.Restaurant> GetAllRestaurantsByCategory(int categoryId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var res = db.Restaurants.Where(x => x.resCatId == categoryId).ToList();
            List<ModelLibrary.Restaurant> mRes = new List<ModelLibrary.Restaurant>();
            foreach (var x in res)
            {
                mRes.Add(RestaurantCtrl.ConvertRestaurantToModel(x));
            }
            return mRes;
        }
        public IEnumerable<ModelLibrary.Restaurant> GetAllRestaurantsByZipCode(int zipcode)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var res = db.Restaurants.Where(x => x.zipcode == zipcode).ToList();
            List<ModelLibrary.Restaurant> mRes = new List<ModelLibrary.Restaurant>();
            foreach (var x in res)
            {
                mRes.Add(RestaurantCtrl.ConvertRestaurantToModel(x));
            }
            return mRes;
        }
        public void CreateRestaurant(ModelLibrary.Restaurant restaurant)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var res = RestaurantCtrl.ConvertRestaurantToDatabase(restaurant);
            if (restaurant.Category != null)
                res.ResCat = db.ResCats.FirstOrDefault(x => x.id == restaurant.Category.Id);
            db.Restaurants.InsertOnSubmit(res);
            db.SubmitChanges();
        }
        public void UpdateRestaurant(ModelLibrary.Restaurant restaurant)
        {
            var db = new JustFeastDbDataContext();
            var res = db.Restaurants.FirstOrDefault(x => x.id == restaurant.Id);

            var mRes = RestaurantCtrl.ConvertRestaurantToDatabase(restaurant);
            res.name = mRes.name;
            res.phoneNo = mRes.phoneNo;
            res.verified = mRes.verified;
            res.zipcode = mRes.zipcode;
            res.address = mRes.address;
            res.discontinued = mRes.discontinued;
            res.email = mRes.email;
            res.resCatId = mRes.resCatId;

            db.SubmitChanges();
        }
        public void DeleteRestaurant(ModelLibrary.Restaurant restaurant)
        {
            var db = new JustFeastDbDataContext();
            db.Restaurants.DeleteOnSubmit(RestaurantCtrl.ConvertRestaurantToDatabase(restaurant));
            db.SubmitChanges();
        }
        public IEnumerable<ModelLibrary.RestaurantCategory> GetAllRestaurantCategories()
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var cats = db.ResCats.ToList();
            List<ModelLibrary.RestaurantCategory> mRes = new List<ModelLibrary.RestaurantCategory>();
            foreach (var x in cats)
            {
                mRes.Add(RestaurantCtrl.ConvertRestaurantCategoryToModel(x));
            }
            return mRes;
        }
        public void DeleteRestaurant(int id)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            db.Restaurants.DeleteOnSubmit(db.Restaurants.FirstOrDefault(x => x.id == id));
            db.SubmitChanges();
        }
        public ModelLibrary.Restaurant GetRestaurant(int id)
        {
            RestaurantCtrl ctrl = new RestaurantCtrl();
            var res = ctrl.GetRestaurant(id);
            return res;
        }
        public void CreateRestaurantCategory(ModelLibrary.RestaurantCategory res)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            db.ResCats.InsertOnSubmit(RestaurantCtrl.ConvertRestaurantCategoryToDatabase(res));
            db.SubmitChanges();
        }
        public void DeleteRestaurantCategory(int id)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            db.ResCats.DeleteOnSubmit(db.ResCats.First(x => x.id == id));
            db.SubmitChanges();
        }
        public void UpdateRestaurantCategory(ModelLibrary.RestaurantCategory res)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var nres = db.ResCats.First(x => x.id == res.Id);
            nres.name = res.Name;
            db.SubmitChanges();
        }
        public ModelLibrary.RestaurantCategory GetRestaurantCategory(int id)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            return RestaurantCtrl.ConvertRestaurantCategoryToModel(db.ResCats.FirstOrDefault(x => x.id == id));
        }

        public List<ModelLibrary.Restaurant> GetRestaurantsPaged(int zipcode = 0, int categoryId = 0, int page = 0, int amount = 10, bool verified = true, bool discontinued = false)
        {
            return RestaurantCtrl.GetRestaurantsPaged(zipcode, categoryId, page, amount, verified, discontinued);
        }
        #endregion
    }
}
