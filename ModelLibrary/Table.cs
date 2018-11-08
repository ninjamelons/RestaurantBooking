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
        [RegularExpression("^[0-9]*$",ErrorMessage = "Invalid number")]
        public string NoSeats { get; set; }
        
        [RegularExpression("^[0-9]*$",ErrorMessage = "Invalid number")]
        public string Total { get; set; }
        
        [RegularExpression("^[0-9]*$",ErrorMessage = "Invalid number")]
        public string Reserved { get; set; }
    }
}
