using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using ModelLibrary;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UnitTestProject1
{
    [TestClass]
    public class OrderTest
    {
        
        [SetUp]
        public void SetUp()
        {
        }

        [TestMethod]
        [DataRow("1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "2", true, true, DisplayName = "Valid input")]
        public void Place_Order_Test(string orderId, string dateTime, string reservationDateTime,
            string noSeats, bool accepted, bool shouldValidate)
        {
            Mock itemMock = new Mock<Item>();
            List<Item> itemList = new List<Item>
            {
                new Mock<Item>("1000000", "Chicken Curry Soup", "Spicy curry soup with chicken breast and vegetables", "1000000").Object
            };
            var sut = new Order
            {
                OrderId = orderId,
                DateTime = dateTime,
                ReservationDateTime = reservationDateTime,
                ItemsList = itemList,
                NoSeats = noSeats,
                Accepted = accepted
            };

            var context = new ValidationContext(sut, null, null);
            var result = new List<ValidationResult>();

            var isModelStateValid = Validator.TryValidateObject(sut, context, result, true);

            Assert.IsTrue(shouldValidate == isModelStateValid);
        }
    }
}