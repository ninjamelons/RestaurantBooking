using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using DatabaseAccessLibrary;
using OrderLineItem = ModelLibrary.OrderLineItem;

namespace ControllerLibrary
{
    public class CustomerCtrl
    {
        public static ModelLibrary.Customer LoginCustomer(string email, string passwordHashed)
        {
            var db = new JustFeastDbDataContext();
            var customer = db.Customers.FirstOrDefault(x => x.email.Equals(email));

            if (customer == null)
                return null;

            bool match = db.Users.FirstOrDefault(x => x.customerId == customer.id).password == passwordHashed;

            return match ? ConvertCustomerToModel(customer) : null;   
        }

        public static ModelLibrary.Customer ConvertCustomerToModel(DatabaseAccessLibrary.Customer customer)
        {
            if (customer == null)
                return null;

            var mCus = new ModelLibrary.Customer
            {
                Address = customer.address,
                Email = customer.email,
                Id = customer.id,
                Name = customer.name,
                RoleId = customer.roleId,
                PhoneNo = customer.phoneNo.ToString()
            };

            return mCus;
        }

        public List<DatabaseAccessLibrary.Order> GetOrdersByCustomerId(int id)
        {
            var cusDb = new CustomerDb();
            var ordC = new OrderCtrl();
            var orderHistory = cusDb.GetOrderHistoryByCustomerId(id);
            orderHistory.ForEach(x => x.OrderLineItems.AddRange(ordC.GetOrderLineItemsById(x.id)));
            return orderHistory;
        }

        public static DatabaseAccessLibrary.Customer ConvertCustomerToDatabase(ModelLibrary.Customer mCus)
        {
            if (mCus == null)
                return null;

            var customer = new DatabaseAccessLibrary.Customer
            {
                address = mCus.Address,
                email = mCus.Email,
                id = mCus.Id,
                name = mCus.Name,
                roleId = mCus.RoleId,
            };
            Int32.TryParse(mCus.PhoneNo, out int no);
            customer.phoneNo = no;

            return customer;
        }

        public static bool RegisterUser(ModelLibrary.Customer mCus, string passwordHashed)
        {
            var customer = ConvertCustomerToDatabase(mCus);
            if (customer == null)
                return false;

            var db = new JustFeastDbDataContext();
            if (db.Customers.Any(x => x.email.Equals(customer.email)))
                return false;

            db.Customers.InsertOnSubmit(customer);
            db.SubmitChanges();

            db.Users.InsertOnSubmit(new DatabaseAccessLibrary.User
            {
                customerId = db.Customers.FirstOrDefault(x => x.email == customer.email).id,
                password = passwordHashed
            });
            db.SubmitChanges();
            return true;
        }
    }
}
