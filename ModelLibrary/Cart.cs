using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModelLibrary
{
    class Cart
    {

        public string ItemId { get; set; }
        public string CartId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }

        public System.DateTime DateCreated { get; set; }
    }
}
