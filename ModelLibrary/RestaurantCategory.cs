using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ModelLibrary
{
    public class RestaurantCategory
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}