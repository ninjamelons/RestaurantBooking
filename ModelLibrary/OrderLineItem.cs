using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ModelLibrary
{
    /*[KnownType(typeof(Item))]*/
    public class OrderLineItem
    {
        public OrderLineItem(){}

        public OrderLineItem(Item item, int quantity)
        {
            LineItem = item;
            Quantity = quantity;

        }

        public double Price { get; set; }

        public Item LineItem { get; set; }

        public int Quantity { get; set; }
    }
}