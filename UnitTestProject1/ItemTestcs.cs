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
        [DataRow("ChickenSteak", "WeirdDescription", true, DisplayName = "All data correct")]
        [DataRow("a", "WeirderDescription", false, DisplayName = "Name too short")]
        [DataRow("asdjkddsfjdsfjkdfsjkdsfkjdfjkdfsjksdfjksdfjksdfjkdsfjkdksfsdkjsdfkjh", "Weird description", false, DisplayName = "name too long")]
        [DataRow("GoodName", "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
                   "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
                   "01234567890123456789012345678901234567890123456789", false, DisplayName = "Description Too long")]
        public void Create_Table_Valid_Inputs(string name, string description, bool shouldValidate)
        {
            //Setup
            var sut = new ModelLibrary.Item
            {
                Name = name,
                Description = description,

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
            ItemDb itemDb = new ItemDb();
            //Setup
            #region creates Item,Menu,Restaurant,Price
            DatabaseAccessLibrary.Restaurant newRestaurant = new DatabaseAccessLibrary.Restaurant
            {

                name = "RestaurantNameExample",
                address = "RestaurantAddressExample",
                email = "RestaurantEmailExample",
                zipcode = 1231223
            };

            DatabaseAccessLibrary.Menu newMenu = new DatabaseAccessLibrary.Menu
            {

                name = "aMenuName",
                active = true

            };

            DatabaseAccessLibrary.Item newItem = new DatabaseAccessLibrary.Item
            {
                description = "PerfectlyGoodDescription",

                name = "PerfectlyGoodName",
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


            itemDb.AddItem(newItem);
            //Act
            itemDb.AddItem(newItem);

            //Get Item(1,1,"PerfectlyGoodName");
            DatabaseAccessLibrary.Item anotherItem = itemDb.GetItem(1, "PerfectlyGoodName");// PRICE!?

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
            ItemDb itemDb = new ItemDb();
            //Setup
            #region creates Item,Menu,Restaurant

            DatabaseAccessLibrary.Item newItem = new DatabaseAccessLibrary.Item
            {
                description = "PerfectlyGoodDescription",
                //ItemCat = itemCat1
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

            itemDb.AddItem(newItem);

            //Act


            //Delete Item
            itemDb.DeleteItem(newItem);

            //Assert
            Assert.IsNull(newItem);
        }/*
        [TestMethod]
        public void Update_Item(string description, double price, string name )
        {
            #region creates two items

            ModelLibrary.Item newItem = new ModelLibrary.Item
            {
                Description = "PerfectlyGoodDescription",
                Id = 1,
                //ItemCatId = 1,
                //MenuId = 1,
                Name = "PerfectlyGoodName",
                Price = 12
            };
           
                ModelLibrary.Item editItem = new ModelLibrary.Item
                {


                    Description = description,
                    Id = 1,
                    //ItemCatId = 1,
                    //MenuId = 1,
                    Name = name,
                    //Price = price
                };


            if (newItem.Description != description && newItem.Name != name && newItem.Price != price)
            {
                var tableCtrl = new TableCtrl();
                #endregion

                //Act
                ItemCtrl itemCtrl = new ItemCtrl();
                itemCtrl.UpdateItem(newItem, editItem);
                var returnedItem = ItemCtrl.GetItem(editItem);

                //Assert
                Assert.AreNotEqual(newItem, returnedItem);
            }*/






        [TestMethod]
        public void Delete_Correct_Item_Db()
        {
            ItemCtrl itemCtrl = new ItemCtrl();
            //Setup
            var item = new ModelLibrary.Item
            {
                Description = "PerfectlyGoodDescription",
                Id = 1,
                //ItemCatId = 1,
                //MenuId = 1,
                Name = "PerfectlyGoodName",
                //Price = 12
            };

            //Act
            //ItemCtrl.DeleteItem(item);
            var resTable = itemCtrl.GetItem(item);

            //Assert
            Assert.IsNull(resTable);
        }

        [TestMethod]
        public void Create_Item_Service()
        {
            ItemCtrl itemCtrl = new ItemCtrl();
            //Setup
            var itemCat = new ModelLibrary.ItemCat
            {
                Id = 1000000,
                Name = "Soup"
            };
            var price = new ModelLibrary.Price
            {
                StartDate = DateTime.Now.Date,
                EndDate = new DateTime(9999, 12, 31),
                VarPrice = 12.5

            };
            var item = new ModelLibrary.Item
            {
                Name = "Controllerforabi",
                Description = "Totallynotadescription",
                ItemCat = itemCat,
                Price = price
            };

            //Act
            var itemdb = itemCtrl.CreateItem(item, 1000000);

            //Assert
            Assert.AreEqual(item.Name, itemdb.name);
        }
        [TestMethod]
        public void Edit_Item_Service()
        {
            ItemCtrl itemCtrl = new ItemCtrl();
            //Setup
            var itemCat = new ModelLibrary.ItemCat
            {
                Id = 1000000,
                Name = "Soup"
            };
            var price = new ModelLibrary.Price
            {
                StartDate = DateTime.Now.Date,
                EndDate = new DateTime(9999, 12, 31),
                VarPrice = 12.5

            };
            var item = new ModelLibrary.Item
            {
                Name = "Controllerforabi",
                Description = "Totallynotadescription",
                ItemCat = itemCat,
                Price = price
            };
            //Act
            var itemDb = new ItemDb();


        }
    }
}
