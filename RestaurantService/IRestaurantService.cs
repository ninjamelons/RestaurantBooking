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
    [ServiceContract(Name = "IRestaurantService")]
    public interface IRestaurantService
    {
        [OperationContract]
        IEnumerable<ModelLibrary.Restaurant> GetAllRestaurants();
        [OperationContract]
        IEnumerable<ModelLibrary.Restaurant> GetAllRestaurantsByCategory(int categoryId);
        [OperationContract]
        IEnumerable<ModelLibrary.Restaurant> GetAllRestaurantsByZipCode(int zipcode);
        [OperationContract]
        void CreateRestaurant(ModelLibrary.Restaurant restaurant);
        [OperationContract]
        void UpdateRestaurant(ModelLibrary.Restaurant restaurant);
        [OperationContract]
        void DeleteRestaurant(int id);
        [OperationContract]
        IEnumerable<ModelLibrary.RestaurantCategory> GetAllRestaurantCategories();
        [OperationContract]
        ModelLibrary.Restaurant GetRestaurant(int id);

        [OperationContract]
        void CreateTable(Table table);
        [OperationContract]
        IEnumerable<Table> GetAllTables(int restaurantId);
        [OperationContract]
        Table GetTable(Table table);
        [OperationContract]
        void UpdateTable(Table oldTable, Table newTable);
        [OperationContract]
        void DeleteTable(Table table);

    }
}
