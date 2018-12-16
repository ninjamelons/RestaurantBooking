using System;
using Moq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ModelLibrary;

namespace UnitTests
{
    [TestClass]
    public class VerifyRestaurantTest
    {
        [TestMethod]
        [DataRow(1000010, "RestaurantName", "Good Address","9000", "004550389704", "ohshit@gmail.com", true, DisplayName = "Restaurant is verified!")]
        [DataRow(1000010, "Restadfds", "Good Address", "9000", "004550389704", "ohshitgmail.com", false,  DisplayName = "Restaurant is not verified!")]
        public void Test_Restaurant_Verify(int id, string name, string address, string zipcode, string phoneNo, string email, bool shouldVerify)


        {
            //Setup
            var sut =  new Restaurant
            {
                Id = id,
                Name = name,
                Address = address,
                ZipCode = zipcode,
                PhoneNo = phoneNo,
                Email = email,
               
            };

            var context = new ValidationContext(sut, null, null);
            var result = new List<ValidationResult>();

            //Act
            var isModelStateValid = Validator.TryValidateObject(sut, context, result, true);

            //Assert
            Assert.IsTrue(isModelStateValid == shouldVerify);
        }
    }
}
