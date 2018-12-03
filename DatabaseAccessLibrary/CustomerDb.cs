using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class CustomerDb
    {
        public List<DatabaseAccessLibrary.Order> GetOrderHistoryByCustomerId(int id)
        {
            var db = new JustFeastDbDataContext();
            var orderList = new List<Order>();
            var tempOrder = new Order();

            var query = from orders in db.Orders
                join orderHistoryTable in db.OrderHistories on orders.id equals orderHistoryTable.orderId
                where orderHistoryTable.customerId == id
                select new
                {
                    orders.id,
                    orders.restaurantId,
                    orders.dateTime,
                    orders.reservation,
                    orders.noSeats,
                    orders.accepted
                };


            foreach (var order in query)
            {
                tempOrder.id = order.id;
                tempOrder.restaurantId = order.restaurantId;
                tempOrder.dateTime = order.dateTime;
                tempOrder.reservation = order.reservation;
                tempOrder.noSeats = order.noSeats;
                tempOrder.accepted = order.accepted;

                orderList.Add(tempOrder);
            }

            return orderList;
        }
    }
}
