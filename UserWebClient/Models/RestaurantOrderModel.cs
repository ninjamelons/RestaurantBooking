using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace UserWebClient.Models
{
    public class RestaurantOrderModel
    {
        public Restaurant Restaurant { get; set; }
        public int OrderId { get; set; }
        public int NoSeats { get; set; }
        
        [Display(Name = "Reservation Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime ReserveDateTime { get; set; }
    }
}