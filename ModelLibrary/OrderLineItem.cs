using System;
using System.Collections.Generic;

namespace ModelLibrary
{
    public class OrderLineItem
    {
        public int Price;

        public OrderLineItem(Item item, int quantity)
        {
            LineItem = item;
            Quantity = quantity;

        }

        public Item LineItem { get; set; }

        public int Quantity { get; set; }
    }
}