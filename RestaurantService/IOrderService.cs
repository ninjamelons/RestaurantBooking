using System.Collections.Generic;
using System.ServiceModel;
using ModelLibrary;

namespace RestaurantService
{
    [ServiceContract]
    public interface IOrderService
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