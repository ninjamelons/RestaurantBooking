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

        [Required]
        [RegularExpression("^(\\d)*$",ErrorMessage = "Invalid number: Cannot contain spaces, symbols, or letters")]
        public string NoSeats { get; set; }
        
        [Required]
        [RegularExpression("^(\\d)*$",ErrorMessage = "Invalid number: Cannot contain spaces, symbols, or letters")]
        public string Total { get; set; }
        
        [Required]
        [RegularExpression("^(\\d)*$",ErrorMessage = "Invalid number: Cannot contain spaces, symbols, or letters")]
        public string Reserved { get; set; }
    }
}
