﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DatabaseAccessLibrary;
using ModelLibrary;

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

        public void DoWork()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ModelLibrary.Restaurant> GetAllRestaurants()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Table> GetAllTables(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public Table GetTable(Table table)
        {
            throw new NotImplementedException();
        }

        public void RegisterRestaurant(ModelLibrary.Restaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public void UpdateTable(Table table)
        {
            throw new NotImplementedException();
        }
    }
}
