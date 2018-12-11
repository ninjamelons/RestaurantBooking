using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ControllerLibrary;
using DatabaseAccessLibrary;
using ModelLibrary;
using Order = DatabaseAccessLibrary.Order;

namespace RestaurantService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
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
                var order = new DatabaseAccessLibrary.Order
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
                var oli = new DatabaseAccessLibrary.OrderLineItem
                {
                    orderId = orderId,
                    itemId = item.id,
                    quantity = 1
                };
                db.OrderLineItems.InsertOnSubmit(oli);
            }
            db.SubmitChanges();
        }

        public void CreateOrder(ModelLibrary.Order order)
        {
            var db = new JustFeastDbDataContext();
            db.Orders.InsertOnSubmit(new OrderCtrl().ConvertOrder(order));
            db.SubmitChanges();
        }

        public void UpdateOrder(ModelLibrary.Order order)
        {
            var db = new JustFeastDbDataContext();
            var dbOrder = db.Orders.FirstOrDefault(x => x.id == Convert.ToInt32(order.OrderId));
            dbOrder.accepted = order.Accepted;
            db.SubmitChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var ctrl = new OrderCtrl();
            //ctrl.DeleteOrder(orderId);
        }

        public void DeleteItemById(int orderId, int itemId)
        {
            var ctrl = new OrderCtrl();
            ctrl.DeleteItemById(orderId, itemId);
        }

        public ModelLibrary.Order GetOrderById(int orderId)
        {
            var ctrl = new OrderCtrl();
            var order = ctrl.GetOrderById(orderId);
            return order;
        }
    }
}
