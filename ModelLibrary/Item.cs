using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Item
    {
        [RegularExpression("^[a-zA-Z ]{3,20}$*", ErrorMessage = "Invalid number")]
        public string Name { get; set; }

        [RegularExpression("^[0-9 a-zA-Z]{3,150}*", ErrorMessage = "Invalid number")]
        public string Description { get; set; }

        [RegularExpression("^[0-9/.]*", ErrorMessage = "Invalid number")]
        public double Price { get; set; }
    }
}
