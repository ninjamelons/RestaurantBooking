using System.Collections.Generic;
using System.Linq;

namespace DatabaseAccessLibrary
{
    public class RestaurantsDb
    {
        public DatabaseAccessLibrary.Restaurant GetRestaurant(int id, string name, string address, int zipcode, int phoneNo, string email)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var dbRestaurants = db.Restaurants.Single(a => a.id == id && a.name == name && a.address == address && a.zipcode == zipcode
                                              && a.phoneNo == phoneNo && a.email == email);
            return dbRestaurants;
        }
    }
}
