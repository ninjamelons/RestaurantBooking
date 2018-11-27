using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class OrderDb
    {
        public void AddOrder(Order order)
        {
            var db = new JustFeastDbDataContext();
            
            db.Orders.InsertOnSubmit(order);
            db.OrderLineItems.InsertAllOnSubmit(order.OrderLineItems);
            db.SubmitChanges();
        }

        public void AddItemToOrder(int orderId, int itemId)
        {
            var db = new JustFeastDbDataContext();
            var item = db.Items.SingleOrDefault(i => i.id == itemId);
            var exists =
                from lineItem in db.OrderLineItems
                where lineItem.orderId == orderId && lineItem.itemId == itemId
                select lineItem;

            if (exists.Any() && exists.FirstOrDefault() != null)
            {
                exists.FirstOrDefault().quantity++;
            }
            else if(item != null)
            {
                var oli = new OrderLineItem
                {
                    orderId = orderId,
                    itemId = item.id,
                    quantity = 1
                };
                db.OrderLineItems.InsertOnSubmit(oli);
            }
            db.SubmitChanges();
        }

        public int GetLastOrderIdentity()
        {
            var db = new JustFeastDbDataContext();
            return db.Orders.Max(x => x.id);
        }

        public DatabaseAccessLibrary.Order GetOrderById(int id)
        {
            var db = new JustFeastDbDataContext();

            DatabaseAccessLibrary.Order dbO = null;

            dbO = db.Orders.SingleOrDefault(o => o.id == id);

            return dbO;
        }

        public void UpdateOrder(Order order)
        {
            var db = new JustFeastDbDataContext();
            var ord = db.Orders.SingleOrDefault(o => o.id == Convert.ToInt32(order.id));
            ord.OrderLineItems = order.OrderLineItems;
            ord.restaurantId = order.restaurantId;
            ord.accepted = order.accepted;
            ord.dateTime = order.dateTime;
            ord.reservation = order.reservation;
            ord.noSeats = order.noSeats;
            db.SubmitChanges();
        }
    }
}