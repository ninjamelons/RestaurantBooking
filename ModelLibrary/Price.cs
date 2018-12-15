using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Price
    {
        [Required]
        [RegularExpression("^[0-9.]*", ErrorMessage = "Invalid Price")]
        public double VarPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return VarPrice.ToString();
        }
    }
}
