using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ControllerLibrary;
using ModelLibrary;

namespace RestaurantBookingService.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestaurantService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RestaurantService.svc or RestaurantService.svc.cs at the Solution Explorer and start debugging.
    public class RestaurantService : IRestaurantService
    {
        IEnumerable<Restaurant> IRestaurantService.GetAllRestaurants()
        {
            throw new NotImplementedException();
        }

        void IRestaurantService.RegisterRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }
        
        void IRestaurantService.CreateTable(Table table)
        {
            throw new NotImplementedException();
        }
        IEnumerable<Table> IRestaurantService.GetAllTables(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Table> GetTable(Table table)
        {
            throw new NotImplementedException();
        }
        void IRestaurantService.UpdateTable(Table table)
        {
            throw new NotImplementedException();
        }
        void IRestaurantService.DeleteTable(Table table)
        {
            throw new NotImplementedException();
        }
    }
}
