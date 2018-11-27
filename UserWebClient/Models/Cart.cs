using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ModelLibrary;
using DatabaseAccessLibrary;
using OrderLineItem = DatabaseAccessLibrary.OrderLineItem;

namespace UserWebClient.Models
{
    public class Cart
    {

        [Key]
        public string CartId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual  OrderLineItem Item{ get; set; }
    }
}