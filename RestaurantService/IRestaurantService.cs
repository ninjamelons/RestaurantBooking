using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DatabaseAccessLibrary;
using ModelLibrary;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRestaurantService" in both code and config file together.
    [ServiceContract]
    public interface IRestaurantService
    {
        [OperationContract]
        IEnumerable<ModelLibrary.Restaurant> GetAllRestaurants();
        [OperationContract]
        void RegisterRestaurant(ModelLibrary.Restaurant restaurant);

        [OperationContract]
        void CreateTable(Table table);
        [OperationContract]
        IEnumerable<Table> GetAllTables(int restaurantId);
        [OperationContract]
        Table GetTable(Table table);
        [OperationContract]
        void UpdateTable(Table table);
        [OperationContract]
        void DeleteTable(Table table);
    }
}
