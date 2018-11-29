using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    class Cart
    {
        public int Id { get; set; }

        public IEnumerable<Order>Orders { get; set; }

        public IEnumerable<Item>Items { get; set; }

        public IEnumerable<OrderLineItem>OrderLineItems { get; set; }

        public int Count { get; set; }

        public System.DateTime DateCreated { get; set; }
    }
}
