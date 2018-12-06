using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class OrderDb
    {
        public int AddOrder(Order order)
        {
            var db = new JustFeastDbDataContext();

            db.Orders.InsertOnSubmit(order);
            db.OrderLineItems.InsertAllOnSubmit(order.OrderLineItems);
            db.SubmitChanges();

            return order.id;
        }

        public void AddItemToCart(int orderId, int itemId)
        {
            var db = new JustFeastDbDataContext();
            var item = db.Items.SingleOrDefault(i => i.id == itemId);
            var exists = db.OrderLineItems.SingleOrDefault(i => i.itemId == itemId && i.orderId == orderId);

            if (exists != null)
            {
                exists.quantity++;
            }
            else if (item != null)
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

        public void CreateOrder(Order order)
        {
            var db = new JustFeastDbDataContext();
            db.Orders.InsertOnSubmit(order);
            db.SubmitChanges();
        }

        public Order GetOrderById(int id)
        {
            var db = new JustFeastDbDataContext();
            return db.Orders.FirstOrDefault(o => o.id == id);
        }

        public void UpdateOrder(Order order)
        {
            var db = new JustFeastDbDataContext();
            var ord = db.Orders.SingleOrDefault(o => o.id == order.id);
            if (ord != null)
            {
                ord.OrderLineItems = order.OrderLineItems;
                ord.restaurantId = order.restaurantId;
                ord.accepted = order.accepted;
                ord.dateTime = order.dateTime;
                ord.reservation = order.reservation;
                ord.noSeats = order.noSeats;
                db.SubmitChanges();
            }
        }

        public List<OrderLineItem> OliList(int id)
        {
            var db = new JustFeastDbDataContext();
            return db.OrderLineItems.Where(x => x.orderId == id).ToList();
        }

        public int GetLastOrderIdentity()
        {
            var db = new JustFeastDbDataContext();
            return db.Orders.Max(x => x.id);
        }

        public void DeleteOrder(int id)
        {
            var db = new JustFeastDbDataContext();

            db.Orders.DeleteOnSubmit(db.Orders.FirstOrDefault(x => x.id == id));
            db.SubmitChanges();
        }

        public IEnumerable<OrderLineItem> GetOrderLineItemsById(int id)
        {
            var db = new JustFeastDbDataContext();
            var items = db.OrderLineItems.Where(oi => oi.orderId == id).AsEnumerable();
            return items;
        }
    }

}