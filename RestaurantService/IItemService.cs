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
        ModelLibrary.Item GetItemByNameAndMenuId(string itemName, int menuId);
        [OperationContract]
        ModelLibrary.Price GetItemPrice(ModelLibrary.Item item);

        [OperationContract]
        void CreateItem(ModelLibrary.Item item);

        [OperationContract]
        void UpdateItem(ModelLibrary.Item beforeItem, ModelLibrary.Item afterItem);

        [OperationContract]
        void DeleteItem(ModelLibrary.Item item);

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
        void DeleteItemCat(ModelLibrary.ItemCat itemCat);

        [OperationContract]
        IEnumerable<ModelLibrary.ItemCat> GetAllItemCategories();

        [OperationContract]
        IEnumerable<ModelLibrary.ItemCat> GetItemCats();



    }
}
