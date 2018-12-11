using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DatabaseAccessLibrary;
using ModelLibrary;
using ControllerLibrary;
using OrderHistory = DatabaseAccessLibrary.OrderHistory;
using OrderLineItem = DatabaseAccessLibrary.OrderLineItem;

namespace RestaurantService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class CustomerService : ICustomerService
    {
        public ModelLibrary.Customer GetCustomerById(int id)
        {
            var db = new JustFeastDbDataContext();
            var cus = db.Customers.FirstOrDefault(x => x.id == id);
            return CustomerCtrl.ConvertCustomerToModel(cus);
        }

        public ModelLibrary.Customer GetCustomerByEmail(string email)
        {
            var db = new JustFeastDbDataContext();
            var cus = db.Customers.FirstOrDefault(x => x.email.Equals(email));
            return CustomerCtrl.ConvertCustomerToModel(cus);
        }

        public ModelLibrary.Customer LoginCustomer(string email, string passwordHashed)
        {
            return CustomerCtrl.LoginCustomer(email, passwordHashed);
        }

        public bool RegisterUser(ModelLibrary.Customer customer, string passwordHashed)
        {
            return CustomerCtrl.RegisterUser(customer, passwordHashed);
        }

        public List<DatabaseAccessLibrary.Order> GetOrderHistoryByCustomerId(int id)
        {
            var db = new JustFeastDbDataContext();
            var ordC = new OrderCtrl();

            var query = from orders in db.Orders
                join orderHistoryTable in db.OrderHistories on orders.id equals orderHistoryTable.orderId
                where orderHistoryTable.customerId == id
                select new DatabaseAccessLibrary.Order
                {
                    id = orders.id,
                    restaurantId = orders.restaurantId, dateTime = orders.dateTime,
                    reservation = orders.reservation, noSeats = orders.noSeats,
                    accepted = orders.accepted
                };

            query.ToList().ForEach(x => x.OrderLineItems.AddRange(ordC.GetOrderLineItemsById(x.id)));

            return query.ToList();
        }
    }
}
