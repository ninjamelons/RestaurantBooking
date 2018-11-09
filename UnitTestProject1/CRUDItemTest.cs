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

        [TestMethod]
        public void Create_Item_Added_To_Db()
        {
            //Setup
            #region creates Item,Menu,Restaurant

            DatabaseAccessLibrary.Item newItem = new DatabaseAccessLibrary.Item
            {
                description = "PerfectlyGoodDescription",
                id = 1,
                itemCatId = 1,
                menuId = 1,
                name = "PerfectlyGoodName",
            };
            Menu newMenu = new Menu
            {
               id = 1,
               restaurantId = 1,
               active = true

            };
            
            Restaurant newRestaurant = new Restaurant
            {
                id = 1,
                name = "RestaurantNameExample",
                address = "RestaurantAddressExample",
                email = "RestaurantEmailExample",
                zipcode = 1231223
            };
            #endregion


            ItemDb itemDb = new ItemDb();

            //Act
            itemDb.AddItem(newItem);

            //Get Item(1,1,"PerfectlyGoodName");
            DatabaseAccessLibrary.Item anotherItem = itemDb.GetItem(1, "PerfectlyGoodName");

            //Assert
            Assert.Equals(anotherItem, newItem);
        }
        [TestMethod]
        public void Item_Deleted_From_Db()
        {
            //Setup
            #region creates Item,Menu,Restaurant

            DatabaseAccessLibrary.Item newItem = new DatabaseAccessLibrary.Item
            {
                description = "PerfectlyGoodDescription",
                id = 1,
                itemCatId = 1,
                menuId = 1,
                name = "PerfectlyGoodName",
            };
            Menu newMenu = new Menu
            {
                id = 1,
                restaurantId = 1,
                active = true

            };

            Restaurant newRestaurant = new Restaurant
            {
                id = 1,
                name = "RestaurantNameExample",
                address = "RestaurantAddressExample",
                email = "RestaurantEmailExample",
                zipcode = 1231223
            };
            #endregion


            ItemDb itemDb = new ItemDb();
            itemDb.AddItem(newItem);
            
            //Act


            //Delete Item
            itemDb.DeleteItem(newItem);

            //Assert
            Assert.IsNull(newItem);
        }
        [TestMethod]
        public void Update_Item_In_Db()
        {
            #region creates Item,Menu,Restaurant

            DatabaseAccessLibrary.Item newItem = new DatabaseAccessLibrary.Item
            {
                description = "PerfectlyGoodDescription",
                id = 1,
                itemCatId = 1,
                menuId = 1,
                name = "PerfectlyGoodName",
            };
            Menu newMenu = new Menu
            {
                id = 1,
                restaurantId = 1,
                active = true

            };

            Restaurant newRestaurant = new Restaurant
            {
                id = 1,
                name = "RestaurantNameExample",
                address = "RestaurantAddressExample",
                email = "RestaurantEmailExample",
                zipcode = 1231223
            };
            #endregion

            ItemDb itemDb = new ItemDb();
            itemDb.AddItem(newItem);
            

            //Get Item(1,1,"PerfectlyGoodName");
            DatabaseAccessLibrary.Item anotherItem = itemDb.GetItem(1, "PerfectlyGoodName");
            itemDb.UpdateItem(newItem);
            //Assert
            Assert.AreNotEqual(newItem, anotherItem);



        }
        [TestMethod]
        public void Update_Item_In_Db_Without_Changes()
        {

        }
    }
    }
