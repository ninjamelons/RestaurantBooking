using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccessLibrary;
using ModelLibrary;
using System.ComponentModel.DataAnnotations;
using Restaurant = DatabaseAccessLibrary.Restaurant;

namespace ControllerLibrary
{
    public class RestaurantCtrl
    {
        public static ModelLibrary.Restaurant CreateRestaurant(string name, string address, string email, string phoneNo, string zipCode, RestaurantCategory category)
        {
            var restaurant = new ModelLibrary.Restaurant
            {
                Name = name,
                Address = address,
                ZipCode = zipCode,
                PhoneNo = phoneNo,
                Email = email,
                Verified = false,
                Category = category
            };

            var context = new ValidationContext(restaurant, null, null);
            var result = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(restaurant, context, result, true);
            if (!isModelStateValid)
                throw new ValidationException();
            return restaurant;
        }
        public static RestaurantCategory CreateRestaurantCategory(string name)
        {
            return new RestaurantCategory { Name = name };
        }

        public ModelLibrary.Restaurant GetRestaurant(int restaurantId)
        {
            var resDb = new RestaurantsDb();
            var menuCtrl = new MenuCtrl();
            var res = ConvertRestaurantToModel(resDb.GetRestaurant(restaurantId));
            res.Menu = menuCtrl.GetActiveMenu(restaurantId);
            return res;
        }

        public static ModelLibrary.Restaurant ConvertRestaurantToModel(DatabaseAccessLibrary.Restaurant dbRestaurant)
        {
            var mRes = new ModelLibrary.Restaurant
            {
                Name = dbRestaurant.name,
                Address = dbRestaurant.address,
                Email = dbRestaurant.email,
                Id = dbRestaurant.id,
                PhoneNo = dbRestaurant.phoneNo.ToString(),
                Verified = dbRestaurant.verified,
                ZipCode = dbRestaurant.zipcode.ToString(),
                Category = ConvertRestaurantCategoryToModel(dbRestaurant.ResCat)
            };
            return mRes;
        }

        public static DatabaseAccessLibrary.Restaurant ConvertRestaurantToDatabase(ModelLibrary.Restaurant mRes)
        {
            int PhoneNo = 0;
            int ZipCode = 0;
            try
            {
                ZipCode = Int32.Parse(mRes.ZipCode);
                PhoneNo = Int32.Parse(mRes.PhoneNo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var dbRes = new DatabaseAccessLibrary.Restaurant
            {
                name = mRes.Name,
                address = mRes.Address,
                email = mRes.Email,
                id = mRes.Id,
                phoneNo = PhoneNo,
                verified = mRes.Verified,
                zipcode = ZipCode,
                ResCat = ConvertRestaurantCategoryToDatabase(mRes.Category)
            };
            return dbRes;
        }

        public static RestaurantCategory ConvertRestaurantCategoryToModel(ResCat cat)
        {
            if (cat == null)
                return null;
            var resCat = new RestaurantCategory
            {
                Id = cat.id,
                Name = cat.name
            };
            return resCat;
        }

        public static ResCat ConvertRestaurantCategoryToDatabase(RestaurantCategory cat)
        {
            if (cat == null)
                return null;
            var resCat = new ResCat
            {
                id = cat.Id,
                name = cat.Name
            };
            return resCat;
        }
    }
}
