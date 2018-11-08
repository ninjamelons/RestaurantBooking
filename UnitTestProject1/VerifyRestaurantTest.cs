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



namespace UnitTestProject1
{
    class VerifyRestaurantTest
    {
        [TestMethod]
        [DataRow("RestaurantName", "Good Address", "+45 50 38 97 04", "ohshit@gmail.com", true, DisplayName = "Restaurant is verify!")]
        [DataRow("Restadfds", "Good Address", "+45 50 38 97 04", "ohshit@gmail.com", false,  DisplayName = "Restaurant is not verify!")]
        public void Test_Restaurant_Verify(string name, string address, string phoneNo, string email, bool shouldVerify)


        {
            //Setup
            var sut = new Restaurant
            {
                Name = name,
                address = address,
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
