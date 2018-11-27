﻿using System;
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
        public static ResTable ConvertTable(Table table)
        {
            ResTable returnTable = null;

            returnTable.noSeats = Convert.ToInt32(table.NoSeats);
            returnTable.reserved = Convert.ToInt32(table.Reserved);
            returnTable.total = Convert.ToInt32(table.Total);
            returnTable.restaurantId = Convert.ToInt32(table.RestaurantId);

            return returnTable;
        }
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
                Category = ConvertRestaurantCategoryToModel(dbRestaurant.ResCat),
                Discontinued = dbRestaurant.discontinued,
            };

           // if(dbRestaurant.resCatId != null)
           //     mRes.CategoryId = dbRestaurant.resCatId.Value;

            return mRes;
        }

        public static List<ModelLibrary.Restaurant> ConvertRestaurantListToModel(IEnumerable<DatabaseAccessLibrary.Restaurant> restaurants)
        {
            var mRes = new List<ModelLibrary.Restaurant>();
            foreach(var x in restaurants)
            {
                mRes.Add(ConvertRestaurantToModel(x));
            }
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

            var dbRes = new DatabaseAccessLibrary.Restaurant();
            dbRes.name = mRes.Name;
            dbRes.address = mRes.Address;
            dbRes.email = mRes.Email;
            dbRes.id = mRes.Id;
            dbRes.phoneNo = PhoneNo;
            dbRes.verified = mRes.Verified;
            dbRes.zipcode = ZipCode;
            dbRes.discontinued = mRes.Discontinued;
            dbRes.ResCat = ConvertRestaurantCategoryToDatabase(mRes.Category);

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

        public static List<ModelLibrary.Restaurant> GetRestaurantsPaged(int zipcode = 0, int categoryId = 0, int page = 0, int amount = 10, bool verified = true, bool discontinued = false)
        {
            var res = RestaurantsDb.GetRestaurantsPaged(zipcode, categoryId, page, amount, verified, discontinued);
            var mRes = ConvertRestaurantListToModel(res);
            return mRes;
        }
    }
}
