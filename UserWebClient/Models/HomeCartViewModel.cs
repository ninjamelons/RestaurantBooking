using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace UserWebClient.Models
{
    public class HomeCartViewModel
    {
        public Order Order { get; set; }
        public int TotalPrice { get; set; }
    }
}