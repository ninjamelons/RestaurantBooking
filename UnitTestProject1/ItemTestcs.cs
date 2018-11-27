using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DatabaseAccessLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;
using ControllerLibrary;

namespace UnitTestProject1
{
    [TestClass]
    public class ItemTestcs
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
                //Price = price,
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
            #region creates Item,Menu,Restaurant,Price

            DatabaseAccessLibrary.Item newItem = new DatabaseAccessLibrary.Item
            {
                description = "PerfectlyGoodDescription",
                id = 1,
                itemCatId = 1,
                menuId = 1,
                name = "PerfectlyGoodName",
            };
            DatabaseAccessLibrary.Menu newMenu = new DatabaseAccessLibrary.Menu
            {
                id = 1,
                restaurantId = 1,
                active = true

            };

            DatabaseAccessLibrary.Restaurant newRestaurant = new DatabaseAccessLibrary.Restaurant
            {
                id = 1,
                name = "RestaurantNameExample",
                address = "RestaurantAddressExample",
                email = "RestaurantEmailExample",
                zipcode = 1231223
            };
            DateTime startDate = new DateTime(2011, 6, 10);
            DateTime endDate = new DateTime(2011, 7, 11);
            DatabaseAccessLibrary.Price newPrice = new DatabaseAccessLibrary.Price
            {
                price1 = 12, // price int?
                startDate = startDate,
                endDate = endDate

            };
            #endregion

            ItemDb itemDb = new ItemDb();
            ItemDb.AddItem(newItem);
            //Act
            ItemDb.AddItem(newItem);
            
            //Get Item(1,1,"PerfectlyGoodName");
            DatabaseAccessLibrary.Item anotherItem = ItemDb.GetItem(1, "PerfectlyGoodName");// PRICE!?

            //Assert
            Assert.IsTrue(anotherItem.name == newItem.name 
                          && anotherItem.menuId == newItem.menuId
                          && anotherItem.id == newItem.id
                          && anotherItem.description == newItem.description
                          && anotherItem.itemCatId == newItem.itemCatId); //

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
            DatabaseAccessLibrary.Menu newMenu = new DatabaseAccessLibrary.Menu
            {
                id = 1,
                restaurantId = 1,
                active = true

            };

            DatabaseAccessLibrary.Restaurant newRestaurant = new DatabaseAccessLibrary.Restaurant
            {
                id = 1,
                name = "RestaurantNameExample",
                address = "RestaurantAddressExample",
                email = "RestaurantEmailExample",
                zipcode = 1231223
            };
            #endregion

            ItemDb.AddItem(newItem);

            //Act


            //Delete Item
            ItemDb.DeleteItem(newItem);

            //Assert
            Assert.IsNull(newItem);
        }
        [TestMethod]
        public void Update_Item(string description, double price, string name )
        {
            #region creates two items

            ModelLibrary.Item newItem = new ModelLibrary.Item
            {
                Description = "PerfectlyGoodDescription",
                Id = 1,
                ItemCatId = 1,
                MenuId = 1,
                Name = "PerfectlyGoodName",
                //Price = 12
            };
           
                ModelLibrary.Item editItem = new ModelLibrary.Item
                {


                    Description = description,
                    Id = 1,
                    ItemCatId = 1,
                    MenuId = 1,
                    Name = name,
                    //Price = price
                };


            if (newItem.Description != description && newItem.Name != name /*&& newItem.Price != price*/)
            {
                var tableCtrl = new TableCtrl();
                #endregion

                //Act
                ItemCtrl itemCtrl = new ItemCtrl();
                itemCtrl.UpdateItem(newItem, editItem);
                var returnedItem = ItemCtrl.GetItem(editItem);

                //Assert
                Assert.AreNotEqual(newItem, returnedItem);
            }
          
            



        }
        [TestMethod]
        public void Delete_Correct_Item_Db()
        {
            //Setup
            var item = new ModelLibrary.Item
            {
                Description = "PerfectlyGoodDescription",
                Id = 1,
                ItemCatId = 1,
                MenuId = 1,
                Name = "PerfectlyGoodName",
                //Price = 12
            };

            //Act
            ItemCtrl.DeleteTable(item);
            var resTable = ItemCtrl.GetItem(item);

            //Assert
            Assert.IsNull(resTable);
        }

    }
}
