using System.Collections.Generic;
using System.ServiceModel;
using ModelLibrary;

namespace RestaurantService
{
    public interface IOrderService
    {
        ModelLibrary.Order GetOrderById(int id);

        void CreateOrder(Order order);

        void UpdateOrder(Order order);

        void AddItemToOrder(int orderId, int itemId);
    }
}