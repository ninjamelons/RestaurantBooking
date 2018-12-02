using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DatabaseAccessLibrary;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IItemService" in both code and config file together.
    [ServiceContract]
    public interface IItemService
    {
        [OperationContract]
        ModelLibrary.ItemCat GetItemCatById(int itemCatId);
        [OperationContract]
        ModelLibrary.ItemCat GetItemCatByName(string itemCatName);
        [OperationContract]
        ModelLibrary.Item GetItem(int itemId);
        [OperationContract]
        ModelLibrary.Item GetItemByNameAndMenuId(string itemName, int menuId);
        [OperationContract]
        ModelLibrary.Item GetItemByName(string itemName);
        [OperationContract]
        ModelLibrary.Price GetItemPrice(ModelLibrary.Item item, int menuId, int itemCatId);

        [OperationContract]
        void CreateItem(ModelLibrary.Item item, int menuId, int itemCatId);

        [OperationContract]
        void UpdateItem(ModelLibrary.Item updatedItem, int menuId, int itemCatId);

        [OperationContract]
        void DeleteItem(int itemId);

        [OperationContract]
        IEnumerable<ModelLibrary.Item> GetAllItemsByCategory(int categoryId);  // ????????
        [OperationContract]
        IEnumerable<ModelLibrary.Item> GetAllItemsByMenu(int menuId);
        [OperationContract]
        IEnumerable<ModelLibrary.Item> GetAllItemsByRestaurant(int restaurandId);

        [OperationContract]
        void CreateItemCat(ModelLibrary.ItemCat itemCat);

        [OperationContract]
        void UpdateItemCat(ModelLibrary.ItemCat beforeItemCat, ModelLibrary.ItemCat afterItemCat);

        [OperationContract]
        void DeleteItemCat(int itemCatId);

        [OperationContract]
        IEnumerable<ModelLibrary.ItemCat> GetAllItemCategories();

        [OperationContract]
        IEnumerable<ModelLibrary.ItemCat> GetItemCats();

        [OperationContract]
        ModelLibrary.ItemCat GetCatByItemCatId(int itemId);


    }
}
