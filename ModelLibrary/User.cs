using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class User
    {
        [Required]
        public string Password { get; set; }
        public int CustomerId { get; set; }
    }
}
