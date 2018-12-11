using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using DatabaseAccessLibrary;

namespace ControllerLibrary
{
    public class OrderCtrl
    {
        public Order ConvertOrder(ModelLibrary.Order order)
        {
            Order returnOrder = new Order();

            returnOrder.id = Convert.ToInt32(order.OrderId);
            returnOrder.restaurantId = Convert.ToInt32(order.RestaurantId);
            returnOrder.dateTime = Convert.ToDateTime(order.DateTime);
            returnOrder.reservation = Convert.ToDateTime(order.ReservationDateTime);
            returnOrder.OrderLineItems.AddRange(ConvertOrderLineItemsToDb(order));
            //returnOrder.OrderHistories.Add(new OrderHistory(returnOrder.id, Convert.ToInt32(order.CustomerId), Convert.ToDouble(order.Payment));
            returnOrder.noSeats = Convert.ToInt32(order.NoSeats);
            returnOrder.accepted = order.Accepted;

            return returnOrder;
        }

        public ModelLibrary.Order ConvertOrderToModel(DatabaseAccessLibrary.Order order)
        {
            var returnOrder = new ModelLibrary.Order
            {
                OrderId = order.id.ToString(),
                RestaurantId = order.restaurantId.ToString(),
                DateTime = order.dateTime.ToString(),
                ReservationDateTime = order.reservation.ToString(),
                NoSeats = order.noSeats.ToString(),
                Accepted = order.accepted
            };
            return returnOrder;
        }

        public List<OrderLineItem> ConvertOrderLineItemsToDb(ModelLibrary.Order order)
        {
            var oli = new OrderLineItem();
            var returnOliList = new List<OrderLineItem>();
            if (order.ItemsList == null) 
                return returnOliList;
            foreach (var item in order.ItemsList)
            {
                oli.orderId = Convert.ToInt32(order.OrderId);
                oli.itemId = Convert.ToInt32(item.LineItem.Id);
                oli.quantity = Convert.ToInt32(item.Quantity);

                returnOliList.Add(oli);
            }
            return returnOliList;
        }

        public int AddOrder(ModelLibrary.Order order)
        {
            OrderDb ordDb = new OrderDb();
            Order dbOrder = ConvertOrder(order);
            dbOrder.OrderLineItems.AddRange(ConvertOrderLineItemsToDb(order));

            return ordDb.AddOrder(dbOrder);
        }

        public IEnumerable<OrderLineItem> GetOrderLineItemsById(int id)
        {
            OrderDb ordDb = new OrderDb();
            var items = ordDb.GetOrderLineItemsById(id);
            return items;
        }

        public void AddItemToOrder(int orderId, int itemId)
        {
            OrderDb ordDb = new OrderDb();
            ordDb.AddItemToCart(orderId, itemId);
        }

        public ModelLibrary.Order GetOrderById(int id)
        {
            OrderDb ordDb = new OrderDb();
            var order = ConvertOrderToModel(ordDb.GetOrderById(id));
            order.ItemsList = ConvertOrderLineItemsToModel(GetOrderLineItemsById(id));
            return order;
        }

        private List<ModelLibrary.OrderLineItem> ConvertOrderLineItemsToModel(IEnumerable<OrderLineItem> orderLineItems)
        {
            var itemCtrl = new ItemCtrl();
            var itemsList = new List<ModelLibrary.OrderLineItem>();
            foreach (var item in orderLineItems)
            {
                var orderItem = new ModelLibrary.OrderLineItem
                    {
                        LineItem = itemCtrl.ConvertItemToModel(item.Item),
                        Quantity = item.quantity
                    };
                itemsList.Add(orderItem);
            }

            return itemsList;
        }

        public void UpdateOrder(Order order)
        {
            OrderDb ordDb = new OrderDb();
            ordDb.UpdateOrder(order);
        }

        public int GetLastOrderIdentity()
        {
            OrderDb ordDb = new OrderDb();
            return ordDb.GetLastOrderIdentity();
        }

        public void DeleteOrder(int orderId)
        {
            OrderDb db = new OrderDb();
        }

        public void DeleteItemById(int orderId, int itemId)
        {
            OrderDb db = new OrderDb();
        }

      
    }

}