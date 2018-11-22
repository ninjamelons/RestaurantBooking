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

            /*returnOrder.id = Convert.ToInt32(order.OrderId);
            returnOrder.restaurantId = Convert.ToInt32(order.RestaurantId);
            returnOrder.dateTime = Convert.ToDateTime(order.DateTime);
            returnOrder.reservation = Convert.ToDateTime(order.ReservationDateTime);
            //returnOrder.OrderLineItems = order.ItemsList;
            returnOrder.OrderHistories = new EntitySet<OrderHistory>();
            //returnOrder.OrderHistories.Add(new OrderHistory(returnOrder.id, Convert.ToInt32(order.CustomerId), Convert.ToDouble(order.Payment));
            returnOrder.noSeats = Convert.ToInt32(order.NoSeats);
            returnOrder.accepted = order.Accepted;*/

            return returnOrder;
        }

        public ModelLibrary.Order CreateOrder(string id, string customerId, string resId, string dateTime, string reservationDateTime,
            List<ModelLibrary.Item> orderLineItems, string noSeats, string payment)
        {
            var order = new ModelLibrary.Order
            {
                OrderId = id,
                CustomerId = customerId,
                RestaurantId = resId,
                DateTime = dateTime,
                ReservationDateTime = reservationDateTime,
                //ItemsList = orderLineItems,
                NoSeats = noSeats,
                Payment = payment,
                Accepted = false
            };
            var context = new ValidationContext(order, null, null);
            var result = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(order, context, result, true);
            if (!isModelStateValid)
                throw new ValidationException();
            return order;
        }
    }
}