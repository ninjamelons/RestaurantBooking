using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace UserWebClient.Models
{
    public class RestaurantOrderModel
    {
        public Restaurant Restaurant { get; set; }
        public int OrderId { get; set; }
        public ModelLibrary.Menu menu { get; set; }
    }
}