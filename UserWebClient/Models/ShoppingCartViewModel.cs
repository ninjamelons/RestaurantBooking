using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelLibrary;
using System.Web.Mvc;

namespace UserWebClient.Models
{
    public class ShoppingCartViewModel
    {

        public int OrderId { get; set; }

        public List<OrderLineItem> OrderLineItems { get; set; }

       

    }
}