using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ModelLibrary
{
    public class Order
    {
        [RegularExpression("(\\d){7,7}$", ErrorMessage = "Invalid number")]
        public string OrderId { get; set; }
        
        public string CustomerId { get; set; }
        
        public string RestaurantId { get; set; }
        
        [RegularExpression("[0-9-: ]{18,24}$", ErrorMessage = "Invalid dateTime")]
        public string DateTime { get; set; }
        
        [RegularExpression("[0-9-: ]{18,24}$", ErrorMessage = "Invalid dateTime")]
        public string ReservationDateTime { get; set; }
        
        public List<OrderLineItem> ItemsList { get; set; }
        
        [RegularExpression("[0-9]*$", ErrorMessage = "Invalid number")]
        public string NoSeats { get; set; }
        
        public string Payment { get; set; }

        public bool Accepted { get; set; }


        public int TotalPriceCent { get
            {
                return (int)(ItemsList.Sum(x => x.LineItem.Price.VarPrice * x.Quantity) * 100);
            }
        }
    }
}