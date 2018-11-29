using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DatabaseAccessLibrary;
using ModelLibrary;

namespace RestaurantService
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        ModelLibrary.Customer LoginCustomer(string email, string passwordHashed);
        [OperationContract]
        ModelLibrary.Customer GetCustomerById(int id);
        [OperationContract]
        ModelLibrary.Customer GetCustomerByEmail(string email);
        [OperationContract]
        bool RegisterUser(ModelLibrary.Customer customer, string passwordHashed);
    }
}
