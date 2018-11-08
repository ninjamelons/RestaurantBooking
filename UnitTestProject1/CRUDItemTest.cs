using System;
using System.Collections.Generic;
using Moq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;
using DatabaseAccessLibrary;

namespace UnitTestProject1
{
    [TestClass]
    public class CRUDItemTest
    {
      
            [TestMethod]
            [DataRow("ChickenSteak", "WeirdDescription", 12.0, true, DisplayName = "All data correct")]
            [DataRow("a", "WeirderDescription", 12.0, false, DisplayName = "Name too short")]
            [DataRow("asdjkddsfjdsfjkdfsjkdsfkjdfjkdfsjksdfjksdfjksdfjkdsfjkdksfsdkjsdfkjh", "Weird description", 12.0, false, DisplayName = "name too long")]
            [DataRow(null, "Weird Description", 12.0, false, DisplayName = "Null name")]
            [DataRow("GoodName", "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
                "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
                "01234567890123456789012345678901234567890123456789", 12.0, false, DisplayName = "Description Too long")]
            [DataRow("GoodName", null, 12.0, false, DisplayName = "Null Description")]
            [DataRow("GoodName", "GoodDescription", -12.0, false, DisplayName = "Price < 0")]
            [DataRow("GoodName", "GoodDescription", null, false, DisplayName = "Null Price")]
            public void Create_Table_Valid_Inputs(string name, string description, double price, bool shouldValidate)
            {
                //Setup
                var sut = new ModelLibrary.Item
                {
                    Name = name,
                    Description = description,
                    Price = price,
                };

                var context = new ValidationContext(sut, null, null);
                var result = new List<ValidationResult>();

                //Act
                var isModelStateValid = Validator.TryValidateObject(sut, context, result, true);

                //Assert
                Assert.IsTrue(isModelStateValid == shouldValidate);
            }

        public void Create_Item_Added_To_Db()
        {
            //Setup
            DatabaseAccessLibrary.Item newItem = new DatabaseAccessLibrary.Item
            {
                description = "PerfectlyGoodDescription",
                id = 1,
                itemCatId = 1,
                menuId = 1,
                name = "PerfectlyGoodName",
            };
            DatabaseAccessLibrary.Menu newMenu = new Menu
            {
               id = 1,
               restaurantId = 1,
               active = true

            };
            DatabaseAccessLibrary.Restaurant newRestaurant = new Restaurant
            {
                id = 1,
                name = "RestaurantNameExample",
                address = "RestaurantAddressExample",
                email = "RestaurantEmailExample",
                zipcode = 1231223
            };
            

            ItemDb ItemDb = new ItemDb();

            //Act
            ItemDb.AddItem(newItem, newMenu, newRestaurant);

            //Get table with: noSeats = 4, reserved = 0, restaurantId = 1, total = 20
            DatabaseAccessLibrary.Item anotherItem = ItemDb.GetItem(1, 1, "PerfectlyGoodName", "PerfectlyGoodDescription", 1);

            //Assert
            Assert.Equals(anotherItem, newItem);
        }

        
    }
    }
