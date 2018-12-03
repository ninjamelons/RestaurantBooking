using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ControllerLibrary;
using DatabaseAccessLibrary;
using Order = DatabaseAccessLibrary.Order;

namespace RestaurantService
{
    class OrderService : IOrderService
    {
        public void AddItemToOrder(int orderId, int itemId, int resId)
        {
            var ordC = new OrderCtrl();
            var db = new JustFeastDbDataContext();
            var item = db.Items.SingleOrDefault(i => i.id == itemId);

            if (orderId == 0)
            {
                orderId = ordC.GetLastOrderIdentity() + 1;
                var order = new Order
                {
                    id = orderId,
                    restaurantId = resId
                };
                db.Orders.InsertOnSubmit(order);
            }
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
            var ordC = new OrderCtrl();
            return ordC.GetOrderById(id);
        }

        public void UpdateOrder(Order order)
        {
            var db = new JustFeastDbDataContext();
            var ord = db.Orders.SingleOrDefault(o => o.id == Convert.ToInt32(order.id));
            db.SubmitChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var ctrl = new OrderCtrl();
            ctrl.DeleteOrder(orderId);
        }

        public void DeleteItemById(int orderId, int itemId)
        {
            var ctrl = new OrderCtrl();
            ctrl.DeleteItemById(orderId, itemId);
        }
    }
}
