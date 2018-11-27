using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseAccessLibrary
{
    public static class RestaurantsDb
    {
        public static List<Restaurant> GetRestaurantsPaged(int zipcode, int categoryId, int page, int amount, bool verified, bool discontinued)
        {
            var db = new JustFeastDbDataContext();
            var restaurants = new List<Restaurant>();

            restaurants = db.Restaurants.Where(x => 
            (zipcode == 0 ? true : x.zipcode == zipcode) && 
            (categoryId == 0 ? true : x.resCatId == categoryId) &&
            (verified ? x.verified : true) &&
            (discontinued ? true : !x.discontinued))
            .OrderBy(x => x.name).Skip(page*amount).Take(amount).ToList();

            return restaurants;
        }

        public static Restaurant GetRestaurant(int restaurantId)
        {
            var db = new JustFeastDbDataContext();
            var res = db.Restaurants.FirstOrDefault(x => x.id == restaurantId);
            return res;
        }
    }
}
