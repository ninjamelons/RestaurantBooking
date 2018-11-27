using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrderService" in both code and config file together.
    public class OrderService : IOrderService
    {
        public void DoNotDoWork(int num1, int num2)
        {
            Console.Out.WriteLine();
        }

        public void DoWork(int id)
        {
            Console.Out.WriteLine(id);
        }
    }
}
