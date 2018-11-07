using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebClient.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public RestaurantCategory Category { get; set; }
    }
}