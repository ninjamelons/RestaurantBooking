using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DatabaseAccessLibrary;
using ModelLibrary;
using ControllerLibrary;

namespace RestaurantService
{
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
    }
}
