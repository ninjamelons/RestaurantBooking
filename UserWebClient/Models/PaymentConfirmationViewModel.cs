using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebClient.Models
{
    public class PaymentConfirmationViewModel
    {
        public string Result { get; set; }
        public string OrderId { get; set; }
        public long Amount { get; set; }
    }
}