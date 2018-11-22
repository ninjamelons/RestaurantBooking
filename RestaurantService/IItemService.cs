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
        void CreateItem(ModelLibrary.Item item);

        [OperationContract]
        void UpdateItem(ModelLibrary.Item item);

        [OperationContract]
        void DeleteItem(ModelLibrary.Item item);

        [OperationContract]
        IEnumerable<ModelLibrary.Item> GetAllItems();

        [OperationContract]
        IEnumerable<ModelLibrary.Item> GetAllItemsByCategory(int categoryId);  // ????????

        IEnumerable<ModelLibrary.Item> GetAllItemsByMenu(int menuId);

        [OperationContract]
        void CreateItemCat(ModelLibrary.ItemCat itemCat);

        [OperationContract]
        void UpdateItemCat(ModelLibrary.ItemCat itemCat);

        [OperationContract]
        void DeleteItemCat(ModelLibrary.ItemCat itemCat);

        [OperationContract]
        IEnumerable<ModelLibrary.ItemCat> GetAllItemCategories();

    }
}
