using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Item
    {   [Required]
        [RegularExpression("^[a-zA-Z ]{3,20}$*", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }

        [RegularExpression("^[0-9 a-zA-Z]{3,150}*", ErrorMessage = "Invalid Description")]
        public string Description { get; set; }

        [RegularExpression("^[0-9/.]*", ErrorMessage = "Invalid Price")]
        public double Price { get; set; }
    }
}
