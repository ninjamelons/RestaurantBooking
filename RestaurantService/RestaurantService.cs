using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DatabaseAccessLibrary;
using ModelLibrary;
using ControllerLibrary;
using Item = ModelLibrary.Item;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestaurantService" in both code and config file together.
    public class RestaurantService : IRestaurantService
    {
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
            foreach(var x in res)
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
            if(restaurant.Category != null)
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
          //  if (restaurant.Category != null)
          //      res.ResCat = db.ResCats.FirstOrDefault(x => x.id == restaurant.Category.Id);

            db.SubmitChanges();
        }
        public void DeleteRestaurant(ModelLibrary.Restaurant restaurant)
        {
            var db = new JustFeastDbDataContext();
            db.Restaurants.DeleteOnSubmit(RestaurantCtrl.ConvertRestaurantToDatabase(restaurant));
            db.SubmitChanges();
        }
        public IEnumerable<RestaurantCategory> GetAllRestaurantCategories()
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var cats = db.ResCats.ToList();
            List<RestaurantCategory> mRes = new List<RestaurantCategory>();
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
        public void CreateRestaurantCategory(RestaurantCategory res)
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
        public void UpdateRestaurantCategory(RestaurantCategory res)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var nres = db.ResCats.First(x => x.id == res.Id);
            nres.name = res.Name;
            db.SubmitChanges();
        }
        public RestaurantCategory GetRestaurantCategory(int id)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            return RestaurantCtrl.ConvertRestaurantCategoryToModel(db.ResCats.First(x => x.id == id));
        }

        public List<ModelLibrary.Restaurant> GetRestaurantsPaged(int zipcode = 0, int categoryId = 0, int page = 0, int amount = 10, bool verified = true, bool discontinued = false)
        {
            return RestaurantCtrl.GetRestaurantsPaged(zipcode, categoryId, page, amount, verified, discontinued);
        }
    }
}
