using System;
using System.Collections.Generic;

namespace ModelLibrary
{
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