using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebClient.Models
{
    public class RestaurantViewModel
    {
        public Restaurant Restaurant { get; set; }
        public Dictionary<string, RestaurantCategory> Categories { get; set; }
        
    }
}