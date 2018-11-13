﻿using System;
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
            throw new NotImplementedException();
        }

        public void DeleteTable(Table table)
        {
            throw new NotImplementedException();
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

        public IEnumerable<Table> GetAllTables(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public Table GetTable(Table table)
        {
            throw new NotImplementedException();
        }

        public void CreateRestaurant(ModelLibrary.Restaurant restaurant)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            db.Restaurants.InsertOnSubmit(RestaurantCtrl.ConvertRestaurantToDatabase(restaurant));
            db.SubmitChanges();
        }

        public void UpdateTable(Table table)
        {
            throw new NotImplementedException();
        }

        public void UpdateRestaurant(ModelLibrary.Restaurant restaurant)
        {
            var db = new JustFeastDbDataContext();
            var res = db.Restaurants.SingleOrDefault(x => x.id == restaurant.Id);
            var r = RestaurantCtrl.ConvertRestaurantToDatabase(restaurant);
            res.name = r.name;
            res.address = r.address;
            res.email = r.email;
            res.phoneNo = r.phoneNo;
            res.zipcode = r.zipcode;
            res.verified = r.verified;
            db.SubmitChanges();
        }

        public void DeleteRestaurant(int id)
        {
            var db = new JustFeastDbDataContext();
            db.Restaurants.DeleteOnSubmit(db.Restaurants.FirstOrDefault(x => x.id == id));
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

        public ModelLibrary.Restaurant GetRestaurant(int id)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var res = db.Restaurants.FirstOrDefault(x => x.id == id);
            return RestaurantCtrl.ConvertRestaurantToModel(res);
        }
    }
}
