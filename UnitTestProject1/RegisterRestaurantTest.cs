using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using ModelLibrary;
using ControllerLibrary;
using System;

namespace UnitTests
{
    /// <summary>
    /// Summary description for RegisterRestaurantTest
    /// </summary>
    [TestClass]
    public class RegisterRestaurantTest
    {
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        [DataRow("9000", "Restaurant Name", "Restaurant Address", "4552900554", "virma21@gmail.com", true, DisplayName = "Valid data")]
        [DataRow("9000", "Restaurant Name", "Restaurant Address", "4552900554", "", false, DisplayName = "Empty Email")]
        [DataRow("9000", "Restaurant Name", "Restaurant Address", "4552900554", "virma21@@!@#$@gmail.com", false, DisplayName = "Bad Symbols in Email")]
        [DataRow("9000", "Restaurant Name", "Restaurant Address", "4552900554", "virma21gmail.com", false, DisplayName = "No @ in email")]
        [DataRow("9000", "Restaurant Name", "Restaurant Address", "", "virma21@gmail.com", true, DisplayName = "Empty Number")]
        [DataRow("9000", "Restaurant Name", "Restaurant Address", "+4%@$5 52 90 05 54", "virma21@gmail.com", false, DisplayName = "Symbols in number")]
        [DataRow("9000", "Restaurant Name", "Restaurant Address", "455290055455555555555555555555555", "virma21@gmail.com", false, DisplayName = "Long number")]
        [DataRow("9000", "Restaurant Name", "Restaurant Addresssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss", "4552900554", "virma21@gmail.com", false, DisplayName = "Long address")]
        [DataRow("9000", "Restaurant Name", "", "4552900554", "virma21@gmail.com", false, DisplayName = "empty address")]
        [DataRow("9000", "Restaurant Name", "#@#$!", "4552900554", "virma21@gmail.com", false, DisplayName = "symbols in address")]
        [DataRow("9000", "Restaurant Nameeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee", "Restaurant Address", "4552900554", "virma21@gmail.com", false, DisplayName = "Long name")]
        [DataRow("9000", "", "Restaurant Address", "4552900554", "virma21@gmail.com", false, DisplayName = "Empty name")]
        [DataRow("9000", "!@*#&^!@(", "Restaurant Address", "4552900554", "virma21@gmail.com", false, DisplayName = "Symbols in name")]
        [DataRow("", "Restaurant Name", "Restaurant Address", "4552900554", "virma21@gmail.com", false, DisplayName = "Empty ZipCode")]
        [DataRow("9000000000", "Restaurant Name", "Restaurant Address", "4552900554", "virma21@gmail.com", false, DisplayName = "Long ZipCode")]
        [DataRow("#$%", "Restaurant Name", "Restaurant Address", "4552900554", "virma21@gmail.com", false, DisplayName = "Symbols in ZipCode")]
        public void Restaurant_Validation_Test(string zipCode, string name, string address, string phoneNo, string email, bool shouldValidate)
        {
            // Setup
            var sut = new Restaurant
            {
                Name = name,
                Address = address,
                ZipCode = zipCode,
                PhoneNo = phoneNo,
                Email = email
            };

            var context = new ValidationContext(sut, null, null);
            var result = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(sut, context, result, true);

            // Assert
            Assert.IsTrue(isModelStateValid == shouldValidate);
        }

        [TestMethod]
        public void Controller_Will_Not_Create_Restaurant_If_Not_Validated()
        {
            // Setup
            //RestaurantCtrl ctrl = new RestaurantCtrl();

            // Act
            try
            {
                var res = RestaurantCtrl.CreateRestaurant("Na#@%me", "Add!@#%ress", "Email@g@#$!mail.com", "123@#$4", "90034650", null);
            }

            // Assert
            catch(Exception ex)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void Controller_Will_Create_Restaurant_If_Validated()
        {
            // Setup
           // RestaurantCtrl ctrl = new RestaurantCtrl();

            // Act
            try
            {
                var res = RestaurantCtrl.CreateRestaurant("Name", "Address", "Email@gmail.com", "1234", "9000", null);
            }

            // Assert
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Controller_Will_Set_Created_Restaurant_As_Not_Verified()
        {
            // Setup
            //RestaurantCtrl ctrl = new RestaurantCtrl();

            // Act
            var res = RestaurantCtrl.CreateRestaurant("Name", "Address", "Email@gmail.com", "1234", "9000", null);

            // Assert
            Assert.IsTrue(!res.Verified);
        }

        [TestMethod]
        public void Service_Will_Create_Restaurant()
        {
            // Setup
           // RestaurantService.RestaurantServiceClient proxy = new RestaurantService.RestaurantServiceClient();

            // Act

            // Assert
            Assert.Fail();
        }
    }
}
