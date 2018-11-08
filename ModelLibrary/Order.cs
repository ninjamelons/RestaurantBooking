using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ModelLibrary
{
    public class Order
    {
        [RegularExpression("^[0-9]{7,7}$", ErrorMessage = "Invalid number")]
        public string OrderId { get; set; }

        [RegularExpression("[-{2}][s][:{2}]{19,24}$", ErrorMessage = "Invalid dateTime")]
        public string DateTime { get; set; }

        [RegularExpression("^[-{2}][s][:{2}]{19,24}$", ErrorMessage = "Invalid dateTime")]
        public string ReservationDateTime { get; set; }

        [@RequiredAttribute()]
        public List<Item> ItemsList { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid number")]
        public string NoSeats { get; set; }

        [RegularExpression("^[true][false]", ErrorMessage = "Invalid boolean")]
        public bool Accepted { get; set; }
    }
}