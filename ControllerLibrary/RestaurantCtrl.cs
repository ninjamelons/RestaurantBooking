using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccessLibrary;
using ModelLibrary;
using System.ComponentModel.DataAnnotations;

namespace ControllerLibrary
{
    public class RestaurantCtrl
    {
        public ResTable ConvertTable(Table table)
        {
            ResTable returnTable = null;

            returnTable.noSeats = Convert.ToInt32(table.NoSeats);
            returnTable.reserved = Convert.ToInt32(table.Reserved);
            returnTable.total = Convert.ToInt32(table.Total);
            returnTable.restaurantId = Convert.ToInt32(table.RestaurantId);

            return returnTable;
        }
        public ModelLibrary.Restaurant CreateRestaurant(string name, string address, string email, string phoneNo, string zipCode, RestaurantCategory category)
        {
            var restaurant = new ModelLibrary.Restaurant
            {
                Name = name,
                Address = address,
                ZipCode = zipCode,
                PhoneNo = phoneNo,
                Email = email,
                Verified = false
            };

            var context = new ValidationContext(restaurant, null, null);
            var result = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(restaurant, context, result, true);
            if (!isModelStateValid)
                throw new ValidationException();
            return restaurant;
        }

        public ModelLibrary.Restaurant ConvertRestaurantToModel(DatabaseAccessLibrary.Restaurant dbRestaurant)
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
                CategoryId = dbRestaurant.resCatId.GetValueOrDefault()
            };
            return mRes;
        }

        public DatabaseAccessLibrary.Restaurant ConvertRestaurantToDatabase(ModelLibrary.Restaurant mRes)
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
                resCatId = mRes.CategoryId
            };
            return dbRes;
        }
    }
}
