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
        IEnumerable<ModelLibrary.Restaurant> GetAllRestaurantsByCategory(int categoryId);
        [OperationContract]
        IEnumerable<ModelLibrary.Restaurant> GetAllRestaurantsByZipCode(int zipcode);
        [OperationContract]
        List<ModelLibrary.Restaurant> GetRestaurantsPaged(int zipcode = 0, int categoryId = 0, int page = 0, int amount = 10, bool verified = true, bool discontinued = false);
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
        void CreateRestaurantCategory(RestaurantCategory res);
        [OperationContract]
        void DeleteRestaurantCategory(int id);
        [OperationContract]
        void UpdateRestaurantCategory(RestaurantCategory res);
        [OperationContract]
        RestaurantCategory GetRestaurantCategory(int id);
        [OperationContract]
        ModelLibrary.Restaurant GetRestaurantWithMenu(int id);


        /*[OperationContract]
        void CreateTable(Table table);
        [OperationContract]
        IEnumerable<Table> GetAllTables(int restaurantId);
        [OperationContract]
        Table GetTable(Table table);
        [OperationContract]
        void UpdateTable(Table oldTable, Table newTable);
        [OperationContract]
        void DeleteTable(Table table);*/

        //return newly created orderId
        [OperationContract]
        int ReserveTables(int resId, int NoSeats, DateTime dateTime);

    }
}
