using System.Collections.Generic;
using System.Linq;

namespace DatabaseAccessLibrary
{
    public class RestaurantsDb
    {
        public Restaurant GetRestaurant(int restaurantId)
        {
            var dbContext = new JustFeastDbDataContext();
            var res = dbContext.Restaurants.FirstOrDefault(x => x.id == restaurantId);
            return res;
        }
    }
}
