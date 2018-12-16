using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using DatabaseAccessLibrary;
using System.Linq;

namespace ControllerLibrary
{
    public class OrderCtrl
    {
        public Order ConvertOrder(ModelLibrary.Order order)
        {
            var returnOrder = new Order();

            returnOrder.id = Convert.ToInt32(order.OrderId);
            returnOrder.restaurantId = Convert.ToInt32(order.RestaurantId);
            returnOrder.dateTime = Convert.ToDateTime(order.DateTime);
            returnOrder.reservation = Convert.ToDateTime(order.ReservationDateTime);
            returnOrder.OrderLineItems.AddRange(ConvertOrderLineItemsToDb(order));
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
                Accepted = order.accepted,
                ItemsList = ConvertOrderLineItemsToModel(order.OrderLineItems)
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
            var ordDb = new OrderDb();
            var dbOrder = ConvertOrder(order);
            dbOrder.OrderLineItems.AddRange(ConvertOrderLineItemsToDb(order));

            return ordDb.AddOrder(dbOrder);
        }

        public IEnumerable<OrderLineItem> GetOrderLineItemsById(int id)
        {
            var ordDb = new OrderDb();
            var items = ordDb.GetOrderLineItemsById(id);
            return items;
        }

        public void AddItemToOrder(int orderId, int itemId)
        {
            var ordDb = new OrderDb();
            ordDb.AddItemToCart(orderId, itemId);
        }

        public ModelLibrary.Order GetOrderById(int id)
        {
            var ordDb = new OrderDb();
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

        public IEnumerable<ModelLibrary.Order> GetAllOrdersByRestaurant(int restaurantId)
        {
            var orderDb = new OrderDb();
            var orderCtrl = new OrderCtrl();
            var dbOrders = orderDb.GetAllRestaurantOrders(restaurantId);
            var orderList = new List<ModelLibrary.Order>();
            foreach(var order in dbOrders)
            {
                 var modelOrderList = orderCtrl.ConvertOrderToModel(order);
                 orderList.Add(modelOrderList);
            }
            return orderList;
        }

        public void UpdateOrder(Order order)
        {
            var ordDb = new OrderDb();
            ordDb.UpdateOrder(order);
        }

        public int GetLastOrderIdentity()
        {
            var ordDb = new OrderDb();
            return ordDb.GetLastOrderIdentity();
        }

        public void DeleteOrder(int orderId)
        {
            var db = new JustFeastDbDataContext();
        }

        public void DeleteOrderLineItem(int itemId)
        {
            var db = new JustFeastDbDataContext();
            var oli = db.OrderLineItems.Where(x => x.itemId == itemId);
            db.OrderLineItems.DeleteAllOnSubmit(oli);
            db.SubmitChanges();
        }

        public void DeleteItemById(int orderId, int itemId)
        {
            var db = new JustFeastDbDataContext();
            var lineitem = db.OrderLineItems.FirstOrDefault(x => x.orderId == orderId && x.itemId == itemId);
            if (lineitem.quantity > 1)
                lineitem.quantity--;
            else
                db.OrderLineItems.DeleteOnSubmit(lineitem);
            db.SubmitChanges();
        }

      
    }

}