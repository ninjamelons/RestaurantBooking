﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOrderService" in both code and config file together.
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        void AddItemToOrder(int orderId, int itemId, int resId);
        [OperationContract]
        void CreateOrder(ModelLibrary.Order order);
        [OperationContract]
        void UpdateOrder(ModelLibrary.Order order);
        [OperationContract]
        void DeleteOrder(int orderId);
        [OperationContract]
        void DeleteItemById(int orderId, int itemId);

        [OperationContract]
        ModelLibrary.Order GetOrderById(int orderId);
        [OperationContract]
        IEnumerable<ModelLibrary.Order> GetAllOrdersByRestaurant(int restaurantId);
    }
}
