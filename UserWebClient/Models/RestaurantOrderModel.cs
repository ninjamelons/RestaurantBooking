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

        [Required]
        [Display(Name = "Number of Persons")]
        public int NoSeats { get; set; }
        
        [Required]
        [Display(Name = "Reservation Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime ReserveDateTime { get; set; }
    }
}