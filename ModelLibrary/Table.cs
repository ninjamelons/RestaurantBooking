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
        public int TableId { get; set; }

        public int RestaurantId { get; set; }

        public int NoSeats { get; set; }
        
        public bool Reserved { get; set; }
    }
}
