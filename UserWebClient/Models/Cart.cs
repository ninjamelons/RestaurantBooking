using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace UserWebClient.Models
{
    public class Cart
    {
        public string ItemId { get; set; }
        public string CartId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    
        public System.DateTime DateCreated { get; set; }



    }
}