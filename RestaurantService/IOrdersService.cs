using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ModelLibrary;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOrdersService" in both code and config file together.
    [ServiceContract]
    public interface IOrdersService
    {
        [OperationContract]
        ModelLibrary.Order GetOrderById(int id);

        [OperationContract]
        void CreateOrder(Order order);

        [OperationContract]
        void UpdateOrder(Order order);

        [OperationContract]
        void AddItemToOrder(int orderId, int itemId);
    }
}
