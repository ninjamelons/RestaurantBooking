using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class OrderHistory
    {
        public int customerId { get; set; }

        public List<Order> orders { get; set; }
    }
}
