using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Menu
    {
        public int Id { get; set; }

        public bool Active { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z ]*", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }

        public int RestaurantId { get; set; }

        public IEnumerable<Item> Items{ get;  set;}

        public override string ToString()
        {
            return Name;
        }
    }
}
