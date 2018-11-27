using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ModelLibrary;

namespace RestaurantService
{
    interface IAdminService
    {
        #region Items
        [OperationContract]
        void CreateItem(ModelLibrary.Item item, int menuId);
        [OperationContract]
        void UpdateItem(ModelLibrary.Item beforeItem, ModelLibrary.Item afterItem, int menuId);
        [OperationContract]
        void DeleteItem(ModelLibrary.Item item, int menuId);
        [OperationContract]
        IEnumerable<ModelLibrary.Item> GetAllItemsByCategory(int categoryId);  // ????????
        [OperationContract]
        IEnumerable<ModelLibrary.Item> GetAllItemsByMenu(int menuId);
        [OperationContract]
        void CreateItemCat(ModelLibrary.ItemCat itemCat);
        [OperationContract]
        void UpdateItemCat(ModelLibrary.ItemCat beforeItemCat, ModelLibrary.ItemCat afterItemCat);
        [OperationContract]
        void DeleteItemCat(ModelLibrary.ItemCat itemCat);
        [OperationContract]
        IEnumerable<ModelLibrary.ItemCat> GetAllItemCategories();
        #endregion

        #region Menu
        [OperationContract]
        void CreateMenu(ModelLibrary.Menu menu, int restaurantId);

        [OperationContract]
        void UpdateMenu(ModelLibrary.Menu beforeMenu, ModelLibrary.Menu afterMenu, int restaurantId);

        [OperationContract]
        void DeleteMenu(ModelLibrary.Menu menu, int restaurantId);

        [OperationContract]
        IEnumerable<ModelLibrary.Menu> GetAllMenusByRestaurant(int restaurantId);
        #endregion

        #region Order
        [OperationContract]
        ModelLibrary.Order GetOrderById(int id);

        [OperationContract]
        void CreateOrder(Order order);

        [OperationContract]
        void UpdateOrder(Order order);

        [OperationContract]
        void AddItemToOrder(int orderId, int itemId);
        #endregion

        #region Price
        [OperationContract]
        void CreatePrice(ModelLibrary.Price price);

        [OperationContract]
        void DeletePrice(ModelLibrary.Price price);

        [OperationContract]
        IEnumerable<ModelLibrary.Price> GetAllPricesByItem(string id);

        [OperationContract]
        void UpdatePrice(ModelLibrary.Price beforePrice, ModelLibrary.Price afterPrice);
        #endregion

        #region Restaurant
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
        void CreateTable(Table table);
        [OperationContract]
        IEnumerable<Table> GetAllTables(int restaurantId);
        [OperationContract]
        Table GetTable(Table table);
        [OperationContract]
        void UpdateTable(Table oldTable, Table newTable);
        [OperationContract]
        void DeleteTable(Table table);
        #endregion
    }
}
