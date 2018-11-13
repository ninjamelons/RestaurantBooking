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

            if (db.Orders.Any(o => (o.id != order.id)))
            {
                db.Orders.InsertOnSubmit(order);
                for (int i = 0; i < order.OrderLineItems.Count - 1; i++)
                {
                    db.OrderLineItems.InsertOnSubmit(order.OrderLineItems[i]);

                }
                db.OrderHistories.InsertOnSubmit(order.OrderHistories[0]);
                db.SubmitChanges();
            }

        }
    }
}