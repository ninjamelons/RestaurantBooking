using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class RestaurantCategory
    {
        public int Id { get; set; }
        [Display(Name="Category")]
        public string Name { get; set; }
        public List<Restaurant> Restaurants { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
