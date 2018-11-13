using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DatabaseAccessLibrary;
using ModelLibrary;
using ControllerLibrary;

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

        public void CreateRestaurant(ModelLibrary.Restaurant restaurant)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            db.Restaurants.InsertOnSubmit(RestaurantCtrl.ConvertRestaurantToDatabase(restaurant));
            db.SubmitChanges();
        }

        public void UpdateRestaurant(ModelLibrary.Restaurant restaurant)
        {
            var db = new JustFeastDbDataContext();
            var res = db.Restaurants.SingleOrDefault(x => x.id == restaurant.Id);
            res = RestaurantCtrl.ConvertRestaurantToDatabase(restaurant);
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
    }
}
