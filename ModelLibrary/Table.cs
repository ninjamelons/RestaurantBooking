using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Table
    {
        public string RestaurantId { get; set; }

        public int ZipCode { get; set; }

        [RegularExpression("^(\\d)*$",ErrorMessage = "Invalid number: Cannot contain spaces, ")]
        public string NoSeats { get; set; }
        
        [RegularExpression("^(\\d)*$",ErrorMessage = "Invalid number")]
        public string Total { get; set; }
        
        [RegularExpression("^(\\d)*$",ErrorMessage = "Invalid number")]
        public string Reserved { get; set; }
    }
}
