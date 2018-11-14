using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IItemService" in both code and config file together.
    [ServiceContract]
    public interface IItemService
    {
        [OperationContract]
        void CreateItem(Item item);

        [OperationContract]
        void UpdateItem(Item item);

        [OperationContract]
        void DeleteItem(Item item);

        [OperationContract]
        IEnumerable<Item> GetAllItems();

        [OperationContract]
        IEnumerable<Item> GetAllItemsByCategory();

    }
}
