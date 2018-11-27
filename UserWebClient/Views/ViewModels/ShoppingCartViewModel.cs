using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserWebClient;
using System.ComponentModel.DataAnnotations;
using ModelLibrary;
/*
namespace UserWebClient.Views.ViewModels
{
    public class ShoppingCartViewModel
    {
        internal object CartItems;

        [Key]
            public List<OrderLineItem> Items { get; set; }
           public decimal CartTotal { get; set; }
      
    }
}