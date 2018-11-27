using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ControllerLibrary;
using DatabaseAccessLibrary;
using Order = ModelLibrary.Order;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrdersService" in both code and config file together.
    public class OrdersService : IOrdersService
    {
        public void AddItemToOrder(int orderId, int itemId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var item = db.Items.SingleOrDefault(i => i.id == itemId);
            var exists =
                from lineItem in db.OrderLineItems
                where lineItem.orderId == orderId && lineItem.itemId == itemId
                select lineItem;
            
            if (exists.Any() && exists.FirstOrDefault() != null)
            {
                exists.FirstOrDefault().quantity++;
            }
            else
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

        public void CreateOrder(ModelLibrary.Order order)
        {
            OrderCtrl ordC = new OrderCtrl();
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            db.Orders.InsertOnSubmit(ordC.ConvertOrder(order));
            db.SubmitChanges();
        }

        public ModelLibrary.Order GetOrderById(int id)
        {
            OrderCtrl ordC = new OrderCtrl();
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            return ordC.ConvertOrderToModel(db.Orders.SingleOrDefault(o => o.id == id));
        }

        public void UpdateOrder(Order order)
        {
            var ordC = new OrderCtrl();
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var ord = db.Orders.SingleOrDefault(o => o.id == Convert.ToInt32(order.OrderId));
            ord = ordC.ConvertOrder(order);
            db.SubmitChanges();
        }
    }
}
