using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DatabaseAccessLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;
using ControllerLibrary;
using System.Linq;
using RestaurantService;

namespace UnitTests
{
    [TestClass]
    public class ItemTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            var resetDb = new ResetDb();
            resetDb.Clean();
        }

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
        public void Create_Item_With_Price_Service_Test()
        {
            ItemCtrl itemCtrl = new ItemCtrl();
            ItemDb itemDb = new ItemDb();
            //Setup
            #region creates ModelItem, ModelPrice
            ModelLibrary.Price newPrice = new ModelLibrary.Price
            {
                VarPrice = 12,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(100)
            };
            ModelLibrary.Item newItem = new ModelLibrary.Item
            {
                Name = "testName",
                Description = "testDescr",
                Price = newPrice

            };
            ItemService itemService = new ItemService();
            PriceService priceService = new PriceService();
            #endregion
            //Act
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            itemService.CreateItem(newItem, 1000000, 1000000);
            var checkItem = db.Items.SingleOrDefault(p => p.name == newItem.Name && p.description == newItem.Description);
            priceService.CreatePrice(newPrice, checkItem.id);


            //Get Item(1,1,"PerfectlyGoodName");

            //Assert
            
            Assert.IsNotNull( db.Items.SingleOrDefault(p=> p.id == checkItem.id));
            Assert.IsNotNull(db.Prices.Where(p => p.itemId == checkItem.id).OrderByDescending(p => p.startDate).First());
        }

        [TestMethod]
        public void Edit_Item_With_Price()
        {
            ItemCtrl itemCtrl = new ItemCtrl();
            ItemDb itemDb = new ItemDb();
            //Setup
            #region creates ModelItem, ModelPrice
            ModelLibrary.Price newPrice = new ModelLibrary.Price
            {
                VarPrice = 12,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(100)
            };
            ModelLibrary.Price newPriceUpdated = new ModelLibrary.Price
            {
                VarPrice = 20,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(100)
            };
            ModelLibrary.Item newItem = new ModelLibrary.Item
            {
                Name = "testName",
                Description = "testDescr",
                Price = newPrice

            };
            
            ItemService itemService = new ItemService();
            PriceService priceService = new PriceService();
            #endregion
            //Act
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            itemService.CreateItem(newItem, 1000000, 1000000);
            var checkItem = db.Items.SingleOrDefault(p => p.name == newItem.Name && p.description == newItem.Description);
            ModelLibrary.Item newItemUpdated = new ModelLibrary.Item
            {
                Id = checkItem.id,
                Name = "testNameUpdated",
                Description = "testDescrUpdated",
                Price = newPrice

            };
            priceService.CreatePrice(newPrice, checkItem.id);
            itemService.UpdateItem(newItemUpdated, 1000000,1000000);
            var checkItemUpdated = db.Items.Single(p => p.id == newItemUpdated.Id &&p.name == newItemUpdated.Name 
                                                            && p.description == newItemUpdated.Description);
            priceService.UpdatePrice(newPriceUpdated,checkItemUpdated.id);
            var checkPriceUpdated = db.Prices.SingleOrDefault(p=> p.itemId == checkItemUpdated.id && p.price1 == newPriceUpdated.VarPrice);

            //Assert
            Assert.IsFalse(newItem.Name == checkItemUpdated.name && newItem.Description == checkItemUpdated.description);
            Assert.IsFalse(newPrice.VarPrice == checkPriceUpdated.price1);
            Assert.IsNull(db.Prices.SingleOrDefault(p => p.itemId == checkItem.id && p.price1 == newPrice.VarPrice));
            Assert.IsTrue(newItemUpdated.Name == checkItemUpdated.name
                          && newItemUpdated.Description == checkItemUpdated.description);
            Assert.IsTrue(newPriceUpdated.VarPrice == checkPriceUpdated.price1);
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
                Id = 1000000,
                Name = "PerfectlyGoodName",
            };

            //Act
            itemCtrl.DeleteItem(1000000);

            //Assert
            Assert.IsNull(itemCtrl.GetItem(1000000));
        }

        [TestMethod]
        public void Create_Item_Service()
        {
            ItemCtrl itemCtrl = new ItemCtrl();
            //Setup
            var menu = new ModelLibrary.Menu
            {
                 Active = false,
                 Id = 1000000,
                 Name = "newName",
                 RestaurantId = 1000000, 
                  

            };
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
                Id = 1000000,
                Name = "Controllerforabi",
                Description = "Totallynotadescription",
                Price = price,
            };

            //Act
            var itemdb = itemCtrl.CreateItem(item, menu.Id, itemCat.Id);

            //Assert
            Assert.AreEqual(item.Name, itemdb.Name);
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
                Id = 1000000,
                Description = "Totallynotadescription",
                Price = price
            };
            //Act
            var itemDb = new ItemDb();


        }
    }
}
