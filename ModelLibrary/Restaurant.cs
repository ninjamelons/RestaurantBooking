using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    class Restaurant
    {
        [RegularExpression("^[a-zA-Z ]{3,20}$*", ErrorMessage = "Invalid number")]
        public string Name { get; set; }

        [RegularExpression("^[0-9 a-zA-Z]{3,150}*", ErrorMessage = "Invalid number")]
        public string Address { get; set; }

        [RegularExpression("^[0-9 a-zA-Z]{3,150}*", ErrorMessage = "Invalid number")]
        public string PhoneNo { get; set; }

        [RegularExpression("^[0-9 a-zA-Z]{3,150}*", ErrorMessage = "Invalid number")]
        public string Email { get; set; }

    }
}
