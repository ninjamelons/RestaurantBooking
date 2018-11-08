using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class RestaurantCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}
