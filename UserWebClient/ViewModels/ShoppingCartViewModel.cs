using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserWebClient.Models;
using ModelLibrary;
using DatabaseAccessLibrary;
using System.ComponentModel.DataAnnotations;

namespace UserWebClient.ViewModels
{
    public class ShoppingCartViewModel
    {
        [Key]
         public List<ModelLibrary.Item> items { get; set; }
        public List<ModelLibrary.Order> OrderLineItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}