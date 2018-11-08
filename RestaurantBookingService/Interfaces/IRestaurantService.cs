using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ModelLibrary;

namespace RestaurantBookingService.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRestaurantService" in both code and config file together.
    [ServiceContract]
    public interface IRestaurantService
    {
        [OperationContract]
        IEnumerable<Restaurant> GetAllRestaurants();
        [OperationContract]
        void RegisterRestaurant(Restaurant restaurant);

        [OperationContract]
        void CreateTable(Table table);
        [OperationContract]
        IEnumerable<Table> GetAllTables(Restaurant restaurant);
        [OperationContract]
        IEnumerable<Table> GetTable(Table table);
        [OperationContract]
        void UpdateTable(Table table);
        [OperationContract]
        void DeleteTable(Table table);
    }
}
