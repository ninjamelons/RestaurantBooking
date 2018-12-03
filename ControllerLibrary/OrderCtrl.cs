﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using DatabaseAccessLibrary;
using ModelLibrary;
using Order = DatabaseAccessLibrary.Order;
using OrderLineItem = DatabaseAccessLibrary.OrderLineItem;

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
            var oli = new List<DatabaseAccessLibrary.OrderLineItem>();
            foreach (var item in order.OrderLineItems)
            {
                oli.Add(item);
            }

            returnOrder.ItemsList.AddRange((ConvertOrderLineItemsToModel(oli)));
            return returnOrder;
        }

        public List<OrderLineItem> ConvertOrderLineItemsToDb(ModelLibrary.Order order)
        {
            var oli = new OrderLineItem();
            var returnOliList = new List<OrderLineItem>();
            for (int i = 0; i < order.ItemsList.Count; i++)
            {
                oli.orderId = Convert.ToInt32(order.OrderId);
                oli.itemId = Convert.ToInt32(order.ItemsList[i].LineItem.Id);
                oli.quantity = Convert.ToInt32(order.ItemsList[i].Quantity);

                returnOliList.Add(oli);
            }
            return returnOliList;
        }

        public List<ModelLibrary.OrderLineItem> ConvertOrderLineItemsToModel(List<DatabaseAccessLibrary.OrderLineItem> olis)
        {
            var modelItems = new List<ModelLibrary.OrderLineItem>();
            var itemC = new ItemCtrl();
            for (int i = 0; i < olis.Count; i++)
            {
                modelItems.Add(new ModelLibrary.OrderLineItem(itemC.GetItem(olis[i].itemId), olis[i].quantity));
            }

            return modelItems;
        }

        public int AddOrder(ModelLibrary.Order order)
        {
            OrderDb ordDb = new OrderDb();
            Order dbOrder = ConvertOrder(order);
            dbOrder.OrderLineItems.AddRange(ConvertOrderLineItemsToDb(order));

            return ordDb.AddOrder(dbOrder);
        }

        public void AddItemToOrder(int orderId, int itemId)
        {
            OrderDb ordDb = new OrderDb();
            ordDb.AddItemToOrder(orderId, itemId);
        }

        public ModelLibrary.Order GetOrderById(int id)
        {
            OrderDb ordDb = new OrderDb();
            return ConvertOrderToModel(ordDb.GetOrderById(id));
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
    }
}